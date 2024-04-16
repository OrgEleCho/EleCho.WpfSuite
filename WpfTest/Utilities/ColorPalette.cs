using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfTest.Utilities
{
    public class ColorPalette : ResourceDictionary
    {
        private Color _primaryColor = Color.FromRgb(0, 136, 255);
        private Color _lightColor = Colors.White;
        private Color _darkColor = Colors.Black;
        private bool _darkMode = false;

        public ColorPalette()
        {
            PopulatePalette();
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

        public static void RGB2HSV(
            float r, float g, float b,
            out float h, out float s, out float v)
        {
            float max = Math.Max(Math.Max(r, g), b);
            float min = Math.Min(Math.Min(r, g), b);
            float delta = max - min;

            // Calculate hue
            if (delta == 0)
            {
                h = 0; // Grayscale
            }
            else if (max == r)
            {
                h = (g - b) / delta % 6;
            }
            else if (max == g)
            {
                h = (b - r) / delta + 2;
            }
            else
            {
                h = (r - g) / delta + 4;
            }

            h /= 6.0f;
            if (h < 0)
            {
                h += 1.0f;
            }

            // Calculate saturation
            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = delta / max;
            }

            // Calculate value
            v = max;
        }
        public static void HSL2RGB(
            float h, float s, float l,
            out float r, out float g, out float b)
        {
            if (s == 0)
            {
                // 如果饱和度为0，颜色将变为灰度。
                r = g = b = l;
            }
            else
            {
                float v;
                r = l; // 默认为灰色
                g = l;
                b = l;
                v = (l <= 0.5f) ? (l * (1.0f + s)) : (l + s - l * s);
                if (v > 0)
                {
                    float m;
                    float sv;
                    int sextant;
                    float fract, vsf, mid1, mid2;
                    m = l + l - v;
                    sv = (v - m) / v;
                    h *= 6.0f;
                    sextant = (int)h;
                    fract = h - sextant;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    switch (sextant)
                    {
                        case 0:
                            r = v;
                            g = mid1;
                            b = m;
                            break;
                        case 1:
                            r = mid2;
                            g = v;
                            b = m;
                            break;
                        case 2:
                            r = m;
                            g = v;
                            b = mid1;
                            break;
                        case 3:
                            r = m;
                            g = mid2;
                            b = v;
                            break;
                        case 4:
                            r = mid1;
                            g = m;
                            b = v;
                            break;
                        default:
                            r = v;
                            g = m;
                            b = mid2;
                            break;
                    }
                }
                r *= 255.0f;
                g *= 255.0f;
                b *= 255.0f;
            }
        }

        public static void RGB2HSL(
            float r, float g, float b,
            out float h, out float s, out float l)
        {
            float max = Math.Max(Math.Max(r, g), b);
            float min = Math.Min(Math.Min(r, g), b);
            float delta = max - min;

            // 计算色调
            if (delta == 0)
            {
                h = 0; // 灰度
            }
            else if (max == r)
            {
                h = (g - b) / delta % 6;
            }
            else if (max == g)
            {
                h = (b - r) / delta + 2;
            }
            else
            {
                h = (r - g) / delta + 4;
            }

            h /= 6.0f;
            if (h < 0)
            {
                h += 1.0f;
            }

            // 计算饱和度
            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = delta / max;
            }

            // 计算亮度
            l = (max + min) / 2.0f;
        }

        public static Color MixColor(Color a, Color b, float t)
        {
            float
                scr = Lerp(a.ScR, b.ScR, t),
                scg = Lerp(a.ScG, b.ScG, t),
                scb = Lerp(a.ScB, b.ScB, t);

            return Color.FromScRgb(1, scr, scg, scb);

            //RGB2HSV(a.ScR, a.ScG, a.ScB, out var ha, out var sa, out var va);
            //RGB2HSV(b.ScR, b.ScG, b.ScB, out var hb, out var sb, out var vb);

            //if (sa == 0)
            //{
            //    ha = hb;
            //}
            //else if (sb == 0)
            //{
            //    hb = ha;
            //}

            //if (a.R == 0 && a.G == 0 && a.B == 0)
            //{
            //    sa = sb;
            //    ha = hb;
            //}
            //else if (b.R == 0 && b.G == 0 && b.B == 0)
            //{
            //    sb = sa;
            //    hb = ha;
            //}

            //float
            //    h = Lerp(ha, hb, t),
            //    s = Lerp(sa, sb, t),
            //    v = Lerp(va, vb, t);

            //HSV2RGB(h, s, v, out var outR, out var outG, out var outB);

            //return Color.FromScRgb(1, outR, outG, outB);

            static float Lerp(float a, float b, float t)
            {
                return b * t + a * (1 - t);
            }
        }

        public bool DarkMode
        {
            get => _darkMode; set
            {
                _darkMode = value;
                PopulatePalette();
            }
        }

        public Color PrimaryColor
        {
            get => _primaryColor; set
            {
                _primaryColor = value;
                PopulatePalette();
            }
        }
        public Color LightColor
        {
            get => _lightColor; set
            {
                _lightColor = value;
                PopulatePalette();
            }
        }
        public Color DarkColor
        {
            get => _darkColor; set
            {
                _darkColor = value;
                PopulatePalette();
            }
        }
        public Brush PrimaryBrush => new SolidColorBrush(PrimaryColor);
        public Brush LightBrush => new SolidColorBrush(LightColor);
        public Brush DarkBrush => new SolidColorBrush(DarkColor);

        public Color ForegroundColor1 => DarkMode ? LightColor : DarkColor;
        public Color ForegroundColor2 => DarkMode ? LightColor : DarkColor;
        public Color ForegroundColor3 => DarkMode ? LightColor : DarkColor;
        public Color ForegroundColor4 => DarkMode ? LightColor : DarkColor;
        public Color ForegroundColor5 => DarkMode ? LightColor : DarkColor;
        public Color ForegroundColor6 => DarkMode ? DarkColor : LightColor;
        public Color ForegroundColor7 => DarkMode ? DarkColor : LightColor;
        public Color ForegroundColor8 => DarkMode ? DarkColor : LightColor;
        public Color ForegroundColor9 => DarkMode ? DarkColor : LightColor;

        public Color BackgroundColor1 => DarkMode ? MixColor(DarkColor, PrimaryColor, 0.2f) : MixColor(LightColor, PrimaryColor, 0.2f);
        public Color BackgroundColor2 => DarkMode ? MixColor(DarkColor, PrimaryColor, 0.4f) : MixColor(LightColor, PrimaryColor, 0.4f);
        public Color BackgroundColor3 => DarkMode ? MixColor(DarkColor, PrimaryColor, 0.6f) : MixColor(LightColor, PrimaryColor, 0.6f);
        public Color BackgroundColor4 => DarkMode ? MixColor(DarkColor, PrimaryColor, 0.8f) : MixColor(LightColor, PrimaryColor, 0.8f);
        public Color BackgroundColor5 => PrimaryColor;
        public Color BackgroundColor6 => DarkMode ? MixColor(PrimaryColor, LightColor, 0.2f) : MixColor(PrimaryColor, DarkColor, 0.2f);
        public Color BackgroundColor7 => DarkMode ? MixColor(PrimaryColor, LightColor, 0.4f) : MixColor(PrimaryColor, DarkColor, 0.4f);
        public Color BackgroundColor8 => DarkMode ? MixColor(PrimaryColor, LightColor, 0.6f) : MixColor(PrimaryColor, DarkColor, 0.6f);
        public Color BackgroundColor9 => DarkMode ? MixColor(PrimaryColor, LightColor, 0.8f) : MixColor(PrimaryColor, DarkColor, 0.8f);

        public Brush ForegroundBrush1 => new SolidColorBrush(ForegroundColor1);
        public Brush ForegroundBrush2 => new SolidColorBrush(ForegroundColor2);
        public Brush ForegroundBrush3 => new SolidColorBrush(ForegroundColor3);
        public Brush ForegroundBrush4 => new SolidColorBrush(ForegroundColor4);
        public Brush ForegroundBrush5 => new SolidColorBrush(ForegroundColor5);
        public Brush ForegroundBrush6 => new SolidColorBrush(ForegroundColor6);
        public Brush ForegroundBrush7 => new SolidColorBrush(ForegroundColor7);
        public Brush ForegroundBrush8 => new SolidColorBrush(ForegroundColor8);
        public Brush ForegroundBrush9 => new SolidColorBrush(ForegroundColor9);

        public Brush BackgroundBrush1 => new SolidColorBrush(BackgroundColor1);
        public Brush BackgroundBrush2 => new SolidColorBrush(BackgroundColor2);
        public Brush BackgroundBrush3 => new SolidColorBrush(BackgroundColor3);
        public Brush BackgroundBrush4 => new SolidColorBrush(BackgroundColor4);
        public Brush BackgroundBrush5 => new SolidColorBrush(BackgroundColor5);
        public Brush BackgroundBrush6 => new SolidColorBrush(BackgroundColor6);
        public Brush BackgroundBrush7 => new SolidColorBrush(BackgroundColor7);
        public Brush BackgroundBrush8 => new SolidColorBrush(BackgroundColor8);
        public Brush BackgroundBrush9 => new SolidColorBrush(BackgroundColor9);

        protected virtual void PopulatePalette()
        {
            this[nameof(PrimaryColor)] = PrimaryColor;
            this[nameof(LightColor)] = LightColor;
            this[nameof(DarkColor)] = DarkColor;

            this[nameof(ForegroundColor1)] = ForegroundColor1;
            this[nameof(ForegroundColor2)] = ForegroundColor2;
            this[nameof(ForegroundColor3)] = ForegroundColor3;
            this[nameof(ForegroundColor4)] = ForegroundColor4;
            this[nameof(ForegroundColor5)] = ForegroundColor5;
            this[nameof(ForegroundColor6)] = ForegroundColor6;
            this[nameof(ForegroundColor7)] = ForegroundColor7;
            this[nameof(ForegroundColor8)] = ForegroundColor8;
            this[nameof(ForegroundColor9)] = ForegroundColor9;

            this[nameof(BackgroundColor1)] = BackgroundColor1;
            this[nameof(BackgroundColor2)] = BackgroundColor2;
            this[nameof(BackgroundColor3)] = BackgroundColor3;
            this[nameof(BackgroundColor4)] = BackgroundColor4;
            this[nameof(BackgroundColor5)] = BackgroundColor5;
            this[nameof(BackgroundColor6)] = BackgroundColor6;
            this[nameof(BackgroundColor7)] = BackgroundColor7;
            this[nameof(BackgroundColor8)] = BackgroundColor8;
            this[nameof(BackgroundColor9)] = BackgroundColor9;

            this[nameof(PrimaryBrush)] = PrimaryBrush;
            this[nameof(LightBrush)] = LightBrush;
            this[nameof(DarkBrush)] = DarkBrush;

            this[nameof(ForegroundBrush1)] = ForegroundBrush1;
            this[nameof(ForegroundBrush2)] = ForegroundBrush2;
            this[nameof(ForegroundBrush3)] = ForegroundBrush3;
            this[nameof(ForegroundBrush4)] = ForegroundBrush4;
            this[nameof(ForegroundBrush5)] = ForegroundBrush5;
            this[nameof(ForegroundBrush6)] = ForegroundBrush6;
            this[nameof(ForegroundBrush7)] = ForegroundBrush7;
            this[nameof(ForegroundBrush8)] = ForegroundBrush8;
            this[nameof(ForegroundBrush9)] = ForegroundBrush9;

            this[nameof(BackgroundBrush1)] = BackgroundBrush1;
            this[nameof(BackgroundBrush2)] = BackgroundBrush2;
            this[nameof(BackgroundBrush3)] = BackgroundBrush3;
            this[nameof(BackgroundBrush4)] = BackgroundBrush4;
            this[nameof(BackgroundBrush5)] = BackgroundBrush5;
            this[nameof(BackgroundBrush6)] = BackgroundBrush6;
            this[nameof(BackgroundBrush7)] = BackgroundBrush7;
            this[nameof(BackgroundBrush8)] = BackgroundBrush8;
            this[nameof(BackgroundBrush9)] = BackgroundBrush9;
        }
    }
}
