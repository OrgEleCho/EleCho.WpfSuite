using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Xml;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var stringWriter = new StringWriter();
            var xmlWriter = XmlWriter.Create(stringWriter);

            XamlWriter.Save(FindResource(typeof(ToolTip)), xmlWriter);
            var output = stringWriter.ToString();

        }
    }


}
