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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Show content depends on specified condition
    /// </summary>
    [ContentProperty(nameof(ContentWhenTrue))]
    public class ConditionalControl : Control
    {
        static ConditionalControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConditionalControl), new FrameworkPropertyMetadata(typeof(ConditionalControl)));
        }


        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Condition of the current <see cref="ConditionalControl"/>
        /// </summary>
        public bool Condition
        {
            get { return (bool)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }

        /// <summary>
        /// Content will be shown when condition is true
        /// </summary>
        public object ContentWhenTrue
        {
            get { return (object)GetValue(ContentWhenTrueProperty); }
            set { SetValue(ContentWhenTrueProperty, value); }
        }

        /// <summary>
        /// Content will be shown when condition is false
        /// </summary>
        public object ContentWhenFalse
        {
            get { return (object)GetValue(ContentWhenFalseProperty); }
            set { SetValue(ContentWhenFalseProperty, value); }
        }

        /// <summary>
        /// ControlTemplate will be used when condition is true
        /// </summary>
        public DataTemplate ContentTemplateWhenTrue
        {
            get { return (DataTemplate)GetValue(ContentTemplateWhenTrueProperty); }
            set { SetValue(ContentTemplateWhenTrueProperty, value); }
        }

        /// <summary>
        /// ControlTemplate will be used when condition is false
        /// </summary>
        public DataTemplate ContentTemplateWhenFalse
        {
            get { return (DataTemplate)GetValue(ContentTemplateWhenFalseProperty); }
            set { SetValue(ContentTemplateWhenFalseProperty, value); }
        }

        /// <summary>
        /// Transition of content switching
        /// </summary>
        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        /// <summary>
        /// Shown Content
        /// </summary>
        public object ComputedContent
        {
            get { return (object)GetValue(ComputedContentProperty); }
        }

        /// <summary>
        /// Used ContentTemplate
        /// </summary>
        public DataTemplate ComputedContentTemplate
        {
            set { SetValue(ComputedContentTemplatePropertyKey, value); }
        }

        /// <summary>
        /// The key needed set a read-only property
        /// </summary>
        private static readonly DependencyPropertyKey ComputedContentPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ComputedContent), typeof(object), typeof(ConditionalControl), new PropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="ComputedContent"/> property
        /// </summary>
        public static readonly DependencyProperty ComputedContentProperty = ComputedContentPropertyKey.DependencyProperty;

        /// <summary>
        /// The key needed set a read-only property
        /// </summary>
        public static readonly DependencyPropertyKey ComputedContentTemplatePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ComputedContentTemplate), typeof(DataTemplate), typeof(ConditionalControl), new PropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="ComputedContentTemplate"/> property
        /// </summary>
        public static readonly DependencyProperty ComputedContentTemplateProperty = ComputedContentTemplatePropertyKey.DependencyProperty;



        /// <summary>
        /// The DependencyProperty of <see cref="CornerRadius"/> property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ConditionalControl));

        /// <summary>
        /// The DependencyProperty of <see cref="Condition"/> property
        /// </summary>
        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register(nameof(Condition), typeof(bool), typeof(ConditionalControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentWhenTrue"/> property
        /// </summary>
        public static readonly DependencyProperty ContentWhenTrueProperty =
            DependencyProperty.Register(nameof(ContentWhenTrue), typeof(object), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentWhenFalse"/> property
        /// </summary>
        public static readonly DependencyProperty ContentWhenFalseProperty =
            DependencyProperty.Register(nameof(ContentWhenFalse), typeof(object), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentTemplateWhenFalse"/> property
        /// </summary>
        public static readonly DependencyProperty ContentTemplateWhenFalseProperty =
            DependencyProperty.Register(nameof(ContentTemplateWhenFalse), typeof(DataTemplate), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentTemplateWhenTrue"/> property
        /// </summary>
        public static readonly DependencyProperty ContentTemplateWhenTrueProperty =
            DependencyProperty.Register(nameof(ContentTemplateWhenTrue), typeof(DataTemplate), typeof(ConditionalControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="TransitionProperty"/> property
        /// </summary>
        public static readonly DependencyProperty TransitionProperty =
            TransitioningContentControl.TransitionProperty.AddOwner(typeof(ConditionalControl));

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            var condition = Condition;
            SetValue(ComputedContentPropertyKey, condition ? ContentWhenTrue : ContentWhenFalse);
            SetValue(ComputedContentTemplatePropertyKey, condition ? ContentTemplateWhenTrue : ContentTemplateWhenFalse);

            return base.MeasureOverride(constraint);
        }
    }
}
