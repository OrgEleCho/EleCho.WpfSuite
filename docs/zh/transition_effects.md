---
title: 过渡效果
layout: default
nav_order: 4
permalink: /zh/transition-effects
parent: 中文文档
---

# 过渡效果
{: .fs-9 }

当内容切换时, 执行一个过渡效果, 例如淡入淡出, 或者滑入滑出, 能够让程序美观许多.
{: .fs-6 .fw-300 }

---

下面是有关 WPF Suite 中过渡动画的介绍

## IContentTransition

表示一个内容过渡效果的接口

定义
{: .fw-300}

```cs
/// <summary>
/// Content transition
/// </summary>
public interface IContentTransition
{
    /// <summary>
    /// Run the content transition
    /// </summary>
    /// <param name="container">Container UIElement</param>
    /// <param name="oldContent">Old content UIElement</param>
    /// <param name="newContent">New content UIElement</param>
    /// <param name="forward">This transition is forward</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task Run(
        FrameworkElement container, 
        FrameworkElement? oldContent, 
        FrameworkElement? newContent, 
        bool forward, 
        CancellationToken cancellationToken);
}

```

下面是一些内置的过渡效果:

| 类名 | 描述 |
| --- | --- |
| FadeTransition | 淡入淡出效果, 通过对 Opacity 运行动画实现 |
| SlideTransition | 滑动过渡效果, 通过设置 RenderTransform, 对 TranslateTransform 运行动画实现 |
| RotateTransition | 旋转过渡效果, 通过设置 RenderTransform, 对 RotateTransform 运行动画实现 |
| SlideFadeTransition | 淡入淡出与滑动过渡效果的结合 |
| RotateFadeTransition | 淡入淡出与旋转过渡效果的结合 |

## TransitioningContentControl

允许使用过渡动画的 ContentControl 实现. 继承自 Control.

| 属性 | 类型 | 描述 |
| Content | object | 内容, 默认为 null |
| ContentTemplate | DataTemplate | 内容的模板, 默认为 null |
| ContentTemplateSelector | DataTemplateSelector | 内容的模板选择器, 默认为 null |
| Transition | IContentTransition | 内容切换所使用的过渡效果, 默认为 null |
| CornerRadius | CornerRadius | 圆角边缘半径 |

{: .warning }
> TransitioningContentControl 并不继承自 ContentControl

使用示例
{: .fw-300}

```xml
<ws:Button Click="Button_Click"
           ClipToBounds="True">
    <ws:TransitioningContentControl Name="buttonContentControl"
                                    Content="Test">
        <ws:TransitioningContentControl.Transition>
            <ws:SlideFadeTransition Orientation="Vertical"/>
        </ws:TransitioningContentControl.Transition>
    </ws:TransitioningContentControl>
</ws:Button>
```

```cs
private void Button_Click(object sender, RoutedEventArgs e)
{
    buttonContentControl.Content = System.IO.Path.GetRandomFileName();
}
```

![过渡演示1](/images/transition-example1.webp)

更复杂的使用
{: .fw-300}

```xml
<Grid>
    <ws:TransitioningContentControl Content="{Binding CurrentImageSource}"
                                    d:Content="{d:DesignInstance Type=ImageSource}"
                                    CornerRadius="5"
                                    ClipToBounds="True">
        <ws:TransitioningContentControl.Transition>
            <ws:SlideTransition Reverse="{Binding TransitionReverse}"/>
        </ws:TransitioningContentControl.Transition>
        <ws:TransitioningContentControl.ContentTemplate>
            <DataTemplate>
                <Image Height="300"
                       Source="{Binding}"
                       Stretch="UniformToFill"/>
            </DataTemplate>
        </ws:TransitioningContentControl.ContentTemplate>
    </ws:TransitioningContentControl>
    <ws:Button Command="{Binding GoPrevCommand}"
               VerticalAlignment="Center"
               HorizontalAlignment="Left"
               Opacity=".8"
               Content="&lt;"
               Margin="5"
               Padding="5">
    </ws:Button>
    <ws:Button Command="{Binding GoNextCommand}"
               VerticalAlignment="Center"
               HorizontalAlignment="Right"
               Opacity=".8"
               Content="&gt;"
               Margin="5"
               Padding="5"/>
</Grid>
```

```cs
[ObservableProperty]
[NotifyPropertyChangedFor(nameof(CurrentImageSource))]
private int _currentImageSourceIndex;

[ObservableProperty]
private bool _transitionReverse;

public ObservableCollection<ImageSource> ImageSources { get; } = new()
{
    new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/1.jpg")),
    new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/2.jpg")),
    new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/3.jpg")),
    new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/4.jpg")),
    new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/5.jpg")),
};

public ImageSource? CurrentImageSource => ImageSources[CurrentImageSourceIndex];

public WelcomePage()
{
    InitializeComponent();

    DataContext = this;
}

[RelayCommand]
public void GoPrev()
{
    TransitionReverse = true;
    var prevIndex = CurrentImageSourceIndex;
    if (prevIndex == 0)
        prevIndex = ImageSources.Count;

    prevIndex--;

    CurrentImageSourceIndex = prevIndex;
}

[RelayCommand]
public void GoNext()
{
    TransitionReverse = false;
    CurrentImageSourceIndex = (CurrentImageSourceIndex + 1) % ImageSources.Count;
}
```

{: .tip }
> 这里的 ObservableProperty 和 RelayCommand 是使用了 CommunityToolkit.Mvvm 自动生成可观察属性与指令

![过渡演示2](/images/transition-example2.webp)

## Frame

WPF Suite 中提供的 Frame 控件也支持导航, 所以, 当你的程序是一个多页面程序时, 可以用过渡效果来使其更美观

| 属性 | 类型 | 描述 |
| --- | --- | ---- |
| CornerRadius | CornerRadius | 圆角边缘半径 |
| Transition | IContentTransition | 内容切换的过渡效果 |

大致效果
{: .fw-300}

![过渡演示3](/images/transition-example3.webp)