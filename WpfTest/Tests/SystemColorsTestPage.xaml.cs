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
using WpfTest.Models;

namespace WpfTest.Tests
{
    public partial class SystemColorsTestPage : Page
    {
        public SystemColorsTestPage()
        {
            DataContext = this;
            InitializeComponent();

            var typeBrush = typeof(Brush);
            var typeSystemColors = typeof(SystemColors);
            foreach (var property in typeSystemColors.GetProperties())
            {
                if (!typeBrush.IsAssignableFrom(property.PropertyType))
                    continue;

                var brush = property.GetValue(null) as Brush;
                var color = default(Color);
                if (brush is null)
                    continue;

                if (brush is SolidColorBrush solidColorBrush)
                    color = solidColorBrush.Color;

                SystemColorBrushes.Add(new SystemColorBrush(property.Name, brush, color));
            }
        }

        public ObservableCollection<SystemColorBrush> SystemColorBrushes { get; } = new();
    }
}
