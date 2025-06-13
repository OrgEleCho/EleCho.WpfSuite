using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlurBehindTest;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private Point _startElementPosition;
    private Point _startMousePosition;

    private void SiblingPresenter_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not UIElement blurArea)
        {
            return;
        }

        blurArea.CaptureMouse();

        if (blurArea.IsMouseCaptured)
        {
            _startElementPosition = new Point(Canvas.GetLeft(blurArea), Canvas.GetTop(blurArea));
            _startMousePosition = Mouse.GetPosition(null);
        }
    }

    private void SiblingPresenter_MouseMove(object sender, MouseEventArgs e)
    {
        if (sender is not UIElement blurArea)
        {
            return;
        }

        if (blurArea.IsMouseCaptured)
        {
            var nowPosition = Mouse.GetPosition(null);
            var offset = nowPosition - _startMousePosition;

            Canvas.SetLeft(blurArea, _startElementPosition.X + offset.X);
            Canvas.SetTop(blurArea, _startElementPosition.Y + offset.Y);
        }
    }

    private void SiblingPresenter_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is not UIElement blurArea)
        {
            return;
        }

        blurArea.ReleaseMouseCapture();
    }
}