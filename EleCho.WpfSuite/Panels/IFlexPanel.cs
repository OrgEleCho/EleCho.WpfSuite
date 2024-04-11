using System.Windows.Controls;

namespace EleCho.WpfSuite
{
    public interface IFlexPanel
    {
        public FlexDirection Direction { get; set; }
        public FlexWrap Wrap { get; set; }
        public FlexMainAlignment MainAlignment { get; set; }
        public FlexCrossAlignment CrossAlignment { get; set; }
        public FlexItemAlignment ItemsAlignment { get; set; }

        public double UniformGrow { get; set; }
        public double UniformShrink { get; set; }

        internal UIElementCollection InternalChildren { get; }
        internal FlexPanelAttachedProperties AttachedProperties { get; }
    }
}
