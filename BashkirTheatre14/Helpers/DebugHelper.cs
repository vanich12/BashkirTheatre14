using System.Diagnostics;

namespace BashkirTheatre14.Helpers
{
    public static class DebugHelper
    {
        public static bool IsRunningInDebugMode { get; private set; }

        static DebugHelper()
        {
            Initialize();
        }

        [Conditional("DEBUG")]
        private static void Initialize()
        {
            IsRunningInDebugMode = true;
        }
    }
}
