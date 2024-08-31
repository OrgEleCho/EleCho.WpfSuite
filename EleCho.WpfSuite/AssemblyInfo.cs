using System.Runtime.CompilerServices;
using System.Windows.Markup;

[assembly: System.Windows.ThemeInfo(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsDefinition("https://schemas.elecho.dev/wpfsuite", "EleCho.WpfSuite")]
[assembly: XmlnsDefinition("https://schemas.elecho.dev/wpfsuite", "EleCho.WpfSuite.Markup")]
[assembly: XmlnsDefinition("https://schemas.elecho.dev/wpfsuite", "EleCho.WpfSuite.Media.Animation")]

#if DEBUG
[assembly: InternalsVisibleTo("WpfTest")]

#endif