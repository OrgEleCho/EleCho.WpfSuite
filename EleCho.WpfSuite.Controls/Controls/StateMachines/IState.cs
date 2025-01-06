using System.Windows;
using System.Windows.Media;

#pragma warning disable CS1591

namespace EleCho.WpfSuite.Controls.StateMachines
{
    public interface IState
    {
        public string? Name { get; }

        public Brush Background { get; }
        public Brush Foreground { get; }
        public Brush BorderBrush { get; }

        public Thickness Padding { get; }
        public Thickness Margin { get; }
        public Thickness BorderThickness { get; }
        public CornerRadius CornerRadius { get; }
    }
}
