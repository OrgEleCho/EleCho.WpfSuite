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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EleCho.WpfSuite;
using EleCho.WpfSuite.Helpers;

namespace WpfTest.Tests
{
    /// <summary>
    /// WindowCompositionTest.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class WindowCompositionTest : Page
    {
        public WindowCompositionTest()
        {
            DataContext = this;
            InitializeComponent();
        }

        [RelayCommand]
        public void OpenAcrylicWindow()
        {
            new AcrylicTestWindow().Show();
        }

        [RelayCommand]
        public void OpenMicaTestWindow()
        {
            new MicaTestWindow().Show();
        }

        [RelayCommand]
        public void OpenCustomAcrylicWindow()
        {
            new CustomAcrylicTestWindow().Show();
        }

        [RelayCommand]
        public void OpenDarkAcrylicWindow()
        {
            var window = new AcrylicTestWindow();
            WindowOption.SetIsDarkMode(window, true);
            window.Foreground = new SolidColorBrush(Color.FromRgb(214, 214, 214));
            window.Show();
        }

        [RelayCommand]
        public void OpenDarkMicaTestWindow()
        {
            var window = new MicaTestWindow();
            WindowOption.SetIsDarkMode(window, true);
            window.Foreground = new SolidColorBrush(Color.FromRgb(214, 214, 214));
            window.Show();
        }

        [RelayCommand]
        public void OpenDarkCustomAcrylicWindow()
        {
            var window = new CustomAcrylicTestWindow();
            WindowOption.SetIsDarkMode(window, true);
            window.Foreground = new SolidColorBrush(Color.FromRgb(214, 214, 214));
            window.Show();
        }
    }
}
