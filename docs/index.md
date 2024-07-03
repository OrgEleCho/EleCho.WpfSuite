---
title: Home
layout: home
nav_order: 1
description: 'WPF layout panels, controls, value converters, markup extensions, transitions and utilities.'
permalink: /
---

# Boost your WPF development
{: .fs-9 }

The WPF Suite extends WPF with a number of basic but essential features that can greatly enhance your development experience and can be integrated directly into any project.
{: .fs-6 .fw-300 }


[Get started now](#getting-started){: .btn .btn-primary .fs-5 .mb-4 .mb-md-0 .mr-2 }
[View it on GitHub](https://github.com/OrgEleCho/EleCho.WpfSuite){: .btn .fs-5 .mb-4 .mb-md-0 }

---

WPF Suite is a suite of layout panels, enhanced controls, value converters, markup extensions, transitions and utilities.

With the enhanced layout panel, you can easily add spacing between items using the "Spacing" property. 
The WPF Suite also provides two additional layout panels, FlexPanel and MasonryPanel. 
FlexPanel is an implementation of flex layout for the Web, and MasonryPanel is a simple implementation of waterfall layout.

Enhanced Controls extend some properties that were not present in the original, such as CornerRadius for Button, Placeholder for TextBox.
Setting "PressedBackground" for a Button allows you to set the background of the button when it is pressed directly without changing the button template.
In addition, the ScrollViewer has a special smooth scrolling effect, and the scrolling experience is optimized for touchpad use.

WPF Suite is also optimized for pen devices. Now you can drag and drop the contents of the ScrollViewer with the pen to scroll it!

There is a lot more to explore with the WPF Suite, such as transition animations for content switching and page jumps, form options that allow you to set transparent blurred backgrounds, light/dark color switching, value converters to reduce logic code, and more.

Browse the docs to learn more about how to use this library.


## Getting started

You can install the EleCho.WpfSuite package for your project directly via the nuget package manager.

After installation, there is no need to import any resources, all you need to do is introduce the namespace, and use it.

```xml
<Window ...
        xmlns:ws="https://schemas.elecho.dev/wpfsuite">

</Window>
```

## About the project

EleCho.WpfSuite is &copy; 2024-{{ "now" | date: "%Y" }} by [EleCho](https://github.com/OrgEleCho).

### License

EleCho.WpfSuite is distributed by an [MIT license](https://github.com/just-the-docs/just-the-docs/tree/main/LICENSE.txt).