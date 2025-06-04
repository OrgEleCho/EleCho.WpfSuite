﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// ContentControl with transition
    /// </summary>
    [TemplatePart(Name = "PART_Contents", Type = typeof(Panel))]
    [ContentProperty(nameof(Content))]
    public class ContentControl : Control
    {
        static ContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContentControl), new FrameworkPropertyMetadata(typeof(ContentControl)));
        }

        private Panel? _contentsPanel;
        private Task? _lastTask;
        private UIElement? _lastOldControl;
        private CancellationTokenSource? _lastTaskCancellation;
        private object? _pendingNewContent;
        private bool _backward;
        private bool _lastTaskIsLoadedTransition;

        /// <summary>
        /// Content of this <see cref="ContentControl"/>
        /// </summary>
        public object? Content
        {
            get { return (object?)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Delay of content setting
        /// </summary>
        public Duration ContentDelay
        {
            get { return (Duration)GetValue(ContentDelayProperty); }
            set { SetValue(ContentDelayProperty, value); }
        }

        /// <summary>
        /// ContentTemplate of this <see cref="ContentControl"/>
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        /// <summary>
        /// ContentTemplateSelector of this <see cref="ContentControl"/>
        /// </summary>
        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Transition of content
        /// </summary>
        public IContentTransition? Transition
        {
            get { return (IContentTransition?)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        /// <summary>
        /// When will the content have a transition
        /// </summary>
        public ContentTransitionMode TransitionMode
        {
            get { return (ContentTransitionMode)GetValue(TransitionModeProperty); }
            set { SetValue(TransitionModeProperty, value); }
        }

        /// <summary>
        /// Corner radius
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public ContentControl()
        {
            Loaded += ContentControlLoaded;
        }

        /// <summary>
        /// Set content of the <see cref="ContentControl"/>
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(object? content)
        {
            Content = content;
        }

        /// <summary>
        /// Set content of the <see cref="ContentControl"/>
        /// </summary>
        /// <param name="content"></param>
        /// <param name="forward"></param>
        public void SetContent(object? content, bool forward)
        {
            _backward = !forward;
            Content = content;
        }

        /// <summary>
        /// Wait for the running transition
        /// </summary>
        /// <returns></returns>
        public Task WaitForTransitionAsync()
        {
#if NET45
            return _lastTask ?? Task.FromResult(0);
#else
            return _lastTask ?? Task.CompletedTask;
#endif
        }

        private void ContentControlLoaded(object sender, RoutedEventArgs e)
        {
            if (TransitionMode == ContentTransitionMode.ChangedOrLoaded)
            {
                _lastTask = this.TransionCurrentAsync(true);
                _lastTaskIsLoadedTransition = true;
            }
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentsPanel = (Panel)GetTemplateChild("PART_Contents");

            if (_pendingNewContent is not null)
            {
                _lastTask = this.ApplyContentChangeAsync(null, _pendingNewContent);
                _lastTaskIsLoadedTransition = false;
                _pendingNewContent = null;
            }
        }


        /// <summary>
        /// The DependencyProperty of <see cref="Content"/> property
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(ContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnContentChanged)));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentDelay"/>
        /// </summary>
        public static readonly DependencyProperty ContentDelayProperty =
            DependencyProperty.Register(nameof(ContentDelay), typeof(Duration), typeof(ContentControl), new FrameworkPropertyMetadata(default(Duration), propertyChangedCallback: null, coerceValueCallback: CoerceContentDelay));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentTemplate"/> property
        /// </summary>
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(ContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="ContentTemplateSelector"/> property
        /// </summary>
        public static readonly DependencyProperty ContentTemplateSelectorProperty =
            DependencyProperty.Register(nameof(ContentTemplateSelector), typeof(DataTemplateSelector), typeof(ContentControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// The DependencyProperty of <see cref="TransitionModeProperty"/> property
        /// </summary>
        public static readonly DependencyProperty TransitionModeProperty =
            DependencyProperty.Register(nameof(TransitionMode), typeof(ContentTransitionMode), typeof(ContentControl), new PropertyMetadata(ContentTransitionMode.Standard));

        /// <summary>
        /// The DependencyProperty of <see cref="Transition"/> property
        /// </summary>
        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.Register(nameof(Transition), typeof(IContentTransition), typeof(ContentControl), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The DependencyProperty of <see cref="CornerRadius"/> property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ContentControl));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContentControl transitioningContentControl)
            {
                transitioningContentControl._lastTask = transitioningContentControl.ApplyContentChangeAsync(e.OldValue, e.NewValue);
                transitioningContentControl._lastTaskIsLoadedTransition = false;
            }
        }

        private static object CoerceContentDelay(DependencyObject d, object baseValue)
        {
            if (baseValue is not Duration duration ||
                !duration.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return baseValue;
        }

        private async Task TransionCurrentAsync(bool avoidOther)
        {
            if (_contentsPanel is null)
            {
                return;
            }

            if (avoidOther && 
                !_lastTaskIsLoadedTransition &&
                _lastTask != null &&
                !_lastTask.IsCompleted)
            {
                return;
            }

            var delay = ContentDelay;

            if (delay != default &&
                delay.TimeSpan != default)
            {
                await Task.Delay(delay.TimeSpan);
            }

            if (_contentsPanel.Children.Count == 0)
            {
                return;
            }

            FrameworkElement? elementToTransition = 
                _contentsPanel.Children[_contentsPanel.Children.Count - 1] as FrameworkElement;

            if (Transition is IContentTransition transition &&
                elementToTransition is not null)
            {
                _lastTaskCancellation = new();
                await transition.Run(this, null, elementToTransition, true, _lastTaskCancellation.Token);
            }
        }

        private async Task ApplyContentChangeAsync(object? oldContent, object? newContent)
        {
            if (oldContent is not null)
            {
                RemoveLogicalChild(oldContent);
            }

            if (newContent is not null && 
                newContent != _pendingNewContent)
            {
                if (newContent is DependencyObject newContentDO &&
                    LogicalTreeHelper.GetParent(newContentDO) is null)
                {
                    AddLogicalChild(newContent);
                }
            }

            if (_contentsPanel is null)
            {
                _pendingNewContent = newContent;
                return;
            }

            var delay = ContentDelay;

            if (delay != default && 
                delay.TimeSpan != default)
            {
                await Task.Delay(delay.TimeSpan);
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

            if (Transition is IContentTransition transition)
            {
                if (newContentElement is not null)
                {
                    _contentsPanel.Children.Add(newContentElement);
                }

                _lastOldControl = oldContentElement;
                _pendingNewContent = null;
                _backward = false;

                _lastTaskCancellation = new();
                await transition.Run(this, oldContentElement as FrameworkElement, newContentElement as FrameworkElement, forward, _lastTaskCancellation.Token);

                if (oldContentElement is not null)
                {
                    _contentsPanel.Children.Remove(oldContentElement);
                }
            }
            else
            {
                if (oldContentElement is not null)
                {
                    _contentsPanel.Children.Remove(oldContentElement);
                }

                if (newContentElement is not null)
                {
                    _contentsPanel.Children.Add(newContentElement);
                }
            }
        }
    }
}
