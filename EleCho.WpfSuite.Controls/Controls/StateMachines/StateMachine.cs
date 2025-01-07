using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.StateMachines
{
    public static class StateMachine
    {
        private static void OnStateChanged(FrameworkElement frameworkElement, string? activeState)
        {
            if (frameworkElement is null)
            {
                return;
            }

            var states = GetStates(frameworkElement);

            if (states is null)
            {
                frameworkElement.ClearValue(PresentationPropertyKey);
                return;
            }

            var presentation = GetPresentation(frameworkElement);
            var targetState = states.FirstOrDefault(s => s.Name == activeState);

            if (targetState is null)
            {
                frameworkElement.SetValue(PresentationPropertyKey, null);
                return;
            }

            if (presentation is null)
            {
                frameworkElement.SetValue(PresentationPropertyKey, targetState);
                return;
            }

            if (targetState is not State targetNormalState)
            {
                frameworkElement.SetValue(PresentationPropertyKey, targetState);
                return;
            }

            var totalTransitionDuration = targetNormalState.TotalEasingDuration;
            if (totalTransitionDuration == default)
            {
                frameworkElement.SetValue(PresentationPropertyKey, targetNormalState);
                return;
            }

            var transitioningState = new TransitioningState(presentation, targetNormalState);
            var doubleAnimation = new DoubleAnimation()
            {
                From = 0,
                To = totalTransitionDuration.TotalMilliseconds,
                Duration = totalTransitionDuration,
            };

            var storyboard = new Storyboard();
            Storyboard.SetTarget(doubleAnimation, transitioningState);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(TransitioningState.TimeMillisecondsProperty));
            storyboard.Children.Add(doubleAnimation);

            storyboard.Completed += (s, e) =>
            {
                transitioningState.TimeMilliseconds = totalTransitionDuration.TotalMilliseconds;
                storyboard.Stop();
            };

            storyboard.Begin(frameworkElement);

            frameworkElement.SetValue(PresentationPropertyKey, transitioningState);
        }

        public static StateCollection? GetStates(DependencyObject obj)
        {
            return (StateCollection)obj.GetValue(StatesProperty);
        }

        public static void SetStates(DependencyObject obj, StateCollection value)
        {
            obj.SetValue(StatesProperty, value);
        }

        public static string GetActiveState(DependencyObject obj)
        {
            return (string)obj.GetValue(ActiveStateProperty);
        }

        public static void SetActiveState(DependencyObject obj, string value)
        {
            obj.SetValue(ActiveStateProperty, value);
        }

        public static IState GetPresentation(DependencyObject obj)
        {
            return (IState)obj.GetValue(PresentationProperty);
        }

        public static void SetPresentation(DependencyObject obj, IState value)
        {
            obj.SetValue(PresentationProperty, value);
        }


        public static readonly DependencyProperty StatesProperty =
            DependencyProperty.RegisterAttached("States", typeof(StateCollection), typeof(StateMachine), new FrameworkPropertyMetadata(null, propertyChangedCallback: OnStatesChanged));

        public static readonly DependencyProperty ActiveStateProperty =
            DependencyProperty.RegisterAttached("ActiveState", typeof(string), typeof(StateMachine), new FrameworkPropertyMetadata(null, propertyChangedCallback: OnActiveStateChanged));

        private static readonly DependencyPropertyKey PresentationPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("Presentation", typeof(IState), typeof(StateMachine), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PresentationProperty = PresentationPropertyKey.DependencyProperty;


        private static void OnStatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                return;
            }

            var activeState = GetActiveState(d);
            OnStateChanged(frameworkElement, activeState);
        }

        private static void OnActiveStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                return;
            }

            var activeState = (string?)e.NewValue;
            OnStateChanged(frameworkElement, activeState);
        }
    }
}
