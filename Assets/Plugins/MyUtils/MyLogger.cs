using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace MyUtils
{
    public static class MyLogger
    {
        [Conditional("ENABLE_LOG")]
        public static void Info(object message)
        {
            Debug.Log(message);
        }

        [Conditional("ENABLE_LOG")]
        public static void Warn(object message)
        {
            Debug.LogWarning(message);
        }

        [Conditional("ENABLE_LOG")]
        public static void Error(object message)
        {
            Debug.LogError(message);
        }
    }
}