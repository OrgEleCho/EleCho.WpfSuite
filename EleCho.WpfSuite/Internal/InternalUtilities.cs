using System.Diagnostics;
using System.Reflection;

namespace EleCho.WpfSuite
{
    internal static class InternalUtilities
    {
        private static bool? _cachedIsApplicationInDebugMode;

        public static bool IsApplicationInDebugMode()
        {
            if (_cachedIsApplicationInDebugMode.HasValue)
            {
                return _cachedIsApplicationInDebugMode.Value;
            }

            if (Assembly.GetEntryAssembly() is not { } entryAssembly ||
                entryAssembly.GetCustomAttribute<DebuggableAttribute>() is not { } debuggableAttribute ||
                !debuggableAttribute.DebuggingFlags.HasFlag(DebuggableAttribute.DebuggingModes.EnableEditAndContinue))
            {
                _cachedIsApplicationInDebugMode = false;
                return false;
            }

            _cachedIsApplicationInDebugMode = true;
            return true;
        }
    }
}
