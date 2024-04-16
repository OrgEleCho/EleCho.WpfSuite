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
using CommunityToolkit.Mvvm.ComponentModel;
using WpfTest.Models;
using WpfTest.Tests;

namespace WpfTest
{
    [ObservableObject]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        [ObservableProperty]
        private bool _appFrameTransitionReverse = false;

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
                Title = "Panels test",
                Description = "",
                PageType = typeof(PanelsTestPage)
            },
            new NavigationItem()
            {
                Title = "Image test",
                Description = "",
                PageType = typeof(ImageTestPage)
            },
            new NavigationItem()
            {
                Title = "ConditionalControl test",
                Description = "",
                PageType = typeof(ConditionalControlTestPage)
            },
            new NavigationItem()
            {
                Title = "TextBox test",
                Description = "",
                PageType = typeof(TextBoxTestPage)
            },
            new NavigationItem()
            {
                Title = "Palette test",
                Description = "",
                PageType = typeof(PaletteTestPage)
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

            var newItemIndex = NavigationItems.IndexOf(navigationItem);
            var oldItemIndex = -1;
            if (e.RemovedItems.Count > 0 && e.RemovedItems[0] is NavigationItem oldNavigationItem)
            {
                oldItemIndex = NavigationItems.IndexOf(oldNavigationItem);
            }

            AppFrameTransitionReverse = newItemIndex < oldItemIndex;

            var page = Activator.CreateInstance(navigationItem.PageType);
            AppFrame.Navigate(page);
        }
    }
}