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
using EleCho.WpfSuite.Markup;
using WpfTest.Models;

namespace WpfTest.Tests
{
    [ObservableObject]
    public partial class MasonryTestPage : Page
    {
        private readonly Random _random = new();

        public MasonryTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<MasonryItem> MasonryItems { get; } = new();

        [RelayCommand]
        public Task AddItem()
        {
            var h = (float)_random.NextDouble();
            var s = 1f;
            var v = 0.7f;
            HsvColorExtension.HSV2RGB(h, s, v, out var r, out var g, out var b);

            var newItem = new MasonryItem(new SolidColorBrush(Color.FromScRgb(1, r, g, b)), _random.Next(30, 80));
            MasonryItems.Add(newItem);

            return Task.CompletedTask;
        }

        [RelayCommand]
        public async Task RemoveItem()
        {
            var last = MasonryItems.LastOrDefault();

            if (last is not null)
            {
                await ItemsControlUtils.TransitioningRemoveAsync(MasonryItemsControl, last);
            }
        }
    }
}
