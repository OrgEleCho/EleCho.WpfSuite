using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EleCho.WpfSuite.FluentDesign;
using EleCho.WpfSuite.Helpers;
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

            if (WindowOption.CanSetBackdrop)
            {
                Background = Brushes.Transparent;
                WindowOption.SetBackdrop(this, WindowBackdrop.Mica);
            }

            ApplicationThemeUtilities.EnsureWindowTheme(this);
        }

        [ObservableProperty]
        private bool _appFrameTransitionReverse = false;

        public ObservableCollection<NavigationItem> NavigationItems { get; } = new()
        {
            new NavigationItem()
            {
                Title = "Welcome",
                Description = "",
                PageType = typeof(WelcomePage)
            },
            new NavigationItem()
            {
                Title = "Collection",
                Description = "",
                PageType = typeof(CollectionTestPage)
            },
            new NavigationItem()
            {
                Title = "Panels",
                Description = "",
                PageType = typeof(PanelsTestPage)
            },
            new NavigationItem()
            {
                Title = "Masonry",
                Description = "",
                PageType = typeof(MasonryTestPage)
            },
            new NavigationItem()
            {
                Title = "Scroll",
                Description = "",
                PageType = typeof(ScrollTestPage)
            },
            new NavigationItem()
            {
                Title = "Mica/Acrylic",
                Description = "",
                PageType = typeof(WindowCompositionTest)
            },
            new NavigationItem()
            {
                Title = "Image",
                Description = "",
                PageType = typeof(ImageTestPage)
            },
            new NavigationItem()
            {
                Title = "ConditionalControl",
                Description = "",
                PageType = typeof(ConditionalControlTestPage)
            },
            new NavigationItem()
            {
                Title = "DataTemplateSelector",
                Description = "",
                PageType = typeof(DataSelectorTestPage)
            },
            new NavigationItem()
            {
                Title = "TextBox",
                Description = "",
                PageType = typeof(TextBoxTestPage)
            },
            new NavigationItem()
            {
                Title = "Palette",
                Description = "",
                PageType = typeof(PaletteTestPage)
            },
            new NavigationItem()
            {
                Title = "SystemColors",
                Description = "",
                PageType = typeof(SystemColorsTestPage)
            },
            new NavigationItem()
            {
                Title = "Temp",
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
            //AppFrame.Navigate(page);
            AppFrame.Content = page;
        }

        [RelayCommand]
        public void RemoveTab(NavigationItem navigationItem)
        {
            NavigationItems.Remove(navigationItem);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        [RelayCommand]
        private void MinimumSelf()
        {
            SystemCommands.MinimizeWindow(this);
        }

        [RelayCommand]
        private void MaximumSelf()
        {
            if (WindowState == WindowState.Normal)
                SystemCommands.MaximizeWindow(this);
            else
                SystemCommands.RestoreWindow(this);
        }

        [RelayCommand]
        private void CloseSelf()
        {
            SystemCommands.CloseWindow(this);
        }
    }
}