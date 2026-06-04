using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EleCho.WpfSuite.Media.Transition;
using EleCho.WpfSuite.Properties;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// 提供对话框功能的层，支持多个对话框的推送和弹出。
    /// </summary>
    [TemplatePart(Name = "TempDialogs", Type = typeof(Panel))]
    public class DialogLayer : System.Windows.Controls.ContentControl
    {
        private readonly List<Dialog> _dialogStack = new();

        static DialogLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogLayer), new FrameworkPropertyMetadata(typeof(DialogLayer)));
        }

        private Panel? _tempDialogs;

        private Panel? TempDialogs => _tempDialogs ??= GetTemplateChild("TempDialogs") as Panel;

        /// <summary>
        /// 获取或设置遮罩的画刷。
        /// </summary>
        public Brush Mask
        {
            get { return (Brush)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        /// <summary>
        /// 获取或设置遮罩的过渡效果。
        /// </summary>
        public IContentTransition? MaskTransition
        {
            get { return (IContentTransition)GetValue(MaskTransitionProperty); }
            set { SetValue(MaskTransitionProperty, value); }
        }

        /// <summary>
        /// 获取或设置对话框的过渡效果。
        /// </summary>
        public IContentTransition? DialogTransition
        {
            get { return (IContentTransition)GetValue(DialogTransitionProperty); }
            set { SetValue(DialogTransitionProperty, value); }
        }

        /// <summary>
        /// 获取当前显示的对话框。
        /// </summary>
        public Dialog? ShowingDialog => (Dialog)GetValue(ShowingDialogProperty);

        /// <summary>
        /// 获取一个值，指示是否显示了对话框。
        /// </summary>
        public bool IsShowingDialog => (bool)GetValue(IsShowingDialogProperty);

        /// <summary>
        /// 注册了对 <see cref="Mask"/> 的依赖于属性的字段。
        /// </summary>
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register(nameof(Mask), typeof(Brush), typeof(DialogLayer), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 注册了对 <see cref="MaskTransition"/> 的依赖于属性的字段。
        /// </summary>
        public static readonly DependencyProperty MaskTransitionProperty =
            DependencyProperty.Register(nameof(MaskTransition), typeof(IContentTransition), typeof(DialogLayer), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 注册了对 <see cref="DialogTransition"/> 的依赖于属性的字段。
        /// </summary>
        public static readonly DependencyProperty DialogTransitionProperty =
            DependencyProperty.Register(nameof(DialogTransition), typeof(IContentTransition), typeof(DialogLayer), new FrameworkPropertyMetadata(null));


        /// <summary>
        /// 注册了对 <see cref="ShowingDialog"/> 的依赖于只读属性的字段。
        /// </summary>
        public static readonly DependencyPropertyKey ShowingDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ShowingDialog), typeof(Dialog), typeof(DialogLayer), new FrameworkPropertyMetadata(null, propertyChangedCallback: OnShowingDialogChanged));

        /// <summary>
        /// 注册了对 <see cref="IsShowingDialog"/> 的依赖于只读属性的字段。
        /// </summary>
        public static readonly DependencyPropertyKey IsShowingDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsShowingDialog), typeof(bool), typeof(DialogLayer), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// 获取表示对话框层中当前显示的对话框的属性。
        /// </summary>
        public static readonly DependencyProperty ShowingDialogProperty = ShowingDialogPropertyKey.DependencyProperty;
        /// <summary>
        /// 获取一个值，指示对话框层中是否显示了对话框的属性。
        /// </summary>
        public static readonly DependencyProperty IsShowingDialogProperty = IsShowingDialogPropertyKey.DependencyProperty;
        /// <summary>
        /// 获取当前堆栈中对话框的数量。
        /// </summary>
        public int DialogCount => _dialogStack.Count;

        /// <summary>
        /// 将对话框推入堆栈并显示。
        /// </summary>
        /// <param name="dialog">要推入的对话框。</param>
        /// <exception cref="InvalidOperationException">未能找到模板中的临时对话框容器。</exception>
        public void Push(Dialog dialog)
        {
            _dialogStack.Add(dialog);

            if (dialog.Parent is null)
            {
                if (TempDialogs is not Panel tempDialogs)
                {
                    throw new InvalidOperationException(StringResources.CanNotFindTemplateChildTempDialogs);
                }

                tempDialogs.Children.Add(dialog);
            }

            dialog.IsOpen = true;
            SetValue(ShowingDialogPropertyKey, dialog);
            SetValue(IsShowingDialogPropertyKey, true);
        }

        /// <summary>
        /// 从堆栈中移除对话框。
        /// </summary>
        /// <param name="dialog">要移除的对话框。</param>
        public void Remove(Dialog dialog)
        {
            bool removed = _dialogStack.Remove(dialog);

            if (!removed)
            {
                return;
            }

            dialog.IsOpen = false;

            if (TempDialogs is Panel tempDialogs &&
                dialog.Parent == tempDialogs)
            {
                tempDialogs.Children.Remove(dialog);
            }

            if (_dialogStack.Count > 0)
            {
                SetValue(ShowingDialogPropertyKey, _dialogStack[_dialogStack.Count - 1]);
                SetValue(IsShowingDialogPropertyKey, true);
            }
            else
            {
                SetValue(IsShowingDialogPropertyKey, false);
                SetValue(ShowingDialogPropertyKey, null);
            }
        }

        /// <summary>
        /// 弹出堆栈顶部的对话框。
        /// </summary>
        /// <exception cref="InvalidOperationException">当前没有对话框可供弹出。</exception>
        public void Pop()
        {
            if (_dialogStack.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var topIndex = _dialogStack.Count - 1;
            var topDialog = _dialogStack[topIndex];
            _dialogStack.RemoveAt(topIndex);

            // this will call remove method
            topDialog.IsOpen = false;
        }


        private static DialogLayer? FindDialogLayerFromChildren(DependencyObject dependencyObject)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                if (child is DialogLayer layer)
                {
                    return layer;
                }
            }

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                if (FindDialogLayerFromChildren(child) is DialogLayer layerInChild)
                {
                    return layerInChild;
                }
            }

            return null;
        }

        /// <summary>
        /// 在视觉树中查找并返回 <see cref="DialogLayer"/> 的实例。
        /// </summary>
        /// <param name="dependencyObject">起始查找的依赖对象。</param>
        /// <returns> 找到的 <see cref="DialogLayer"/> 实例，或者如果未找到则为 null。</returns>
        public static DialogLayer? GetDialogLayer(DependencyObject dependencyObject)
        {
            while (true)
            {
                var parent = VisualTreeHelper.GetParent(dependencyObject);

                if (parent is DialogLayer layer)
                {
                    return layer;
                }

                if (parent is null)
                {
                    break;
                }

                dependencyObject = parent;
            }

            return FindDialogLayerFromChildren(dependencyObject);
        }

        private static void OnShowingDialogChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not DialogLayer dialogLayer)
            {
                return;
            }

            dialogLayer.InputBindings.Clear();
            if (e.NewValue is Dialog dialog)
            {
                foreach (InputBinding inputBinding in dialog.InputBindings)
                {
                    dialogLayer.InputBindings.Add(new InputBinding(inputBinding.Command, inputBinding.Gesture)
                    {
                        CommandParameter = inputBinding.CommandParameter,
                        CommandTarget = inputBinding.CommandTarget,
                    });
                }
            }
        }
    }
}
