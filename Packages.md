# Package Notes

EleCho.WpfSuite 分为三种包: 基础包, 整合包, 拓展包

所有的基础功能被拆分为多个基础包, 例如 .Input, .Markup, .Layouts, .Controls 等.
整合包是一个空包, 依赖所有基础包, 可以方便用户安装以获取所有基础功能.
拓展包则是基于整合包的拓展功能包, 例如 .FluentDesign