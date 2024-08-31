using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using EleCho.WpfSuite;
using EleCho.WpfSuite.FluentDesign;

namespace WpfTest.Tests
{
    [ObservableObject]
    public partial class TempPage : Page
    {
        public TempPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        [ObservableProperty]
        private Brush? _currentBrush;

        partial void OnCurrentBrushChanged(Brush? value)
        {
            Debug.WriteLine(value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            buttonContentControl.Content = System.IO.Path.GetRandomFileName();
        }

        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            var resources = App.Current.Resources.MergedDictionaries
                .OfType<FluentResources>()
                .FirstOrDefault();
            var window = Window.GetWindow(this);

            if (resources is not null &&
                window is not null)
            {
                var theme = resources.ActualTheme;
                theme = theme switch
                {
                    ApplicationTheme.Light => ApplicationTheme.Dark,
                    ApplicationTheme.Dark => ApplicationTheme.Light,
                    _ => ApplicationTheme.Light
                };

                ApplicationThemeUtilities.SetApplicationTheme(theme);
            }
        }
    }
}
