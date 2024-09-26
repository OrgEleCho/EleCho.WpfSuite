// Origin code from: https://github.com/walterlv/Walterlv.Packages/blob/master/src/Themes/Walterlv.Themes.FluentDesign/Controls/ClientAreaBorder.cs
// Changes:
//   - Inherit from Decorator instead of Border
//   - Make 'Padding' property read-only
//   - Remove cache of properties

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using EleCho.WpfSuite.Helpers;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// If you're using <see cref="WindowChrome"/> to extend the interface to non-client area, then you can add this container to the content of <see cref="Window"/> in order to have the internal content automatically populate the client area section.
    /// Using this container eliminates the need for the various margin adaptations done by Setter/Trigger inside the <see cref="Window"/> style when the window state changes.
    /// </summary>
    public class WindowContentAdapter : Decorator
    {
#pragma warning disable IDE1006 // 命名样式
#pragma warning disable IDE0052 // 删除未读的私有成员
        private const int SM_CXFRAME = 32;
        private const int SM_CYFRAME = 33;
        private const int SM_CXPADDEDBORDER = 92;
#pragma warning restore IDE0052 // 删除未读的私有成员
#pragma warning restore IDE1006 // 命名样式

        [DllImport("User32", ExactSpelling = true)]
        private static extern int GetSystemMetrics(int nIndex);

        private Window? _nowWindow;

        /// <summary>
        /// Padding of the client area.
        /// </summary>
        public Thickness Padding => (Thickness)GetValue(PaddingPropertyKey.DependencyProperty);

        private static readonly DependencyPropertyKey PaddingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Padding), typeof(Thickness), typeof(WindowContentAdapter),
                new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// DependencyProperty of <see cref="Padding"/>.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty = PaddingPropertyKey.DependencyProperty;

        /// <inheritdoc />
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            if (_nowWindow is { } oldWindow)
            {
                oldWindow.StateChanged -= Window_StateChanged;
            }

            var newWindow = (Window?)Window.GetWindow(this);
            if (newWindow is not null)
            {
                newWindow.StateChanged -= Window_StateChanged;
                newWindow.StateChanged += Window_StateChanged;
            }

            _nowWindow = newWindow;

            UpdatePadding(_nowWindow);
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size constraint)
        {
            var padding = Padding;

            if (Child is { } child)
            {
                child.Measure(new Size(
                    constraint.Width - padding.Left - padding.Right,
                    constraint.Height - padding.Top - padding.Bottom));

                var finalSize = new Size(
                    child.DesiredSize.Width + padding.Left + padding.Right,
                    child.DesiredSize.Height + padding.Top + padding.Bottom);

                finalSize.Width = Math.Min(finalSize.Width, constraint.Width);
                finalSize.Height = Math.Min(finalSize.Height, constraint.Height);

                return finalSize;
            }
            else
            {
                var finalSize = new Size(padding.Left + padding.Right, padding.Top + padding.Bottom);

                finalSize.Width = Math.Min(finalSize.Width, constraint.Width);
                finalSize.Height = Math.Min(finalSize.Height, constraint.Height);

                return finalSize;
            }
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var padding = Padding;
            if (Child is { } child)
            {
                child.Arrange(new Rect(
                    padding.Left,
                    padding.Top,
                    arrangeSize.Width - padding.Left - padding.Right,
                    arrangeSize.Height - padding.Top - padding.Bottom));
            }
            return arrangeSize;
        }

        private void UpdatePadding(Window? window)
        {
            if (window is null ||
                WindowChrome.GetWindowChrome(window) is null)
            {
                SetValue(PaddingPropertyKey, default(Thickness));
                return;
            }

            var padding = window.WindowState switch
            {
                WindowState.Maximized => WindowChromeNonClientFrameThickness,
                _ => default,
            };

            SetValue(PaddingPropertyKey, padding);
        }

        private void Window_StateChanged(object? sender, EventArgs e)
        {
            if (sender is not Window window)
            {
                return;
            }

            UpdatePadding(window);
        }

        /// <summary>
        /// 获取系统的 <see cref="SM_CXPADDEDBORDER"/> 作为 WPF 单位的边框数值。
        /// </summary>
        private Thickness PaddedBorderThickness
        {
            get
            {
                var paddedBorder = GetSystemMetrics(SM_CXPADDEDBORDER);
                var dpi = GetDpi();
                var frameSize = new Size(paddedBorder, paddedBorder);
                var frameSizeInDips = new Size(frameSize.Width / dpi.FactorX, frameSize.Height / dpi.FactorY);
                return new Thickness(frameSizeInDips.Width, frameSizeInDips.Height, frameSizeInDips.Width, frameSizeInDips.Height);
            }
        }

        /// <summary>
        /// 获取系统的 <see cref="SM_CXFRAME"/> 和 <see cref="SM_CYFRAME"/> 作为 WPF 单位的边框数值。
        /// </summary>
        private Thickness ResizeFrameBorderThickness => new Thickness(
            SystemParameters.ResizeFrameVerticalBorderWidth,
            SystemParameters.ResizeFrameHorizontalBorderHeight,
            SystemParameters.ResizeFrameVerticalBorderWidth,
            SystemParameters.ResizeFrameHorizontalBorderHeight);

        /// <summary>
        /// 如果使用了 <see cref="WindowChrome"/> 来制作窗口样式以将窗口客户区覆盖到非客户区，那么就需要自己来处理窗口最大化后非客户区的边缘被裁切的问题。
        /// 使用此属性获取窗口最大化时窗口样式应该内缩的边距数值，这样在窗口最大化时客户区便可以在任何 DPI 下不差任何一个像素地完全覆盖屏幕工作区。
        /// <see cref="GetSystemMetrics"/> 方法无法直接获得这个数值。
        /// </summary>
        private Thickness WindowChromeNonClientFrameThickness => new Thickness(
            ResizeFrameBorderThickness.Left + PaddedBorderThickness.Left,
            ResizeFrameBorderThickness.Top + PaddedBorderThickness.Top,
            ResizeFrameBorderThickness.Right + PaddedBorderThickness.Right,
            ResizeFrameBorderThickness.Bottom + PaddedBorderThickness.Bottom);

        private Dpi GetDpi() => PresentationSource.FromVisual(this) is { } source
            ? new Dpi(
                (int)(Dpi.Standard.X * source.CompositionTarget.TransformToDevice.M11),
                (int)(Dpi.Standard.Y * source.CompositionTarget.TransformToDevice.M22))
            : Dpi.System;
    }
}
