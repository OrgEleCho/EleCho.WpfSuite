using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// SlicedImage 控件用于显示可切片的图像。
    /// </summary>
    public class SlicedImage : Control
    {
        static SlicedImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlicedImage), new FrameworkPropertyMetadata(typeof(SlicedImage)));
        }

        /// <summary>
        /// 获取或设置图像源。
        /// </summary>
        public BitmapSource? Source
        {
            get { return (BitmapSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 获取或设置图像边距。
        /// </summary>
        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        /// <summary>
        /// 获取或设置图像的平铺模式。
        /// </summary>
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



        /// <summary>
        /// 获取左上角切片的画刷。
        /// </summary>
        public Brush? LeftTopTileBrush
        {
            get { return (Brush?)GetValue(LeftTopTileBrushProperty); }
        }

        /// <summary>
        /// 获取右上角切片的画刷。
        /// </summary>
        public Brush? RightTopTileBrush
        {
            get { return (Brush?)GetValue(RightTopTileBrushProperty); }
        }

        /// <summary>
        /// 获取左下角切片的画刷。
        /// </summary>
        public Brush? LeftBottomTileBrush
        {
            get { return (Brush?)GetValue(LeftBottomTileBrushProperty); }
        }

        /// <summary>
        /// 获取右下角切片的画刷。
        /// </summary>
        public Brush? RightBottomTileBrush
        {
            get { return (Brush?)GetValue(RightBottomTileBrushProperty); }
        }

        /// <summary>
        /// 获取左侧切片的画刷。
        /// </summary>
        public Brush? LeftTileBrush
        {
            get { return (Brush?)GetValue(LeftTileBrushProperty); }
        }

        /// <summary>
        /// 获取右侧切片的画刷。
        /// </summary>
        public Brush? RightTileBrush
        {
            get { return (Brush?)GetValue(RightTileBrushProperty); }
        }

        /// <summary>
        /// 获取顶部切片的画刷。
        /// </summary>
        public Brush? TopTileBrush
        {
            get { return (Brush?)GetValue(TopTileBrushProperty); }
        }

        /// <summary>
        /// 获取底部切片的画刷。
        /// </summary>
        public Brush? BottomTileBrush
        {
            get { return (Brush?)GetValue(BottomTileBrushProperty); }
        }

        /// <summary>
        /// 获取中心区域的画刷。
        /// </summary>
        public Brush? CenterTileBrush
        {
            get { return (Brush?)GetValue(CenterTileBrushProperty); }
        }


        /// <summary>
        /// 获取左侧图像边距。
        /// </summary>
        public GridLength ImageMarginLeft
        {
            get { return (GridLength)GetValue(ImageMarginLeftProperty); }
        }

        /// <summary>
        /// 获取右侧图像边距。
        /// </summary>
        public GridLength ImageMarginRight
        {
            get { return (GridLength)GetValue(ImageMarginRightProperty); }
        }

        /// <summary>
        /// 获取顶部图像边距。
        /// </summary>
        public GridLength ImageMarginTop
        {
            get { return (GridLength)GetValue(ImageMarginTopProperty); }
        }

        /// <summary>
        /// 获取底部图像边距。
        /// </summary>
        public GridLength ImageMarginBottom
        {
            get { return (GridLength)GetValue(ImageMarginBottomProperty); }
        }


        /// <inheritdoc/>
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
                var imageCenterTileWidth = (int)(imageSource.PixelWidth - (int)imageMargin.Left - (int)imageMargin.Right);
                var imageCenterTileHeight = (int)(imageSource.PixelHeight - (int)imageMargin.Top - (int)imageMargin.Bottom);

                SetValue(ImageMarginLeftPropertyKey, new GridLength(imageMarginLeft));
                SetValue(ImageMarginRightPropertyKey, new GridLength(imageMarginRight));
                SetValue(ImageMarginTopPropertyKey, new GridLength(imageMarginTop));
                SetValue(ImageMarginBottomPropertyKey, new GridLength(imageMarginBottom));

                CroppedBitmap leftTopImage = new CroppedBitmap(imageSource, new Int32Rect(0, 0, imageMarginLeft, imageMarginTop));
                CroppedBitmap topImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, 0, imageCenterTileWidth, imageMarginTop));
                CroppedBitmap rightTopImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, 0, imageMarginRight, imageMarginTop));

                CroppedBitmap leftImage = new CroppedBitmap(imageSource, new Int32Rect(0, imageMarginTop, imageMarginLeft, imageCenterTileHeight));
                CroppedBitmap centerImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, imageMarginTop, imageCenterTileWidth, imageCenterTileHeight));
                CroppedBitmap rightImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, imageMarginTop, imageMarginRight, imageCenterTileHeight));

                CroppedBitmap leftBottomImage = new CroppedBitmap(imageSource, new Int32Rect(0, imageMarginTop + imageCenterTileHeight, imageMarginLeft, imageMarginBottom));
                CroppedBitmap bottomImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft, imageMarginTop + imageCenterTileHeight, imageCenterTileWidth, imageMarginBottom));
                CroppedBitmap rightBottomImage = new CroppedBitmap(imageSource, new Int32Rect(imageMarginLeft + imageCenterTileWidth, imageMarginTop + imageCenterTileHeight, imageMarginRight, imageMarginBottom));

                SetValue(LeftTopTileBrushPropertyKey, new ImageBrush(leftTopImage) { TileMode = tileMode });
                SetValue(TopTileBrushPropertyKey, new ImageBrush(topImage) { TileMode = tileMode });
                SetValue(RightTopTileBrushPropertyKey, new ImageBrush(rightTopImage) { TileMode = tileMode });

                SetValue(LeftTileBrushPropertyKey, new ImageBrush(leftImage) { TileMode = tileMode });
                SetValue(CenterTileBrushPropertyKey, new ImageBrush(centerImage) { TileMode = tileMode });
                SetValue(RightTileBrushPropertyKey, new ImageBrush(rightImage) { TileMode = tileMode });

                SetValue(LeftBottomTileBrushPropertyKey, new ImageBrush(leftBottomImage) { TileMode = tileMode });
                SetValue(BottomTileBrushPropertyKey, new ImageBrush(bottomImage) { TileMode = tileMode });
                SetValue(RightBottomTileBrushPropertyKey, new ImageBrush(rightBottomImage) { TileMode = tileMode });

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

        /// <summary>
        /// Identifies the <see cref="Source"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(BitmapSource), typeof(SlicedImage), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the <see cref="ImageMargin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Identifies the <see cref="TileMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TileModeProperty =
            DependencyProperty.Register(nameof(TileMode), typeof(TileMode), typeof(SlicedImage), new FrameworkPropertyMetadata(TileMode.Tile));

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(SlicedImage));


        /// <summary>
        /// Identifies the read-only <see cref="LeftTopTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey LeftTopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftTopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="RightTopTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey RightTopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightTopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="LeftBottomTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey LeftBottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftBottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="RightBottomTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey RightBottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightBottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="LeftTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey LeftTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LeftTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="RightTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey RightTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(RightTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="TopTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey TopTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TopTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="BottomTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey BottomTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(BottomTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="CenterTileBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey CenterTileBrushPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CenterTileBrush), typeof(Brush), typeof(SlicedImage), new FrameworkPropertyMetadata(default(Brush)));

        /// <summary>
        /// Identifies the read-only <see cref="ImageMarginLeft"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey ImageMarginLeftPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginLeft), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        /// <summary>
        /// Identifies the read-only <see cref="ImageMarginRight"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey ImageMarginRightPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginRight), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        /// <summary>
        /// Identifies the read-only <see cref="ImageMarginTop"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey ImageMarginTopPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginTop), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        /// <summary>
        /// Identifies the read-only <see cref="ImageMarginBottom"/> dependency property.
        /// </summary>
        public static readonly DependencyPropertyKey ImageMarginBottomPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ImageMarginBottom), typeof(GridLength), typeof(SlicedImage), new FrameworkPropertyMetadata(GridLength.Auto));

        /// <summary>
        /// 获取左上角切片的画刷。
        /// </summary>
        public static readonly DependencyProperty LeftTopTileBrushProperty = LeftTopTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取右上角切片的画刷。
        /// </summary>
        public static readonly DependencyProperty RightTopTileBrushProperty = RightTopTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取左下角切片的画刷。
        /// </summary>
        public static readonly DependencyProperty LeftBottomTileBrushProperty = LeftBottomTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取右下角切片的画刷。
        /// </summary>
        public static readonly DependencyProperty RightBottomTileBrushProperty = RightBottomTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取左侧切片的画刷。
        /// </summary>
        public static readonly DependencyProperty LeftTileBrushProperty = LeftTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取右侧切片的画刷。
        /// </summary>
        public static readonly DependencyProperty RightTileBrushProperty = RightTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取顶部切片的画刷。
        /// </summary>
        public static readonly DependencyProperty TopTileBrushProperty = TopTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取底部切片的画刷。
        /// </summary>
        public static readonly DependencyProperty BottomTileBrushProperty = BottomTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取中心区域的画刷。
        /// </summary>
        public static readonly DependencyProperty CenterTileBrushProperty = CenterTileBrushPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取左侧图像边距。
        /// </summary>
        public static readonly DependencyProperty ImageMarginLeftProperty = ImageMarginLeftPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取右侧图像边距。
        /// </summary>
        public static readonly DependencyProperty ImageMarginRightProperty = ImageMarginRightPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取顶部图像边距。
        /// </summary>
        public static readonly DependencyProperty ImageMarginTopProperty = ImageMarginTopPropertyKey.DependencyProperty;

        /// <summary>
        /// 获取底部图像边距。
        /// </summary>
        public static readonly DependencyProperty ImageMarginBottomProperty = ImageMarginBottomPropertyKey.DependencyProperty;
    }
}
