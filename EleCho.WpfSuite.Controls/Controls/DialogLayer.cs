using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    public class DialogLayer : System.Windows.Controls.ContentControl
    {
        private readonly List<Dialog> _dialogStack = new();

        static DialogLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogLayer), new FrameworkPropertyMetadata(typeof(DialogLayer)));
        }



        public Brush Mask
        {
            get { return (Brush)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        public IContentTransition? MaskTransition
        {
            get { return (IContentTransition)GetValue(MaskTransitionProperty); }
            set { SetValue(MaskTransitionProperty, value); }
        }

        public IContentTransition? DialogTransition
        {
            get { return (IContentTransition)GetValue(DialogTransitionProperty); }
            set { SetValue(DialogTransitionProperty, value); }
        }

        public Dialog? ShowingDialog => (Dialog)GetValue(ShowingDialogProperty);

        public bool IsShowingDialog => (bool)GetValue(IsShowingDialogProperty);


        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register(nameof(Mask), typeof(Brush), typeof(DialogLayer), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty MaskTransitionProperty =
            DependencyProperty.Register(nameof(MaskTransition), typeof(IContentTransition), typeof(DialogLayer), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DialogTransitionProperty =
            DependencyProperty.Register(nameof(DialogTransition), typeof(IContentTransition), typeof(DialogLayer), new FrameworkPropertyMetadata(null));


        public static readonly DependencyPropertyKey ShowingDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ShowingDialog), typeof(Dialog), typeof(DialogLayer), new FrameworkPropertyMetadata(null));

        public static readonly DependencyPropertyKey IsShowingDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsShowingDialog), typeof(bool), typeof(DialogLayer), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty ShowingDialogProperty = ShowingDialogPropertyKey.DependencyProperty;
        public static readonly DependencyProperty IsShowingDialogProperty = IsShowingDialogPropertyKey.DependencyProperty;



        public int DialogCount => _dialogStack.Count;

        public void Push(Dialog dialog)
        {
            _dialogStack.Add(dialog);

            dialog.IsOpen = true;
            SetValue(ShowingDialogPropertyKey, dialog);
            SetValue(IsShowingDialogPropertyKey, true);
        }

        public void Remove(Dialog dialog)
        {
            bool removed = _dialogStack.Remove(dialog);

            if (!removed)
            {
                return;
            }

            dialog.IsOpen = false;
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

        public void Pop()
        {
            if (_dialogStack.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var topIndex = _dialogStack.Count - 1;
            var topDialog = _dialogStack[topIndex];
            _dialogStack.RemoveAt(topIndex);

            topDialog.IsOpen = false;
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
    }
}
