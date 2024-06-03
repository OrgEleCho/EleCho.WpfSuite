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

namespace WpfTest.Tests
{
    /// <summary>
    /// DataSelectorTestPage.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class DataSelectorTestPage : Page
    {
        public DataSelectorTestPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<DataTypeBase> DataList { get; } = new()
        {
            new DataTypeB()
            {
                Name = "This is data type b",
                FieldB = "field b value"
            },
            new DataTypeA()
            {
                Name = "This is data type a",
                FieldA = "field a value"
            },
            new DataTypeBase()
            {
                Name = "This is base type"
            },
            new DataTypeB()
            {
                Name = "This is data type b",
                FieldB = "field b value"
            },
            new DataTypeA()
            {
                Name = "This is data type a",
                FieldA = "field a value"
            },
            new DataTypeB()
            {
                Name = "This is data type b",
                FieldB = "field b value"
            },
        };

    }



    public class DataTypeBase
    {
        public string Name { get; set; } = string.Empty;
    }

    public class DataTypeA : DataTypeBase
    {
        public string FieldA { get; set; } = string.Empty;
    }

    public class DataTypeB : DataTypeBase
    {
        public string FieldB { get; set; } = string.Empty;
    }

}
