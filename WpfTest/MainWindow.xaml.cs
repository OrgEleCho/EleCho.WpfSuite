using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTest.Models;
using WpfTest.Tests;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<NavigationItem> NavigationItems { get; } = new()
        {
            new NavigationItem()
            {
                Title = "Collection test",
                Description = "",
                PageType = typeof(CollectionTestPage)
            },
            new NavigationItem()
            {
                Title = "Image test",
                Description = "",
                PageType = typeof(ImageTestPage)
            },
            new NavigationItem()
            {
                Title = "TextBox test",
                Description = "",
                PageType = typeof(TextBoxTestPage)
            },
            new NavigationItem()
            {
                Title = "Temp test",
                Description = "",
                PageType = typeof(TempPage)
            },
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppNavigations.SelectedItem = NavigationItems.FirstOrDefault();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox listBox ||
                listBox.SelectedItem is not NavigationItem navigationItem)
                return;

            var page = Activator.CreateInstance(navigationItem.PageType);
            AppFrame.Navigate(page);
        }

        private void AppFrame_Navigated(object sender, NavigationEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                From = 10,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(200)),
                EasingFunction = new CircleEase()
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            AppFrame.RenderTransform.BeginAnimation(TranslateTransform.YProperty, doubleAnimation);
        }
    }
}