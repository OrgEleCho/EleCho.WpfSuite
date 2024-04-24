using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace EleCho.WpfSuite.Markup
{
    [MarkupExtensionReturnType(typeof(Color))]
    public class HsvColor : MarkupExtension
    {
        public float H { get; set; }
        public float S { get; set; }
        public float V { get; set; }

        public float Opacity { get; set; } = 1;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ColorUtils.HSV2RGB(H, S, V, out var r, out var g, out var b);

            return Color.FromScRgb(Opacity, r, g, b);
        }
    }
}
