using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EleCho.WpfSuite
{
    [TemplatePart(Name = "PART_Contents", Type = typeof(Panel))]
    public class TransitioningContentControl : Control
    {
        static TransitioningContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitioningContentControl), new FrameworkPropertyMetadata(typeof(TransitioningContentControl)));
        }

        private Panel? _contentsPanel;
        private UIElement? _lastOldControl;
        private CancellationTokenSource? _lastTaskCancellation;
        private object? _pendingNewContent;
        private bool _backward;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentsPanel = (Panel)GetTemplateChild("PART_Contents");

            if (_pendingNewContent is not null)
            {
                _ = this.ApplyContentChangeAsync(null, _pendingNewContent);
            }
        }

        public object? Content
        {
            get { return (object?)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        public IContentTransition? Transition
        {
            get { return (IContentTransition?)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        

        public void SetContent(object? content)
        {
            Content = content;
        }

        public void SetContent(object? content, bool forward)
        {
            _backward = !forward;
            Content = content;
        }


        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnContentChanged)));

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ContentTemplateSelectorProperty =
            DependencyProperty.Register(nameof(ContentTemplateSelector), typeof(DataTemplateSelector), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.Register(nameof(Transition), typeof(IContentTransition), typeof(TransitioningContentControl), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(TransitioningContentControl));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TransitioningContentControl transitioningContentControl)
            {
                _ = transitioningContentControl.ApplyContentChangeAsync(e.OldValue, e.NewValue);
            }
        }

        private async Task ApplyContentChangeAsync(object? oldContent, object? newContent)
        {
            if (_contentsPanel is null)
            {
                _pendingNewContent = newContent;
                return;
            }

            if (_lastOldControl is not null)
            {
                _contentsPanel.Children.Remove(_lastOldControl);
            }

            if (_lastTaskCancellation is not null)
            {
                _lastTaskCancellation.Cancel();
            }

            UIElement? oldContentElement = null;
            UIElement? newContentElement = null;
            if (_contentsPanel.Children.Count > 0)
            {
                oldContentElement = _contentsPanel.Children[_contentsPanel.Children.Count - 1];
            }

            if(newContent is not null)
            {
                var contentPresenter = new ContentPresenter()
                {
                    Content = newContent
                };

                contentPresenter.SetBinding(ContentPresenter.ContentTemplateProperty,
                    new Binding()
                    {
                        Source = this,
                        Path = new PropertyPath(nameof(ContentTemplate)),
                    });

                contentPresenter.SetBinding(ContentPresenter.ContentTemplateSelectorProperty,
                    new Binding()
                    {
                        Source = this,
                        Path = new PropertyPath(nameof(ContentTemplateSelector)),
                    });

                newContentElement = contentPresenter;
            }
            
            var forward = !_backward;
            _contentsPanel.Children.Add(newContentElement);
            _lastOldControl = oldContentElement;
            _pendingNewContent = null;
            _backward = false;
            if (Transition is IContentTransition transition)
            {
                _lastTaskCancellation = new();
                await transition.Run(this, oldContentElement as FrameworkElement, newContentElement as FrameworkElement, forward, _lastTaskCancellation.Token);
            }

            if (oldContentElement is not null)
            {
                _contentsPanel.Children.Remove(oldContentElement);
            }
        }
    }
}
