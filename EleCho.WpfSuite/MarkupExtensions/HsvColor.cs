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
    /// <summary>
    /// Provide a value of <see cref="Color"/>
    /// </summary>
    [MarkupExtensionReturnType(typeof(Color))]
    public class HsvColor : MarkupExtension
    {
        /// <summary>
        /// Hue
        /// </summary>
        public float H { get; set; }

        /// <summary>
        /// Situation
        /// </summary>
        public float S { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public float V { get; set; }

        /// <summary>
        /// Opacity
        /// </summary>
        public float Opacity { get; set; } = 1;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ColorUtils.HSV2RGB(H, S, V, out var r, out var g, out var b);

            return Color.FromScRgb(Opacity, r, g, b);
        }
    }
}
