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

namespace SmoothScrollTest;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string[] SomeCollection { get; } = Enumerable.Range(0, 300)
        .Select(i => $"Item {i}")
        .ToArray();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        foreach (var _ in Enumerable.Range(0, 80))
        {
            panel.Children.Add(new TextBlock()
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                       "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                       "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                       "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                       "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10)
            });
            panel.Children.Add(new Border()
            {
                Background = Brushes.SkyBlue,
                Height = 64,
                Margin = new Thickness(10)
            });
        }

        testListBox.ItemsSource = SomeCollection;
        testListBox2.ItemsSource = SomeCollection;
    }

    private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {

    }
}