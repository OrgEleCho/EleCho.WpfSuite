using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfTest.Models;

namespace WpfTest.Tests
{
    [ObservableObject]
    public partial class MasonryTestPage : Page
    {
        public MasonryTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<MasonryItem> MasonryItems { get; } = new();

        [RelayCommand]
        public void AddItem()
        {
            var h = Random.Shared.NextSingle();
            var s = 1f;
            var v = 0.7f;
            HsvColor.HSV2RGB(h, s, v, out var r, out var g, out var b);

            MasonryItems.Add(new MasonryItem(new SolidColorBrush(Color.FromScRgb(1, r, g, b)), Random.Shared.Next(30, 80)));
        }
    }
}
