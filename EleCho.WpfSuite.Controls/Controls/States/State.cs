#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.States
{
    public enum State
    {
        Normal,

        // focused
        Focused,

        // hover
        Hover,

        // button
        Pressed,

        // thumb
        Dragging,

        // checkbox / toggle button
        Checked,

        // selection
        Selected,
        SelectedActive,
        SelectedFocused,

        // other hover
        FocusedHover,
        CheckedHover,
        SelectedHover,
        SelectedActiveHover,
        SelectedFocusedHover,

        // highlighted
        Highlighted,

        // disabled
        Disabled,

        // disabled
        HighlightedDisabled
    }
}
