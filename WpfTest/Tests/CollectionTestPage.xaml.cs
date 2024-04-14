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

namespace WpfTest.Tests
{
    [ObservableObject]
    public partial class CollectionTestPage : Page
    {
        public CollectionTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        [ObservableProperty]
        private List<string> _stringArray = new List<string>();

        [RelayCommand]
        public void AddItem()
        {
            StringArray = StringArray
                .Append(System.IO.Path.GetRandomFileName())
                .ToList();
        }

        [RelayCommand]
        public void RemoveItem()
        {
            if (StringArray.Count == 0)
                return;

            StringArray = StringArray
                .Take(StringArray.Count - 1)
                .ToList();
        }
    }
}
