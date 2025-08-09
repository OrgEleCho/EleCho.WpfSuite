using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using EleCho.WpfSuite.Media.Animation;

namespace EleCho.WpfSuite.Media.MarkupExtensions
{
    public class AnimatedBindingExtension : MarkupExtension
    {
        public PropertyPath? Path { get; set; }
        public IValueConverter? Converter { get; set; }

        public AnimatedBindingExtension() { }

        public AnimatedBindingExtension(PropertyPath path)
        {
            Path = path;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget valueTargetProvider)
            {
                throw new InvalidOperationException("Service provider must implement IProvideValueTarget.");
            }

            if (valueTargetProvider.TargetObject is not FrameworkElement targetElement)
            {
                throw new InvalidOperationException("Target object must be a FrameworkElement.");
            }

            if (targetElement.DataContext is null)
            {
                throw new InvalidOperationException("DataContext of target element cannot be null.");
            }

            var originBinding = new Binding()
            {
                Source = targetElement.DataContext,
                Path = Path,
                Converter = Converter,
            };

            var proxy = new AnimatedBindingProxy();
            BindingOperations.SetBinding(proxy, AnimatedBindingProxy.ValueProperty, originBinding);

            var outputBinding = new Binding()
            {
                Source = proxy,
                Path = new PropertyPath(nameof(AnimatedBindingProxy.AnimatedValue)),
                Mode = BindingMode.OneWay
            };

            return outputBinding.ProvideValue(serviceProvider);
        }

        private class AnimatedBindingProxy : DependencyObject
        {
            private IValueAnimator? _valueAnimator;
            private Type? _valueTypeOfAnimator;

            public object? Value
            {
                get { return (object)GetValue(ValueProperty); }
                set { SetValue(ValueProperty, value); }
            }

            public object? AnimatedValue
            {
                get { return (object)GetValue(AnimatedValueProperty); }
                private set { SetValue(AnimatedValueProperty, value); }
            }

            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(AnimatedBindingProxy),
                    new PropertyMetadata(null, propertyChangedCallback: OnValuePropertyChanged));

            public static readonly DependencyProperty AnimatedValueProperty =
                DependencyProperty.Register("AnimatedValue", typeof(object), typeof(AnimatedBindingProxy),
                    new PropertyMetadata(null, propertyChangedCallback: OnValuePropertyChanged));

            private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is not AnimatedBindingProxy proxy)
                {
                    return;
                }

                if (e.NewValue is null)
                {
                    proxy.AnimatedValue = null;
                    proxy._valueAnimator = null;
                    proxy._valueTypeOfAnimator = null;
                    return;
                }

                var valueType = e.NewValue.GetType();

                if (proxy._valueAnimator is null ||
                    proxy._valueTypeOfAnimator != valueType)
                {
                    proxy._valueAnimator = ValueAnimatorUtilities.CreateFromType(valueType);
                    proxy._valueTypeOfAnimator = valueType;

                    BindingOperations.SetBinding(proxy, AnimatedValueProperty,
                        new Binding(nameof(IValueAnimator.AnimatedValue))
                        {
                            Source = proxy._valueAnimator,
                            Mode = BindingMode.OneWay
                        });
                }

                proxy._valueAnimator.Value = e.NewValue;
            }
        }
    }
}
