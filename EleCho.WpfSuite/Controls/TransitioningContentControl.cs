using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{
    [TemplatePart(Name = "PART_Contents")]
    public class TransitioningContentControl : Control
    {
        static TransitioningContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitioningContentControl), new FrameworkPropertyMetadata(typeof(TransitioningContentControl)));
        }

        private Panel? _contentsPanel;
        private UIElement? _lastOldControl;
        private CancellationTokenSource? _lastTaskCancellation;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentsPanel = (Panel)GetTemplateChild("PART_Contents");
        }

        public object? Content
        {
            get { return (object?)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        public IContentTransition? Transition
        {
            get { return (IContentTransition?)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }


        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnContentChanged)));

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ContentTemplateSelectorProperty =
            DependencyProperty.Register(nameof(ContentTemplateSelector), typeof(DataTemplateSelector), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.Register(nameof(Transition), typeof(IContentTransition), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TransitioningContentControl transitioningContentControl)
            {
                _ = transitioningContentControl.OnContentChanged(e.OldValue, e.NewValue);
            }
        }

        private async Task OnContentChanged(object oldContent, object newContent)
        {
            if (_contentsPanel is null)
            {
                throw new InvalidOperationException("Can not find 'PART_Contents' in control template");
            }

            if (_lastOldControl is not null)
            {
                _contentsPanel.Children.Remove(_lastOldControl);
            }

            if (_lastTaskCancellation is not null)
            {
                _lastTaskCancellation.Cancel();
            }

            UIElement? oldContentElement = null;
            if (_contentsPanel.Children.Count > 0)
            {
                oldContentElement = _contentsPanel.Children[_contentsPanel.Children.Count - 1];
            }

            ContentPresenter newContentElement = new ContentPresenter()
            {
                Content = newContent
            };

            newContentElement.SetBinding(ContentPresenter.ContentTemplateProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(ContentTemplate)),
                });

            newContentElement.SetBinding(ContentPresenter.ContentTemplateSelectorProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(ContentTemplateSelector)),
                });

            _contentsPanel.Children.Add(newContentElement);
            _lastOldControl = oldContentElement;
            if (Transition is IContentTransition transition && 
                oldContentElement is FrameworkElement oldContentFrameworkElement)
            {
                _lastTaskCancellation = new();
                await transition.Run(this, oldContentFrameworkElement, newContentElement, true, _lastTaskCancellation.Token);
            }

            if (oldContentElement is not null)
            {
                _contentsPanel.Children.Remove(oldContentElement);
            }
        }
    }
}
