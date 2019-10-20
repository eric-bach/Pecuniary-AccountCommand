using System.Diagnostics;

namespace Logging.LambdaLogger
{
    public static class Logger
    {
        public static void Log(string message)
        {
            var stack = new StackTrace();
            var method = stack.GetFrame(1).GetMethod();
            var nameSpace = method?.ReflectedType?.Namespace;

            Amazon.Lambda.Core.LambdaLogger.Log($"[{nameSpace}] {message}");
        }
    }
}
