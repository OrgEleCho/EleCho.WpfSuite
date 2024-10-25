using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EleCho.WpfSuite.Controls
{
    public class SlicedImage : Control
    {
        static SlicedImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlicedImage), new FrameworkPropertyMetadata(typeof(SlicedImage)));
        }

        public BitmapSource? Source
        {
            get { return (BitmapSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        public TileMode TileMode
        {
            get { return (TileMode)GetValue(TileModeProperty); }
            set { SetValue(TileModeProperty, value); }
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



        public Brush? LeftTopTileBrush
        {
            get { return (Brush?)GetValue(LeftTopTileBrushProperty); }
        }

        public Brush? RightTopTileBrush
        {
            get { return (Brush?)GetValue(RightTopTileBrushProperty); }
        }

        public Brush? LeftBottomTileBrush
        {
            get { return (Brush?)GetValue(LeftBottomTileBrushProperty); }
        }

        public Brush? RightBottomTileBrush
        {
            get { return (Brush?)GetValue(RightBottomTileBrushProperty); }
        }

        public Brush? LeftTileBrush
        {
            get { return (Brush?)GetValue(LeftTileBrushProperty); }
        }

        public Brush? RightTileBrush
        {
            get { return (Brush?)GetValue(RightTileBrushProperty); }
        }

        public Brush? TopTileBrush
        {
            get { return (Brush?)GetValue(TopTileBrushProperty); }
        }

        public Brush? BottomTileBrush
        {
            get { return (Brush?)GetValue(BottomTileBrushProperty); }
        }

        public Brush? CenterTileBrush
        {
            get { return (Brush?)GetValue(CenterTileBrushProperty); }
        }


        public GridLength ImageMarginLeft
        {
            get { return (GridLength)GetValue(ImageMarginLeftProperty); }
        }

        public GridLength ImageMarginRight
        {
            get { return (GridLength)GetValue(ImageMarginRightProperty); }
        }

        public GridLength ImageMarginTop
        {
            get { return (GridLength)GetValue(ImageMarginTopProperty); }
        }

        public GridLength ImageMarginBottom
        {
            get { return (GridLength)GetValue(ImageMarginBottomProperty); }
        }


        protected override Size MeasureOverride(Size constraint)
        {
            if (Source is { } imageSource)
            {
                var tileMode = TileMode;
                var imageMargin = ImageMargin;
                var imageMarginLeft = (int)imageMargin.Left;
                var imageMarginRight = (int)imageMargin.Right;
                var imageMarginTop = (int)imageMargin.Top;
                var imageMarginBottom = (int)imageMargin.Bottom;
                var imageCenterTileWidth = (int)(imageSource.Width - (int)imageMargin.Left - (int)imageMargin.Right);
                var imageCenterTileHeight = (int)(imageSource.Height - (int)imageMargin.Top - (int)imageMargin.Bottom);

                SetValue(ImageMarginLeftPropertyKey, new GridLength(imageMarginLeft));
                SetValue(ImageMarginRightPropertyKey, new GridLength(imageMarginRight));
                SetValue(ImageMarginTopPropertyKey, new GridLength(imageMarginTop));
                SetValue(ImageMarginBottomPropertyKey, new GridLength(imageMarginBottom));

                SetValue(LeftTopTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(0, 0, imageMarginLeft, imageMarginTop))) { TileMode = tileMode });
                SetValue(TopTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, 0, imageCenterTileWidth, imageMarginTop))) { TileMode = tileMode });
                SetValue(RightTopTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, 0, imageMarginRight, imageMarginTop))) { TileMode = tileMode });

                SetValue(LeftTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(0, imageMarginTop, imageMarginLeft, imageCenterTileHeight))) { TileMode = tileMode });
                SetValue(CenterTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, imageMarginTop, imageCenterTileWidth, imageCenterTileHeight))) { TileMode = tileMode });
                SetValue(RightTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, imageMarginTop, imageMarginRight, imageCenterTileHeight))) { TileMode = tileMode });

                SetValue(LeftBottomTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(0, imageMarginTop + imageCenterTileHeight, imageMarginLeft, imageMarginBottom))) { TileMode = tileMode });
                SetValue(BottomTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, imageMarginTop + imageCenterTileHeight, imageCenterTileWidth, imageMarginBottom))) { TileMode = tileMode });
                SetValue(RightBottomTileBrushPropertyKey, new ImageBrush(new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, imageMarginTop + imageCenterTileHeight, imageMarginRight, imageMarginBottom))) { TileMode = tileMode });

                var borderThickness = BorderThickness;

                var imageSize = new Size(imageSource.Width, imageSource.Height);

                var imageConstraintWidth = constraint.Width - borderThickness.Left - borderThickness.Right;
                var imageConstraintHeight = constraint.Height - borderThickness.Top - borderThickness.Bottom;
                if (imageConstraintWidth < 0 || imageConstraintHeight < 0)
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
            else
            {
                SetValue(ImageMarginLeftPropertyKey, GridLength.Auto);
                SetValue(ImageMarginRightPropertyKey, GridLength.Auto);
                SetValue(ImageMarginTopPropertyKey, GridLength.Auto);
                SetValue(ImageMarginBottomPropertyKey, GridLength.Auto);

                SetValue(LeftTopTileBrushPropertyKey, null);
                SetValue(TopTileBrushPropertyKey, null);
                SetValue(RightTopTileBrushPropertyKey, null);

                SetValue(LeftTileBrushPropertyKey, null);
                SetValue(CenterTileBrushPropertyKey, null);
                SetValue(RightTileBrushPropertyKey, null);

                SetValue(LeftBottomTileBrushPropertyKey, null);
                SetValue(BottomTileBrushPropertyKey, null);
                SetValue(RightBottomTileBrushPropertyKey, null);


                return base.MeasureOverride(constraint);
            }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(BitmapSource), typeof(SlicedImage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty TileModeProperty =
            DependencyProperty.Register(nameof(TileMode), typeof(TileMode), typeof(SlicedImage), new FrameworkPropertyMetadata(default));

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(SlicedImage));


        public static readonly DependencyPropertyKey LeftTopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftTopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey RightTopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightTopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey LeftBottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftBottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey RightBottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightBottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey LeftTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey RightTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey TopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey BottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(BottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey CenterTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CenterTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        public static readonly DependencyPropertyKey ImageMarginLeftPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginLeft), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        public static readonly DependencyPropertyKey ImageMarginRightPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginRight), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        public static readonly DependencyPropertyKey ImageMarginTopPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginTop), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        public static readonly DependencyPropertyKey ImageMarginBottomPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginBottom), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        public static readonly DependencyProperty LeftTopTileBrushProperty = LeftTopTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty RightTopTileBrushProperty = RightTopTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty LeftBottomTileBrushProperty = LeftBottomTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty RightBottomTileBrushProperty = RightBottomTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty LeftTileBrushProperty = LeftTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty RightTileBrushProperty = RightTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty TopTileBrushProperty = TopTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty BottomTileBrushProperty = BottomTileBrushPropertyKey.DependencyProperty;
        public static readonly DependencyProperty CenterTileBrushProperty = CenterTileBrushPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ImageMarginLeftProperty = ImageMarginLeftPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ImageMarginRightProperty = ImageMarginRightPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ImageMarginTopProperty = ImageMarginTopPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ImageMarginBottomProperty = ImageMarginBottomPropertyKey.DependencyProperty;
    }
}
