namespace EleCho.WpfSuite
{
    /// <summary>
    /// Color utilities
    /// </summary>
    public static class ColorUtils
    {
        /// <summary>
        /// Convert HSV color to RGB color
        /// </summary>
        /// <param name="h">hue</param>
        /// <param name="s">situation</param>
        /// <param name="v">value</param>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
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
