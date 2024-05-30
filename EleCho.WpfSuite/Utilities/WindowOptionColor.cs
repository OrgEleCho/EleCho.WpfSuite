using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    [TypeConverter(typeof(WindowOptionColorConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowOptionColor
    {
        private int _value;

        public int Value
        {
            get => _value;
            set => _value = value;
        }

        public byte R
        {
            get => (byte)(_value);
            set => _value = (_value & ~0xFF) + value;
        }

        public byte G
        {
            get => (byte)(_value >> 8);
            set => _value = (_value & ~0xFF00) + (value << 8);
        }

        public byte B
        {
            get => (byte)(_value >> 16);
            set => _value = (_value & ~0xFF0000) + (value << 16);
        }

        public byte Extra
        {
            get => (byte)(_value >> 24);
            set => _value = (int)(_value & ~0xFF000000) + (value << 24);
        }

        public WindowOptionColor()
        {

        }

        public WindowOptionColor(byte r, byte g, byte b) : this(r, g, b, 0xFF)
        { }

        public WindowOptionColor(byte r, byte g, byte b, byte extra)
        {
            _value = r | g << 8 | b << 16 | extra << 24;
        }

        public WindowOptionColor(int value)
        {
            _value = value;
        }

        public static WindowOptionColor Default { get; } = new WindowOptionColor(unchecked((int)0xFFFFFFFF));
        public static WindowOptionColor None { get; } = new WindowOptionColor(unchecked((int)0xFFFFFFFE));

        public static implicit operator Color(WindowOptionColor color) => Color.FromArgb(0xFF, color.R, color.G, color.B);
        public static implicit operator WindowOptionColor(Color color) => new WindowOptionColor(color.R, color.G, color.B, 0);
    }
}
