﻿using System;
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

namespace WpfTest.Tests
{
    /// <summary>
    /// Interaction logic for ScrollTestPage.xaml
    /// </summary>
    public partial class ScrollTestPage : Page
    {
        public ScrollTestPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button");

            new AcrylicTestWindow().Show();
        }
    }
}
