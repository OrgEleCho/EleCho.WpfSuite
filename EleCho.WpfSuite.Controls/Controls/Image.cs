using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EleCho.WpfSuite.Controls.SourceGeneration;

namespace EleCho.WpfSuite.Controls
{
    [GenerateCornerRadiusProperty]
    public partial class Image : Control
    {
        static Image()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Image), new FrameworkPropertyMetadata(typeof(Image)));
        }

        protected virtual Uri? BaseUri { get; set; }

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if(Source is { } imageSource)
            {
                var borderThickness = BorderThickness;

                var imageSize = new Size(imageSource.Width, imageSource.Height);

                var imageConstraintWidth = constraint.Width - borderThickness.Left - borderThickness.Right;
                var imageConstraintHeight = constraint.Height - borderThickness.Top - borderThickness.Bottom;
                if(imageConstraintWidth < 0 || imageConstraintHeight < 0)
                {
                    return constraint;
                }

                var imageConstraint = new Size(imageConstraintWidth, imageConstraintHeight);
                var factor = imageSize.Width / imageSize.Height;

                if (imageConstraint.Width < imageSize.Width)
                {
                    imageSize.Width = imageConstraint.Width;
                    imageSize.Height = imageConstraint.Width / factor;
                }

                if (imageConstraint.Height < imageSize.Height)
                {
                    imageSize.Height = imageConstraint.Height;
                    imageSize.Width = imageConstraint.Height * factor;
                }

                var finalSize = new Size(
                    imageSize.Width + borderThickness.Left + borderThickness.Right,
                    imageSize.Height + borderThickness.Top + borderThickness.Bottom);

                return finalSize;
            }

            return base.MeasureOverride(constraint);
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(Image), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(Image), new FrameworkPropertyMetadata(Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsRender));

    }
}
