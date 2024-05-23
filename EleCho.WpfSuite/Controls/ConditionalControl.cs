using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class ConditionalControl : Control
    {
        static ConditionalControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConditionalControl), new FrameworkPropertyMetadata(typeof(ConditionalControl)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public bool Condition
        {
            get { return (bool)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }

        public object ContentWhenTrue
        {
            get { return (object)GetValue(ContentWhenTrueProperty); }
            set { SetValue(ContentWhenTrueProperty, value); }
        }

        public object ContentWhenFalse
        {
            get { return (object)GetValue(ContentWhenFalseProperty); }
            set { SetValue(ContentWhenFalseProperty, value); }
        }

        public DataTemplate ContentTemplateWhenTrue
        {
            get { return (DataTemplate)GetValue(ContentTemplateWhenTrueProperty); }
            set { SetValue(ContentTemplateWhenTrueProperty, value); }
        }

        public DataTemplate ContentTemplateWhenFalse
        {
            get { return (DataTemplate)GetValue(ContentTemplateWhenFalseProperty); }
            set { SetValue(ContentTemplateWhenFalseProperty, value); }
        }

        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public object ComputedContent
        {
            get { return (object)GetValue(ComputedContentProperty); }
        }

        public DataTemplate ComputedContentTemplate
        {
            set { SetValue(ComputedContentTemplatePropertyKey, value); }
        }

        public static readonly DependencyPropertyKey ComputedContentTemplatePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ComputedContentTemplate), typeof(DataTemplate), typeof(ConditionalControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ComputedContentTemplateProperty = ComputedContentTemplatePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ComputedContentPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ComputedContent), typeof(object), typeof(ConditionalControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ComputedContentProperty = ComputedContentPropertyKey.DependencyProperty;



        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ConditionalControl));

        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register(nameof(Condition), typeof(bool), typeof(ConditionalControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ContentWhenTrueProperty =
            DependencyProperty.Register(nameof(ContentWhenTrue), typeof(object), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ContentWhenFalseProperty =
            DependencyProperty.Register(nameof(ContentWhenFalse), typeof(object), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ContentTemplateWhenFalseProperty =
            DependencyProperty.Register(nameof(ContentTemplateWhenFalse), typeof(DataTemplate), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ContentTemplateWhenTrueProperty =
            DependencyProperty.Register(nameof(ContentTemplateWhenTrue), typeof(DataTemplate), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty TransitionProperty =
            TransitioningContentControl.TransitionProperty.AddOwner(typeof(ConditionalControl));

        protected override Size MeasureOverride(Size constraint)
        {
            var condition = Condition;
            SetValue(ComputedContentPropertyKey, condition ? ContentWhenTrue : ContentWhenFalse);
            SetValue(ComputedContentTemplatePropertyKey, condition ? ContentTemplateWhenTrue : ContentTemplateWhenFalse);

            return base.MeasureOverride(constraint);
        }
    }
}
