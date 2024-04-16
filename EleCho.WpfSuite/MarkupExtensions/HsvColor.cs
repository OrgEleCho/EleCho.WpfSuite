using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    public class HsvColor : MarkupExtension
    {
        public float H { get; set; }
        public float S { get; set; }
        public float V { get; set; }

        public float Opacity { get; set; } = 1;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            HSV2RGB(H, S, V, out var r, out var g, out var b);

            return Color.FromScRgb(Opacity, r, g, b);
        }

        public static void HSV2RGB(
            float h, float s, float v,
            out float r, out float g, out float b)
        {
            if (s == 0)
            {
                // If saturation is 0, the color is grayscale.
                r = g = b = v;
            }
            else
            {
                float hue = h * 6.0f;
                int sector = (int)hue;
                float fraction = hue - sector;
                float p = v * (1.0f - s);
                float q = v * (1.0f - s * fraction);
                float t = v * (1.0f - s * (1.0f - fraction));

                switch (sector)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;
                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }
        }
    }
}
