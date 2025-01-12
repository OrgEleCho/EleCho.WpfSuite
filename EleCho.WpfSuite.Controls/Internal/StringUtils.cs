using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.WpfSuite.Internal
{
    internal static class StringUtils
    {
        public static bool IsAsciiLetter(char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

        public static bool IsAsciiNumber(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsIdentifier(string? value)
        {
            if (value is null)
            {
                return false;
            }

            if (value.Length == 0)
            {
                return false;
            }

            var firstChar = value[0];
            if (!IsAsciiLetter(firstChar) &&
                firstChar != '_')
            {
                return false;
            }

            for (int i = 1; i < value.Length; i++)
            {
                var c = value[i];
                if (!IsAsciiLetter(c) &&
                    !IsAsciiNumber(c) &&
                    c != '_')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
