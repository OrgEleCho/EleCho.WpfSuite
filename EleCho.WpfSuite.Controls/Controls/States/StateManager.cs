using EleCho.WpfSuite.Controls.States.Internal;
using EleCho.WpfSuite.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.States
{
    public static class StateManager
    {
        private record struct DependencyObjectAndStateProperty(DependencyObject DependencyObject, StateProperty StateProperty);

        private static Dictionary<DependencyObjectAndStateProperty, Storyboard>? _runningStoryboards;

        #region Getter Setter

        public static State GetActiveState(DependencyObject obj)
        {
            return (State)obj.GetValue(ActiveStateProperty);
        }

        public static void SetActiveState(DependencyObject obj, State value)
        {
            obj.SetValue(ActiveStateProperty, value);
        }

        #region Values

        #region State Normal

        public static Brush GetNormalBackground(DependencyObject obj) => (Brush)obj.GetValue(NormalBackgroundProperty);
        public static void SetNormalBackground(DependencyObject obj, Brush value) => obj.SetValue(NormalBackgroundProperty, value);

        public static Brush GetNormalForeground(DependencyObject obj) => (Brush)obj.GetValue(NormalForegroundProperty);
        public static void SetNormalForeground(DependencyObject obj, Brush value) => obj.SetValue(NormalForegroundProperty, value);

        public static Brush GetNormalBorderBrush(DependencyObject obj) => (Brush)obj.GetValue(NormalBorderBrushProperty);
        public static void SetNormalBorderBrush(DependencyObject obj, Brush value) => obj.SetValue(NormalBorderBrushProperty, value);

        public static Thickness GetNormalPadding(DependencyObject obj) => (Thickness)obj.GetValue(NormalPaddingProperty);
        public static void SetNormalPadding(DependencyObject obj, Thickness value) => obj.SetValue(NormalPaddingProperty, value);

        public static Thickness GetNormalBorderThickness(DependencyObject obj) => (Thickness)obj.GetValue(NormalBorderThicknessProperty);
        public static void SetNormalBorderThickness(DependencyObject obj, Thickness value) => obj.SetValue(NormalBorderThicknessProperty, value);

        public static CornerRadius GetNormalCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(NormalCornerRadiusProperty);
        public static void SetNormalCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(NormalCornerRadiusProperty, value);

        #endregion

        #region State Hover

        public static Brush? GetHoverBackground(DependencyObject obj) => (Brush?)obj.GetValue(HoverBackgroundProperty);
        public static void SetHoverBackground(DependencyObject obj, Brush? value) => obj.SetValue(HoverBackgroundProperty, value);

        public static Brush? GetHoverForeground(DependencyObject obj) => (Brush?)obj.GetValue(HoverForegroundProperty);
        public static void SetHoverForeground(DependencyObject obj, Brush? value) => obj.SetValue(HoverForegroundProperty, value);

        public static Brush? GetHoverBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(HoverBorderBrushProperty);
        public static void SetHoverBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(HoverBorderBrushProperty, value);

        public static Thickness? GetHoverPadding(DependencyObject obj) => (Thickness?)obj.GetValue(HoverPaddingProperty);
        public static void SetHoverPadding(DependencyObject obj, Thickness? value) => obj.SetValue(HoverPaddingProperty, value);

        public static Thickness? GetHoverBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(HoverBorderThicknessProperty);
        public static void SetHoverBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(HoverBorderThicknessProperty, value);

        public static CornerRadius? GetHoverCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(HoverCornerRadiusProperty);
        public static void SetHoverCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(HoverCornerRadiusProperty, value);

        #endregion

        #region State Pressed

        public static Brush? GetPressedBackground(DependencyObject obj) => (Brush?)obj.GetValue(PressedBackgroundProperty);
        public static void SetPressedBackground(DependencyObject obj, Brush? value) => obj.SetValue(PressedBackgroundProperty, value);

        public static Brush? GetPressedForeground(DependencyObject obj) => (Brush?)obj.GetValue(PressedForegroundProperty);
        public static void SetPressedForeground(DependencyObject obj, Brush? value) => obj.SetValue(PressedForegroundProperty, value);

        public static Brush? GetPressedBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(PressedBorderBrushProperty);
        public static void SetPressedBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(PressedBorderBrushProperty, value);

        public static Thickness? GetPressedPadding(DependencyObject obj) => (Thickness?)obj.GetValue(PressedPaddingProperty);
        public static void SetPressedPadding(DependencyObject obj, Thickness? value) => obj.SetValue(PressedPaddingProperty, value);

        public static Thickness? GetPressedBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(PressedBorderThicknessProperty);
        public static void SetPressedBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(PressedBorderThicknessProperty, value);

        public static CornerRadius? GetPressedCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(PressedCornerRadiusProperty);
        public static void SetPressedCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(PressedCornerRadiusProperty, value);

        #endregion

        #region State Checked

        public static Brush? GetCheckedBackground(DependencyObject obj) => (Brush?)obj.GetValue(CheckedBackgroundProperty);
        public static void SetCheckedBackground(DependencyObject obj, Brush? value) => obj.SetValue(CheckedBackgroundProperty, value);

        public static Brush? GetCheckedForeground(DependencyObject obj) => (Brush?)obj.GetValue(CheckedForegroundProperty);
        public static void SetCheckedForeground(DependencyObject obj, Brush? value) => obj.SetValue(CheckedForegroundProperty, value);

        public static Brush? GetCheckedBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(CheckedBorderBrushProperty);
        public static void SetCheckedBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(CheckedBorderBrushProperty, value);

        public static Thickness? GetCheckedPadding(DependencyObject obj) => (Thickness?)obj.GetValue(CheckedPaddingProperty);
        public static void SetCheckedPadding(DependencyObject obj, Thickness? value) => obj.SetValue(CheckedPaddingProperty, value);

        public static Thickness? GetCheckedBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(CheckedBorderThicknessProperty);
        public static void SetCheckedBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(CheckedBorderThicknessProperty, value);

        public static CornerRadius? GetCheckedCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(CheckedCornerRadiusProperty);
        public static void SetCheckedCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(CheckedCornerRadiusProperty, value);

        #endregion

        #region State Selected

        public static Brush? GetSelectedBackground(DependencyObject obj) => (Brush?)obj.GetValue(SelectedBackgroundProperty);
        public static void SetSelectedBackground(DependencyObject obj, Brush? value) => obj.SetValue(SelectedBackgroundProperty, value);

        public static Brush? GetSelectedForeground(DependencyObject obj) => (Brush?)obj.GetValue(SelectedForegroundProperty);
        public static void SetSelectedForeground(DependencyObject obj, Brush? value) => obj.SetValue(SelectedForegroundProperty, value);

        public static Brush? GetSelectedBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(SelectedBorderBrushProperty);
        public static void SetSelectedBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(SelectedBorderBrushProperty, value);

        public static Thickness? GetSelectedPadding(DependencyObject obj) => (Thickness?)obj.GetValue(SelectedPaddingProperty);
        public static void SetSelectedPadding(DependencyObject obj, Thickness? value) => obj.SetValue(SelectedPaddingProperty, value);

        public static Thickness? GetSelectedBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(SelectedBorderThicknessProperty);
        public static void SetSelectedBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(SelectedBorderThicknessProperty, value);

        public static CornerRadius? GetSelectedCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(SelectedCornerRadiusProperty);
        public static void SetSelectedCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(SelectedCornerRadiusProperty, value);

        #endregion

        #region State SelectedActive

        public static Brush? GetSelectedActiveBackground(DependencyObject obj) => (Brush?)obj.GetValue(SelectedActiveBackgroundProperty);
        public static void SetSelectedActiveBackground(DependencyObject obj, Brush? value) => obj.SetValue(SelectedActiveBackgroundProperty, value);

        public static Brush? GetSelectedActiveForeground(DependencyObject obj) => (Brush?)obj.GetValue(SelectedActiveForegroundProperty);
        public static void SetSelectedActiveForeground(DependencyObject obj, Brush? value) => obj.SetValue(SelectedActiveForegroundProperty, value);

        public static Brush? GetSelectedActiveBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(SelectedActiveBorderBrushProperty);
        public static void SetSelectedActiveBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(SelectedActiveBorderBrushProperty, value);

        public static Thickness? GetSelectedActivePadding(DependencyObject obj) => (Thickness?)obj.GetValue(SelectedActivePaddingProperty);
        public static void SetSelectedActivePadding(DependencyObject obj, Thickness? value) => obj.SetValue(SelectedActivePaddingProperty, value);

        public static Thickness? GetSelectedActiveBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(SelectedActiveBorderThicknessProperty);
        public static void SetSelectedActiveBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(SelectedActiveBorderThicknessProperty, value);

        public static CornerRadius? GetSelectedActiveCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(SelectedActiveCornerRadiusProperty);
        public static void SetSelectedActiveCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(SelectedActiveCornerRadiusProperty, value);

        #endregion

        #region State Disabled

        public static Brush? GetDisabledBackground(DependencyObject obj) => (Brush?)obj.GetValue(DisabledBackgroundProperty);
        public static void SetDisabledBackground(DependencyObject obj, Brush? value) => obj.SetValue(DisabledBackgroundProperty, value);

        public static Brush? GetDisabledForeground(DependencyObject obj) => (Brush?)obj.GetValue(DisabledForegroundProperty);
        public static void SetDisabledForeground(DependencyObject obj, Brush? value) => obj.SetValue(DisabledForegroundProperty, value);

        public static Brush? GetDisabledBorderBrush(DependencyObject obj) => (Brush?)obj.GetValue(DisabledBorderBrushProperty);
        public static void SetDisabledBorderBrush(DependencyObject obj, Brush? value) => obj.SetValue(DisabledBorderBrushProperty, value);

        public static Thickness? GetDisabledPadding(DependencyObject obj) => (Thickness?)obj.GetValue(DisabledPaddingProperty);
        public static void SetDisabledPadding(DependencyObject obj, Thickness? value) => obj.SetValue(DisabledPaddingProperty, value);

        public static Thickness? GetDisabledBorderThickness(DependencyObject obj) => (Thickness?)obj.GetValue(DisabledBorderThicknessProperty);
        public static void SetDisabledBorderThickness(DependencyObject obj, Thickness? value) => obj.SetValue(DisabledBorderThicknessProperty, value);

        public static CornerRadius? GetDisabledCornerRadius(DependencyObject obj) => (CornerRadius?)obj.GetValue(DisabledCornerRadiusProperty);
        public static void SetDisabledCornerRadius(DependencyObject obj, CornerRadius? value) => obj.SetValue(DisabledCornerRadiusProperty, value);

        #endregion

        #region Showing

        public static Brush GetShowingBackground(DependencyObject obj) => (Brush)obj.GetValue(ShowingBackgroundProperty);

        public static Brush GetShowingForeground(DependencyObject obj) => (Brush)obj.GetValue(ShowingForegroundProperty);

        public static Brush GetShowingBorderBrush(DependencyObject obj) => (Brush)obj.GetValue(ShowingBorderBrushProperty);

        public static Thickness GetShowingPadding(DependencyObject obj) => (Thickness)obj.GetValue(ShowingPaddingProperty);

        public static Thickness GetShowingBorderThickness(DependencyObject obj) => (Thickness)obj.GetValue(ShowingBorderThicknessProperty);

        public static CornerRadius GetShowingCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(ShowingCornerRadiusProperty);

        #endregion

        #endregion

        #region Duration

        #region State Normal

        public static Duration GetDefaultTransitionDuration(DependencyObject obj) => (Duration)obj.GetValue(DefaultTransitionDurationProperty);
        public static void SetDefaultTransitionDuration(DependencyObject obj, Duration value) => obj.SetValue(DefaultTransitionDurationProperty, value);

        public static Duration? GetNormalBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalBackgroundTransitionDurationProperty);
        public static void SetNormalBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalBackgroundTransitionDurationProperty, value);

        public static Duration? GetNormalForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalForegroundTransitionDurationProperty);
        public static void SetNormalForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalForegroundTransitionDurationProperty, value);

        public static Duration? GetNormalBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalBorderBrushTransitionDurationProperty);
        public static void SetNormalBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalBorderBrushTransitionDurationProperty, value);

        public static Duration? GetNormalPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalPaddingTransitionDurationProperty);
        public static void SetNormalPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalPaddingTransitionDurationProperty, value);

        public static Duration? GetNormalBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalBorderThicknessTransitionDurationProperty);
        public static void SetNormalBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetNormalCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(NormalCornerRadiusTransitionDurationProperty);
        public static void SetNormalCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(NormalCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State Hover

        public static Duration? GetHoverTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverTransitionDurationProperty);
        public static void SetHoverTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverTransitionDurationProperty, value);

        public static Duration? GetHoverBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverBackgroundTransitionDurationProperty);
        public static void SetHoverBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverBackgroundTransitionDurationProperty, value);

        public static Duration? GetHoverForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverForegroundTransitionDurationProperty);
        public static void SetHoverForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverForegroundTransitionDurationProperty, value);

        public static Duration? GetHoverBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverBorderBrushTransitionDurationProperty);
        public static void SetHoverBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverBorderBrushTransitionDurationProperty, value);

        public static Duration? GetHoverPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverPaddingTransitionDurationProperty);
        public static void SetHoverPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverPaddingTransitionDurationProperty, value);

        public static Duration? GetHoverBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverBorderThicknessTransitionDurationProperty);
        public static void SetHoverBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetHoverCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(HoverCornerRadiusTransitionDurationProperty);
        public static void SetHoverCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(HoverCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State Pressed

        public static Duration? GetPressedTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedTransitionDurationProperty);
        public static void SetPressedTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedTransitionDurationProperty, value);

        public static Duration? GetPressedBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedBackgroundTransitionDurationProperty);
        public static void SetPressedBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedBackgroundTransitionDurationProperty, value);

        public static Duration? GetPressedForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedForegroundTransitionDurationProperty);
        public static void SetPressedForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedForegroundTransitionDurationProperty, value);

        public static Duration? GetPressedBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedBorderBrushTransitionDurationProperty);
        public static void SetPressedBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedBorderBrushTransitionDurationProperty, value);

        public static Duration? GetPressedPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedPaddingTransitionDurationProperty);
        public static void SetPressedPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedPaddingTransitionDurationProperty, value);

        public static Duration? GetPressedBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedBorderThicknessTransitionDurationProperty);
        public static void SetPressedBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetPressedCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(PressedCornerRadiusTransitionDurationProperty);
        public static void SetPressedCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(PressedCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State Checked

        public static Duration? GetCheckedTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedTransitionDurationProperty);
        public static void SetCheckedTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedTransitionDurationProperty, value);

        public static Duration? GetCheckedBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedBackgroundTransitionDurationProperty);
        public static void SetCheckedBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedBackgroundTransitionDurationProperty, value);

        public static Duration? GetCheckedForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedForegroundTransitionDurationProperty);
        public static void SetCheckedForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedForegroundTransitionDurationProperty, value);

        public static Duration? GetCheckedBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedBorderBrushTransitionDurationProperty);
        public static void SetCheckedBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedBorderBrushTransitionDurationProperty, value);

        public static Duration? GetCheckedPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedPaddingTransitionDurationProperty);
        public static void SetCheckedPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedPaddingTransitionDurationProperty, value);

        public static Duration? GetCheckedBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedBorderThicknessTransitionDurationProperty);
        public static void SetCheckedBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetCheckedCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(CheckedCornerRadiusTransitionDurationProperty);
        public static void SetCheckedCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(CheckedCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State Selected

        public static Duration? GetSelectedTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedTransitionDurationProperty);
        public static void SetSelectedTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedTransitionDurationProperty, value);

        public static Duration? GetSelectedBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedBackgroundTransitionDurationProperty);
        public static void SetSelectedBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedBackgroundTransitionDurationProperty, value);

        public static Duration? GetSelectedForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedForegroundTransitionDurationProperty);
        public static void SetSelectedForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedForegroundTransitionDurationProperty, value);

        public static Duration? GetSelectedBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedBorderBrushTransitionDurationProperty);
        public static void SetSelectedBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedBorderBrushTransitionDurationProperty, value);

        public static Duration? GetSelectedPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedPaddingTransitionDurationProperty);
        public static void SetSelectedPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedPaddingTransitionDurationProperty, value);

        public static Duration? GetSelectedBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedBorderThicknessTransitionDurationProperty);
        public static void SetSelectedBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetSelectedCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedCornerRadiusTransitionDurationProperty);
        public static void SetSelectedCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State SelectedActive

        public static Duration? GetSelectedActiveTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveTransitionDurationProperty);
        public static void SetSelectedActiveTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveTransitionDurationProperty, value);

        public static Duration? GetSelectedActiveBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveBackgroundTransitionDurationProperty);
        public static void SetSelectedActiveBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveBackgroundTransitionDurationProperty, value);

        public static Duration? GetSelectedActiveForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveForegroundTransitionDurationProperty);
        public static void SetSelectedActiveForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveForegroundTransitionDurationProperty, value);

        public static Duration? GetSelectedActiveBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveBorderBrushTransitionDurationProperty);
        public static void SetSelectedActiveBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveBorderBrushTransitionDurationProperty, value);

        public static Duration? GetSelectedActivePaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActivePaddingTransitionDurationProperty);
        public static void SetSelectedActivePaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActivePaddingTransitionDurationProperty, value);

        public static Duration? GetSelectedActiveBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveBorderThicknessTransitionDurationProperty);
        public static void SetSelectedActiveBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetSelectedActiveCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(SelectedActiveCornerRadiusTransitionDurationProperty);
        public static void SetSelectedActiveCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(SelectedActiveCornerRadiusTransitionDurationProperty, value);

        #endregion

        #region State Disabled

        public static Duration? GetDisabledTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledTransitionDurationProperty);
        public static void SetDisabledTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledTransitionDurationProperty, value);

        public static Duration? GetDisabledBackgroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledBackgroundTransitionDurationProperty);
        public static void SetDisabledBackgroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledBackgroundTransitionDurationProperty, value);

        public static Duration? GetDisabledForegroundTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledForegroundTransitionDurationProperty);
        public static void SetDisabledForegroundTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledForegroundTransitionDurationProperty, value);

        public static Duration? GetDisabledBorderBrushTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledBorderBrushTransitionDurationProperty);
        public static void SetDisabledBorderBrushTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledBorderBrushTransitionDurationProperty, value);

        public static Duration? GetDisabledPaddingTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledPaddingTransitionDurationProperty);
        public static void SetDisabledPaddingTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledPaddingTransitionDurationProperty, value);

        public static Duration? GetDisabledBorderThicknessTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledBorderThicknessTransitionDurationProperty);
        public static void SetDisabledBorderThicknessTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledBorderThicknessTransitionDurationProperty, value);

        public static Duration? GetDisabledCornerRadiusTransitionDuration(DependencyObject obj) => (Duration?)obj.GetValue(DisabledCornerRadiusTransitionDurationProperty);
        public static void SetDisabledCornerRadiusTransitionDuration(DependencyObject obj, Duration? value) => obj.SetValue(DisabledCornerRadiusTransitionDurationProperty, value);

        #endregion

        #endregion

        #region EasingFunction

        #region State Normal

        public static IEasingFunction GetDefaultEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DefaultEasingFunctionProperty);
        public static void SetDefaultEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DefaultEasingFunctionProperty, value);

        public static IEasingFunction GetNormalBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalBackgroundEasingFunctionProperty);
        public static void SetNormalBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetNormalForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalForegroundEasingFunctionProperty);
        public static void SetNormalForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetNormalBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalBorderBrushEasingFunctionProperty);
        public static void SetNormalBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetNormalPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalPaddingEasingFunctionProperty);
        public static void SetNormalPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetNormalBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalBorderThicknessEasingFunctionProperty);
        public static void SetNormalBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetNormalCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(NormalCornerRadiusEasingFunctionProperty);
        public static void SetNormalCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(NormalCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State Hover

        public static IEasingFunction GetHoverEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverEasingFunctionProperty);
        public static void SetHoverEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverEasingFunctionProperty, value);

        public static IEasingFunction GetHoverBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverBackgroundEasingFunctionProperty);
        public static void SetHoverBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetHoverForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverForegroundEasingFunctionProperty);
        public static void SetHoverForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetHoverBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverBorderBrushEasingFunctionProperty);
        public static void SetHoverBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetHoverPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverPaddingEasingFunctionProperty);
        public static void SetHoverPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetHoverBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverBorderThicknessEasingFunctionProperty);
        public static void SetHoverBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetHoverCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(HoverCornerRadiusEasingFunctionProperty);
        public static void SetHoverCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(HoverCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State Pressed

        public static IEasingFunction GetPressedEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedEasingFunctionProperty);
        public static void SetPressedEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedEasingFunctionProperty, value);

        public static IEasingFunction GetPressedBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedBackgroundEasingFunctionProperty);
        public static void SetPressedBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetPressedForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedForegroundEasingFunctionProperty);
        public static void SetPressedForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetPressedBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedBorderBrushEasingFunctionProperty);
        public static void SetPressedBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetPressedPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedPaddingEasingFunctionProperty);
        public static void SetPressedPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetPressedBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedBorderThicknessEasingFunctionProperty);
        public static void SetPressedBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetPressedCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(PressedCornerRadiusEasingFunctionProperty);
        public static void SetPressedCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(PressedCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State Checked

        public static IEasingFunction GetCheckedEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedEasingFunctionProperty);
        public static void SetCheckedEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedBackgroundEasingFunctionProperty);
        public static void SetCheckedBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedForegroundEasingFunctionProperty);
        public static void SetCheckedForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedBorderBrushEasingFunctionProperty);
        public static void SetCheckedBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedPaddingEasingFunctionProperty);
        public static void SetCheckedPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedBorderThicknessEasingFunctionProperty);
        public static void SetCheckedBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetCheckedCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(CheckedCornerRadiusEasingFunctionProperty);
        public static void SetCheckedCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(CheckedCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State Selected

        public static IEasingFunction GetSelectedEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedEasingFunctionProperty);
        public static void SetSelectedEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedBackgroundEasingFunctionProperty);
        public static void SetSelectedBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedForegroundEasingFunctionProperty);
        public static void SetSelectedForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedBorderBrushEasingFunctionProperty);
        public static void SetSelectedBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedPaddingEasingFunctionProperty);
        public static void SetSelectedPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedBorderThicknessEasingFunctionProperty);
        public static void SetSelectedBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedCornerRadiusEasingFunctionProperty);
        public static void SetSelectedCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State SelectedActive

        public static IEasingFunction GetSelectedActiveEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveEasingFunctionProperty);
        public static void SetSelectedActiveEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActiveBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveBackgroundEasingFunctionProperty);
        public static void SetSelectedActiveBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActiveForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveForegroundEasingFunctionProperty);
        public static void SetSelectedActiveForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActiveBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveBorderBrushEasingFunctionProperty);
        public static void SetSelectedActiveBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActivePaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActivePaddingEasingFunctionProperty);
        public static void SetSelectedActivePaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActivePaddingEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActiveBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveBorderThicknessEasingFunctionProperty);
        public static void SetSelectedActiveBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetSelectedActiveCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(SelectedActiveCornerRadiusEasingFunctionProperty);
        public static void SetSelectedActiveCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(SelectedActiveCornerRadiusEasingFunctionProperty, value);

        #endregion

        #region State Disabled

        public static IEasingFunction GetDisabledEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledEasingFunctionProperty);
        public static void SetDisabledEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledBackgroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledBackgroundEasingFunctionProperty);
        public static void SetDisabledBackgroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledBackgroundEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledForegroundEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledForegroundEasingFunctionProperty);
        public static void SetDisabledForegroundEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledForegroundEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledBorderBrushEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledBorderBrushEasingFunctionProperty);
        public static void SetDisabledBorderBrushEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledBorderBrushEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledPaddingEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledPaddingEasingFunctionProperty);
        public static void SetDisabledPaddingEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledPaddingEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledBorderThicknessEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledBorderThicknessEasingFunctionProperty);
        public static void SetDisabledBorderThicknessEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledBorderThicknessEasingFunctionProperty, value);

        public static IEasingFunction GetDisabledCornerRadiusEasingFunction(DependencyObject obj) => (IEasingFunction)obj.GetValue(DisabledCornerRadiusEasingFunctionProperty);
        public static void SetDisabledCornerRadiusEasingFunction(DependencyObject obj, IEasingFunction value) => obj.SetValue(DisabledCornerRadiusEasingFunctionProperty, value);

        #endregion

        #endregion

        #region State Fallbacks


        public static State? GetStateHoverFallback(DependencyObject obj) => (State?)obj.GetValue(StateHoverFallbackProperty);
        public static void SetStateHoverFallback(DependencyObject obj, State? value) => obj.SetValue(StateHoverFallbackProperty, value);

        public static State? GetStatePressedFallback(DependencyObject obj) => (State?)obj.GetValue(StatePressedFallbackProperty);
        public static void SetStatePressedFallback(DependencyObject obj, State? value) => obj.SetValue(StatePressedFallbackProperty, value);

        public static State? GetStateCheckedFallback(DependencyObject obj) => (State?)obj.GetValue(StateCheckedFallbackProperty);
        public static void SetStateCheckedFallback(DependencyObject obj, State? value) => obj.SetValue(StateCheckedFallbackProperty, value);

        public static State? GetStateSelectedFallback(DependencyObject obj) => (State?)obj.GetValue(StateSelectedFallbackProperty);
        public static void SetStateSelectedFallback(DependencyObject obj, State? value) => obj.SetValue(StateSelectedFallbackProperty, value);

        public static State? GetStateSelectedActiveFallback(DependencyObject obj) => (State?)obj.GetValue(StateSelectedActiveFallbackProperty);
        public static void SetStateSelectedActiveFallback(DependencyObject obj, State? value) => obj.SetValue(StateSelectedActiveFallbackProperty, value);

        public static State? GetStateDisabledFallback(DependencyObject obj) => (State?)obj.GetValue(StateDisabledFallbackProperty);
        public static void SetStateDisabledFallback(DependencyObject obj, State? value) => obj.SetValue(StateDisabledFallbackProperty, value);


        #endregion

        #endregion


        #region DependencyProperties

        public static readonly DependencyProperty ActiveStateProperty =
            DependencyProperty.RegisterAttached("ActiveState", typeof(State), typeof(StateManager), new PropertyMetadata(State.Normal, propertyChangedCallback: OnActiveStateChanged));

        #region Values

        #region State Normal

        public static readonly DependencyProperty NormalBackgroundProperty =
            DependencyProperty.RegisterAttached("NormalBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty NormalForegroundProperty =
            DependencyProperty.RegisterAttached("NormalForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty NormalBorderBrushProperty =
            DependencyProperty.RegisterAttached("NormalBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty NormalPaddingProperty =
            DependencyProperty.RegisterAttached("NormalPadding", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty NormalBorderThicknessProperty =
            DependencyProperty.RegisterAttached("NormalBorderThickness", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty NormalCornerRadiusProperty =
            DependencyProperty.RegisterAttached("NormalCornerRadius", typeof(CornerRadius), typeof(StateManager), new PropertyMetadata(default(CornerRadius), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State Hover

        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.RegisterAttached("HoverBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.RegisterAttached("HoverForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.RegisterAttached("HoverBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty HoverPaddingProperty =
            DependencyProperty.RegisterAttached("HoverPadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty HoverBorderThicknessProperty =
            DependencyProperty.RegisterAttached("HoverBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty HoverCornerRadiusProperty =
            DependencyProperty.RegisterAttached("HoverCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State Pressed

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.RegisterAttached("PressedForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.RegisterAttached("PressedBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty PressedPaddingProperty =
            DependencyProperty.RegisterAttached("PressedPadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty PressedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("PressedBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty PressedCornerRadiusProperty =
            DependencyProperty.RegisterAttached("PressedCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State Checked

        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty CheckedPaddingProperty =
            DependencyProperty.RegisterAttached("CheckedPadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty CheckedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("CheckedBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty CheckedCornerRadiusProperty =
            DependencyProperty.RegisterAttached("CheckedCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State Selected

        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty SelectedPaddingProperty =
            DependencyProperty.RegisterAttached("SelectedPadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty SelectedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SelectedBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty SelectedCornerRadiusProperty =
            DependencyProperty.RegisterAttached("SelectedCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State SelectedActive

        public static readonly DependencyProperty SelectedActiveBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty SelectedActiveForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedActiveForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty SelectedActiveBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty SelectedActivePaddingProperty =
            DependencyProperty.RegisterAttached("SelectedActivePadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty SelectedActiveBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty SelectedActiveCornerRadiusProperty =
            DependencyProperty.RegisterAttached("SelectedActiveCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region State Disabled

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.RegisterAttached("DisabledBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBackgroundChanged));

        public static readonly DependencyProperty DisabledForegroundProperty =
            DependencyProperty.RegisterAttached("DisabledForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateForegroundChanged));

        public static readonly DependencyProperty DisabledBorderBrushProperty =
            DependencyProperty.RegisterAttached("DisabledBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnAnyStateBorderBrushChanged));

        public static readonly DependencyProperty DisabledPaddingProperty =
            DependencyProperty.RegisterAttached("DisabledPadding", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStatePaddingChanged));

        public static readonly DependencyProperty DisabledBorderThicknessProperty =
            DependencyProperty.RegisterAttached("DisabledBorderThickness", typeof(Thickness?), typeof(StateManager), new PropertyMetadata(default(Thickness?), propertyChangedCallback: OnAnyStateBorderThicknessChanged));

        public static readonly DependencyProperty DisabledCornerRadiusProperty =
            DependencyProperty.RegisterAttached("DisabledCornerRadius", typeof(CornerRadius?), typeof(StateManager), new PropertyMetadata(default(CornerRadius?), propertyChangedCallback: OnAnyStateCornerRadiusChanged));

        #endregion

        #region ShowingProxy

        internal static readonly DependencyProperty ShowingBackgroundProxyProperty =
            DependencyProperty.RegisterAttached("ShowingBackgroundProxy", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnShowingBackgroundProxyChanged));

        internal static readonly DependencyProperty ShowingForegroundProxyProperty =
            DependencyProperty.RegisterAttached("ShowingForegroundProxy", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnShowingForegroundProxyChanged));

        internal static readonly DependencyProperty ShowingBorderBrushProxyProperty =
            DependencyProperty.RegisterAttached("ShowingBorderBrushProxy", typeof(Brush), typeof(StateManager), new PropertyMetadata(null, propertyChangedCallback: OnShowingBorderBrushProxyChanged));

        internal static readonly DependencyProperty ShowingPaddingProxyProperty =
            DependencyProperty.RegisterAttached("ShowingPaddingProxy", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness), propertyChangedCallback: OnShowingPaddingProxyChanged));

        internal static readonly DependencyProperty ShowingBorderThicknessProxyProperty =
            DependencyProperty.RegisterAttached("ShowingBorderThicknessProxy", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness), propertyChangedCallback: OnShowingBorderThicknessProxyChanged));

        internal static readonly DependencyProperty ShowingCornerRadiusProxyProperty =
            DependencyProperty.RegisterAttached("ShowingCornerRadiusProxy", typeof(CornerRadius), typeof(StateManager), new PropertyMetadata(default(CornerRadius), propertyChangedCallback: OnShowingCornerRadiusProxyChanged));

        #endregion

        #region Showing

        public static readonly DependencyPropertyKey ShowingBackgroundPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingBackground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null));

        public static readonly DependencyPropertyKey ShowingForegroundPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingForeground", typeof(Brush), typeof(StateManager), new PropertyMetadata(null));

        public static readonly DependencyPropertyKey ShowingBorderBrushPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingBorderBrush", typeof(Brush), typeof(StateManager), new PropertyMetadata(null));

        public static readonly DependencyPropertyKey ShowingPaddingPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingPadding", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyPropertyKey ShowingBorderThicknessPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingBorderThickness", typeof(Thickness), typeof(StateManager), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyPropertyKey ShowingCornerRadiusPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ShowingCornerRadius", typeof(CornerRadius), typeof(StateManager), new PropertyMetadata(default(CornerRadius)));

        public static readonly DependencyProperty ShowingBackgroundProperty = ShowingBackgroundPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowingForegroundProperty = ShowingForegroundPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowingBorderBrushProperty = ShowingBorderBrushPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowingPaddingProperty = ShowingPaddingPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowingBorderThicknessProperty = ShowingBorderThicknessPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowingCornerRadiusProperty = ShowingCornerRadiusPropertyKey.DependencyProperty;

        #endregion

        #endregion

        #region Duration

        #region State Normal

        public static readonly DependencyProperty DefaultTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DefaultTransitionDuration", typeof(Duration), typeof(StateManager), new PropertyMetadata(new Duration(TimeSpan.Zero), null, coerceValueCallback: CoerceDuration));

        public static readonly DependencyProperty NormalBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty NormalForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty NormalBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty NormalPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty NormalBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty NormalCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("NormalCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State Hover

        public static readonly DependencyProperty HoverTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty HoverCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("HoverCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State Pressed

        public static readonly DependencyProperty PressedTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty PressedCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("PressedCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State Checked

        public static readonly DependencyProperty CheckedTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty CheckedCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("CheckedCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State Selected

        public static readonly DependencyProperty SelectedTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State SelectedActive

        public static readonly DependencyProperty SelectedActiveTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActiveBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActiveForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActiveBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActivePaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActivePaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActiveBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty SelectedActiveCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("SelectedActiveCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #region State Disabled

        public static readonly DependencyProperty DisabledTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledBackgroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledBackgroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledForegroundTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledForegroundTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledBorderBrushTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledBorderBrushTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledPaddingTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledPaddingTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledBorderThicknessTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledBorderThicknessTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        public static readonly DependencyProperty DisabledCornerRadiusTransitionDurationProperty =
            DependencyProperty.RegisterAttached("DisabledCornerRadiusTransitionDuration", typeof(Duration?), typeof(StateManager), new PropertyMetadata(default(Duration?), null, coerceValueCallback: CoerceNullableDuration));

        #endregion

        #endregion

        #region EasingFunction

        #region State Normal

        public static readonly DependencyProperty DefaultEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DefaultEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty NormalCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("NormalCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State Hover

        public static readonly DependencyProperty HoverEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty HoverCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("HoverCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State Pressed

        public static readonly DependencyProperty PressedEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty PressedCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("PressedCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State Checked

        public static readonly DependencyProperty CheckedEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty CheckedCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("CheckedCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State Selected

        public static readonly DependencyProperty SelectedEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State SelectedActive

        public static readonly DependencyProperty SelectedActiveEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActiveBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActiveForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActiveBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActivePaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActivePaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActiveBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty SelectedActiveCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("SelectedActiveCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #region State Disabled

        public static readonly DependencyProperty DisabledEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledBackgroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledBackgroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledForegroundEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledForegroundEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledBorderBrushEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledBorderBrushEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledPaddingEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledPaddingEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledBorderThicknessEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledBorderThicknessEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        public static readonly DependencyProperty DisabledCornerRadiusEasingFunctionProperty =
            DependencyProperty.RegisterAttached("DisabledCornerRadiusEasingFunction", typeof(IEasingFunction), typeof(StateManager), new PropertyMetadata(default(IEasingFunction)));

        #endregion

        #endregion

        #region State Fallbacks


        public static readonly DependencyProperty StateHoverFallbackProperty =
            DependencyProperty.RegisterAttached("StateHoverFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Normal));

        public static readonly DependencyProperty StatePressedFallbackProperty =
            DependencyProperty.RegisterAttached("StatePressedFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Hover));

        public static readonly DependencyProperty StateCheckedFallbackProperty =
            DependencyProperty.RegisterAttached("StateCheckedFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Pressed));

        public static readonly DependencyProperty StateSelectedFallbackProperty =
            DependencyProperty.RegisterAttached("StateSelectedFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Pressed));

        public static readonly DependencyProperty StateSelectedActiveFallbackProperty =
            DependencyProperty.RegisterAttached("StateSelectedActiveFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Selected));

        public static readonly DependencyProperty StateDisabledFallbackProperty =
            DependencyProperty.RegisterAttached("StateDisabledFallback", typeof(State?), typeof(StateManager), new PropertyMetadata(State.Normal));


        #endregion

        #endregion


        private static void OnActiveStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newState = (State)e.NewValue;

            ActiveStateBackground(d, newState);
            ActiveStateForeground(d, newState);
            ActiveStateBorderBrush(d, newState);
            ActiveStatePadding(d, newState);
            ActiveStateBorderThickness(d, newState);
            ActiveStateCornerRadius(d, newState);
        }

        private static void OnAnyStateBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStateBackground(d, GetActiveState(d));
        }

        private static void OnAnyStateForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStateForeground(d, GetActiveState(d));
        }

        private static void OnAnyStateBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStateBorderBrush(d, GetActiveState(d));
        }

        private static void OnAnyStatePaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStatePadding(d, GetActiveState(d));
        }

        private static void OnAnyStateBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStateBorderThickness(d, GetActiveState(d));
        }

        private static void OnAnyStateCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ActiveStateCornerRadius(d, GetActiveState(d));
        }



        private static object CoerceDuration(DependencyObject d, object baseValue)
        {
            if (baseValue is not Duration duration ||
                !duration.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return baseValue;
        }

        private static object CoerceNullableDuration(DependencyObject d, object baseValue)
        {
            if (baseValue is Duration duration &&
                !duration.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return baseValue;
        }

        private static void OnShowingBackgroundProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingBackgroundPropertyKey, e.NewValue);
        }

        private static void OnShowingForegroundProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingForegroundPropertyKey, e.NewValue);
        }

        private static void OnShowingBorderBrushProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingBorderBrushPropertyKey, e.NewValue);
        }

        private static void OnShowingPaddingProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingPaddingPropertyKey, e.NewValue);
        }

        private static void OnShowingBorderThicknessProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingBorderThicknessPropertyKey, e.NewValue);
        }

        private static void OnShowingCornerRadiusProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(ShowingCornerRadiusPropertyKey, e.NewValue);
        }

        private static T? GetStatePropertyStructValue<T>(DependencyObject d, State state, StateProperty property)
            where T : struct
        {
            var targetValue = state switch
            {
                State.Normal => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(NormalPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(NormalBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(NormalCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Hover => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(HoverPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(HoverBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(HoverCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Pressed => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(PressedPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(PressedBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(PressedCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Checked => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(CheckedPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(CheckedBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(CheckedCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Selected => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(NormalPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(NormalBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(NormalCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.SelectedActive => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(SelectedActivePaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(SelectedActiveBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(SelectedActiveCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Disabled => property switch
                {
                    StateProperty.Padding => (T?)d.GetValue(DisabledPaddingProperty),
                    StateProperty.BorderThickness => (T?)d.GetValue(DisabledBorderThicknessProperty),
                    StateProperty.CornerRadius => (T?)d.GetValue(DisabledCornerRadiusProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                _ => throw new ArgumentException("Invalid state", nameof(state))
            };

            if (targetValue is null)
            {
                var fallbackState = state switch
                {
                    State.Hover => GetStateHoverFallback(d),
                    _ => null,
                };

                if (fallbackState is not null)
                {
                    return GetStatePropertyStructValue<T>(d, fallbackState.Value, property);
                }
            }

            return targetValue;
        }

        private static T? GetStatePropertyClassValue<T>(DependencyObject d, State state, StateProperty property)
            where T : class
        {
            var targetValue = state switch
            {
                State.Normal => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(NormalBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(NormalForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(NormalBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Hover => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(HoverBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(HoverForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(HoverBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Pressed => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(PressedBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(PressedForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(PressedBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Checked => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(CheckedBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(CheckedForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(CheckedBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Selected => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(NormalBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(NormalForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(NormalBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.SelectedActive => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(SelectedActiveBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(SelectedActiveForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(SelectedActiveBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                State.Disabled => property switch
                {
                    StateProperty.Background => (T?)d.GetValue(DisabledBackgroundProperty),
                    StateProperty.Foreground => (T?)d.GetValue(DisabledForegroundProperty),
                    StateProperty.BorderBrush => (T?)d.GetValue(DisabledBorderBrushProperty),
                    _ => throw new ArgumentException("Invalid property", nameof(property)),
                },

                _ => throw new ArgumentException("Invalid state", nameof(state))
            };

            if (targetValue is null)
            {
                var fallbackState = state switch
                {
                    State.Hover => GetStateHoverFallback(d),
                    _ => null,
                };

                if (fallbackState is not null)
                {
                    return GetStatePropertyClassValue<T>(d, fallbackState.Value, property);
                }
            }

            return targetValue;
        }

        private static void ActiveStateBackground(DependencyObject d, State targetState)
        {
            var storyboardKey = new DependencyObjectAndStateProperty(d, StateProperty.Background);

            if (_runningStoryboards is not null &&
                _runningStoryboards.ContainsKey(storyboardKey))
            {
                _runningStoryboards[storyboardKey].Stop();
                _runningStoryboards.Remove(storyboardKey);
            }

            var targetValue = GetStatePropertyClassValue<Brush>(d, targetState, StateProperty.Background);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingBackgroundProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingBackgroundPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingBackground(d);
            animatable.BeginAnimation(ShowingBackgroundProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalBackgroundTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverBackgroundTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedBackgroundTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedBackgroundTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedBackgroundTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActiveBackgroundTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledBackgroundTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingBackgroundPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalBackgroundEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverBackgroundEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedBackgroundEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedBackgroundEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedBackgroundEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActiveBackgroundEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledBackgroundEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if ((nowValue is null || nowValue.CanFreeze) &&
                (targetValue is null || targetValue.CanFreeze))
            {
                BrushAnimation brushAnimation = new BrushAnimation()
                {
                    From = nowValue,
                    To = targetValue,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd,
                };

                animatable.BeginAnimation(ShowingBackgroundProxyProperty, brushAnimation, HandoffBehavior.SnapshotAndReplace);
            }
            else
            {
                _runningStoryboards ??= new();

                var brushTransitionHelper = new BrushTransitionHelper(nowValue, targetValue, d, ShowingBackgroundProxyProperty);

                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd
                };

                var storyboard = new Storyboard();
                Storyboard.SetTarget(doubleAnimation, brushTransitionHelper);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(BrushTransitionHelper.ProgressProperty));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Completed += (s, e) =>
                {
                    _runningStoryboards.Remove(storyboardKey);
                };

                _runningStoryboards[storyboardKey] = storyboard;
                animatable.BeginStoryboard(storyboard, HandoffBehavior.SnapshotAndReplace, true);
            }
        }

        private static void ActiveStateForeground(DependencyObject d, State targetState)
        {
            var storyboardKey = new DependencyObjectAndStateProperty(d, StateProperty.Foreground);

            if (_runningStoryboards is not null &&
                _runningStoryboards.ContainsKey(storyboardKey))
            {
                _runningStoryboards[storyboardKey].Stop();
                _runningStoryboards.Remove(storyboardKey);
            }

            var targetValue = GetStatePropertyClassValue<Brush>(d, targetState, StateProperty.Foreground);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingForegroundProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingForegroundPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingForeground(d);
            animatable.BeginAnimation(ShowingForegroundProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalForegroundTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverForegroundTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedForegroundTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedForegroundTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedForegroundTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActiveForegroundTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledForegroundTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingForegroundPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalForegroundEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverForegroundEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedForegroundEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedForegroundEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedForegroundEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActiveForegroundEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledForegroundEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if ((nowValue is null || nowValue.CanFreeze) &&
                (targetValue is null || targetValue.CanFreeze))
            {
                BrushAnimation brushAnimation = new BrushAnimation()
                {
                    From = nowValue,
                    To = targetValue,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd,
                };

                animatable.BeginAnimation(ShowingForegroundProxyProperty, brushAnimation, HandoffBehavior.SnapshotAndReplace);
            }
            else
            {
                _runningStoryboards ??= new();

                var brushTransitionHelper = new BrushTransitionHelper(nowValue, targetValue, d, ShowingForegroundProxyProperty);

                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd
                };

                var storyboard = new Storyboard();
                Storyboard.SetTarget(doubleAnimation, brushTransitionHelper);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(BrushTransitionHelper.ProgressProperty));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Completed += (s, e) =>
                {
                    _runningStoryboards.Remove(storyboardKey);
                };

                _runningStoryboards[storyboardKey] = storyboard;
                animatable.BeginStoryboard(storyboard, HandoffBehavior.SnapshotAndReplace, true);
            }
        }

        private static void ActiveStateBorderBrush(DependencyObject d, State targetState)
        {
            var storyboardKey = new DependencyObjectAndStateProperty(d, StateProperty.BorderBrush);

            if (_runningStoryboards is not null &&
                _runningStoryboards.ContainsKey(storyboardKey))
            {
                _runningStoryboards[storyboardKey].Stop();
                _runningStoryboards.Remove(storyboardKey);
            }

            var targetValue = GetStatePropertyClassValue<Brush>(d, targetState, StateProperty.BorderBrush);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingBorderBrushProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingBorderBrushPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingBorderBrush(d);
            animatable.BeginAnimation(ShowingBorderBrushProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalBorderBrushTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverBorderBrushTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedBorderBrushTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedBorderBrushTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedBorderBrushTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActiveBorderBrushTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledBorderBrushTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingBorderBrushPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalBorderBrushEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverBorderBrushEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedBorderBrushEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedBorderBrushEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedBorderBrushEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActiveBorderBrushEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledBorderBrushEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if ((nowValue is null || nowValue.CanFreeze) &&
                (targetValue is null || targetValue.CanFreeze))
            {
                BrushAnimation brushAnimation = new BrushAnimation()
                {
                    From = nowValue,
                    To = targetValue,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd,
                };

                animatable.BeginAnimation(ShowingBorderBrushProxyProperty, brushAnimation, HandoffBehavior.SnapshotAndReplace);
            }
            else
            {
                _runningStoryboards ??= new();

                var brushTransitionHelper = new BrushTransitionHelper(nowValue, targetValue, d, ShowingBorderBrushProxyProperty);

                DoubleAnimation doubleAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = targetTransitionDuration,
                    EasingFunction = targetEasingFunction,
                    FillBehavior = FillBehavior.HoldEnd
                };

                var storyboard = new Storyboard();
                Storyboard.SetTarget(doubleAnimation, brushTransitionHelper);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(BrushTransitionHelper.ProgressProperty));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Completed += (s, e) =>
                {
                    _runningStoryboards.Remove(storyboardKey);
                };

                _runningStoryboards[storyboardKey] = storyboard;
                animatable.BeginStoryboard(storyboard, HandoffBehavior.SnapshotAndReplace, true);
            }
        }

        private static void ActiveStatePadding(DependencyObject d, State targetState)
        {
            var targetValue = GetStatePropertyStructValue<Thickness>(d, targetState, StateProperty.Padding);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingPaddingProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingPaddingPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingPadding(d);
            animatable.BeginAnimation(ShowingPaddingProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalPaddingTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverPaddingTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedPaddingTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedPaddingTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedPaddingTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActivePaddingTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledPaddingTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingPaddingPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalPaddingEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverPaddingEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedPaddingEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedPaddingEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedPaddingEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActivePaddingEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledPaddingEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            ThicknessAnimation thicknessAnimation = new ThicknessAnimation()
            {
                From = nowValue,
                To = targetValue,
                Duration = targetTransitionDuration,
                EasingFunction = targetEasingFunction,
                FillBehavior = FillBehavior.HoldEnd,
            };

            animatable.BeginAnimation(ShowingPaddingProxyProperty, thicknessAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        private static void ActiveStateBorderThickness(DependencyObject d, State targetState)
        {
            var targetValue = GetStatePropertyStructValue<Thickness>(d, targetState, StateProperty.BorderThickness);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingBorderThicknessProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingBorderThicknessPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingBorderThickness(d);
            animatable.BeginAnimation(ShowingBorderThicknessProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalBorderThicknessTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverBorderThicknessTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedBorderThicknessTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedBorderThicknessTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedBorderThicknessTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActiveBorderThicknessTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledBorderThicknessTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingBorderThicknessPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalBorderThicknessEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverBorderThicknessEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedBorderThicknessEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedBorderThicknessEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedBorderThicknessEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActiveBorderThicknessEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledBorderThicknessEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            ThicknessAnimation thicknessAnimation = new ThicknessAnimation()
            {
                From = nowValue,
                To = targetValue,
                Duration = targetTransitionDuration,
                EasingFunction = targetEasingFunction,
                FillBehavior = FillBehavior.HoldEnd,
            };

            animatable.BeginAnimation(ShowingBorderThicknessProxyProperty, thicknessAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        private static void ActiveStateCornerRadius(DependencyObject d, State targetState)
        {
            var targetValue = GetStatePropertyStructValue<CornerRadius>(d, targetState, StateProperty.CornerRadius);

            if (d is not FrameworkElement animatable ||
                d.ReadLocalValue(ShowingCornerRadiusProperty) == DependencyProperty.UnsetValue)
            {
                d.SetValue(ShowingCornerRadiusPropertyKey, targetValue);
                return;
            }

            var nowValue = GetShowingCornerRadius(d);
            animatable.BeginAnimation(ShowingCornerRadiusProxyProperty, null);

            var targetTransitionDuration = targetState switch
            {
                State.Normal => GetNormalCornerRadiusTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Hover => GetHoverCornerRadiusTransitionDuration(d) ?? GetHoverTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Pressed => GetPressedCornerRadiusTransitionDuration(d) ?? GetPressedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Checked => GetCheckedCornerRadiusTransitionDuration(d) ?? GetCheckedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Selected => GetSelectedCornerRadiusTransitionDuration(d) ?? GetSelectedTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.SelectedActive => GetSelectedActiveCornerRadiusTransitionDuration(d) ?? GetSelectedActiveTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                State.Disabled => GetDisabledCornerRadiusTransitionDuration(d) ?? GetDisabledTransitionDuration(d) ?? GetDefaultTransitionDuration(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            if (targetTransitionDuration.TimeSpan == TimeSpan.Zero)
            {
                d.SetValue(ShowingCornerRadiusPropertyKey, targetValue);
                return;
            }

            var targetEasingFunction = targetState switch
            {
                State.Normal => GetNormalCornerRadiusEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Hover => GetHoverCornerRadiusEasingFunction(d) ?? GetHoverEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Pressed => GetPressedCornerRadiusEasingFunction(d) ?? GetPressedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Checked => GetCheckedCornerRadiusEasingFunction(d) ?? GetCheckedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Selected => GetSelectedCornerRadiusEasingFunction(d) ?? GetSelectedEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.SelectedActive => GetSelectedActiveCornerRadiusEasingFunction(d) ?? GetSelectedActiveEasingFunction(d) ?? GetDefaultEasingFunction(d),
                State.Disabled => GetDisabledCornerRadiusEasingFunction(d) ?? GetDisabledEasingFunction(d) ?? GetDefaultEasingFunction(d),
                _ => throw new InvalidOperationException("Invalid State")
            };

            CornerRadiusAnimation cornerRadiusAnimation = new CornerRadiusAnimation()
            {
                From = nowValue,
                To = targetValue,
                Duration = targetTransitionDuration,
                EasingFunction = targetEasingFunction,
                FillBehavior = FillBehavior.HoldEnd,
            };

            animatable.BeginAnimation(ShowingCornerRadiusProxyProperty, cornerRadiusAnimation, HandoffBehavior.SnapshotAndReplace);
        }

    }
}
