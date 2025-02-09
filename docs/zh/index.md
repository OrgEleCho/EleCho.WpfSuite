---
title: 中文文档
layout: home
nav_order: 999
permalink: /zh
has_children: true
---

# 增强你的 WPF 开发
{: .fs-9 }

WPF Suite 为 WPF 拓展了一些基础但必备的功能, 能够极大的提升你的开发体验, 而且能够直接集成到任何项目中.
{: .fs-6 .fw-300 }


[快速开始](#快速开始){: .btn .btn-primary .fs-5 .mb-4 .mb-md-0 .mr-2 }
[在 GitHub 上查看](https://github.com/OrgEleCho/EleCho.WpfSuite){: .btn .fs-5 .mb-4 .mb-md-0 }

---

WPF Suite 是一个包含布局面板, 增强控件, 值转换器, 标记拓展, 过渡以及实用工具的套件包.

使用增强的布局面板, 你可以非常方便的使用 "Spacing" 属性来指定项之间的间距.
WPF Suite 还提供了两个额外的布局面板, FlexPanel 与  MasonryPanel.
FlexPanel 是 Web 上 flex 布局的一个实现, MasonryPanel 则是瀑布流布局的一个简单实现.

增强的空间拓展了许多原来没有的属性, 例如 Button 的 CornerRadius, TextBox 的 Placeholder.
为 Button 设置 "PressedBackground" 可以直接在不需要修改按钮模板的情况下, 指定按钮在鼠标按下时的背景颜色.
另外, ScrollViewer 现在也有平滑滚动的效果, 而且通过触摸板滚动的体验也有所优化.

对于笔设备, WPF Suite 也有进行优化, 现在你可以通过拖拽 ScrollViewer 的内容来滚动它了

WPF Suite 中还有许多值得探索的东西, 例如内容切换与页面跳转的过渡动画, 允许设置模糊背景或亮暗色切换的窗体选项, 用于减少逻辑代码的值转换器等.

查看文档来学习如何使用本库.


## 快速开始

你可以直接通过 nuget 包管理器为你的项目安装 EleCho.WpfSuite 包.

在安装之后, 无须导入任何资源, 你所需要做的只是引入命名空间, 并使用.

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite">

</Window>
```

## 示例程序

下面是来自 WPF Suite 示例程序的截图, 要下载, 请到 [GitHub 的最新 Release](https://github.com/OrgEleCho/EleCho.WpfSuite/releases/latest)

<div class=".d-flex">

<img src="/images/app1.png" height="300">

</div>

## 关于项目

EleCho.WpfSuite is &copy; 2024-{{ "now" | date: "%Y" }} by [EleCho](https://github.com/OrgEleCho).

### 许可证

EleCho.WpfSuite 通过 [MIT license](https://github.com/OrgEleCho/EleCho.WpfSuite/blob/master/LICENSE.txt) 分发.