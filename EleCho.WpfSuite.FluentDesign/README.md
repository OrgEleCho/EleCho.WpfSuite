# EleCho.WpfSuite.FluentDesign

FluentDesign theme for [EleCho.WpfSuite](https://www.nuget.org/packages/EleCho.WpfSuite)

## Usage

Add resource dictionary to App.xaml

```xml
<Application x:Class="WpfTest.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:wsfd="https://schemas.elecho.dev/wpfsuite-design/fluent"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>

                <!-- Add this line -->
                <wsfd:FluentResources/>

            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Set the theme of your application:

```csharp
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Call this method
        ApplicationThemeUtilities.SetApplicationTheme(ApplicationTheme.Auto);

        base.OnStartup(e);
    }
}
```

Ensure theme in every window:

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Call this method
        ApplicationThemeUtilities.EnsureWindowTheme(this);
    }
}
```

Enjoy! Most of WPF Suite controls are in fluent design now!

```xml
<Window x:Class="WpfTest.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfTest"
        xmlns:ws="https://schemas.elecho.dev/wpfsuite"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

    <ws:ScrollViewer Margin="12"
                     ScrollWithWheelDelta="True">
        <ws:StackPanel Spacing="8">
            <ws:TextBox Width="200"
                        Placeholder="This is a text box with placeholder"/>
            <ws:Image Source="/Assets/TestAvatar.jpg"
                      CornerRadius="10"/>
            <ws:Button Content="Some button"/>

            <StackPanel>
                <TextBlock Text="WrapPanel with spacing"/>
                <ItemsControl ItemsSource="{Binding TestItems}">
                    <ItemsControl.ItemsPanel>
                        <ItemsPanelTemplate>
                            <ws:WrapPanel HorizontalSpacing="8"
                                          VerticalSpacing="8"/>
                        </ItemsPanelTemplate>
                    </ItemsControl.ItemsPanel>
                </ItemsControl>
            </StackPanel>
        </ws:StackPanel>
    </ws:ScrollViewer>

</Window>
```