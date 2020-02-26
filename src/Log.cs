using System.IO;
using System;

namespace Wolf_Server_Manager
{
    public static class Log
    {
        public static string InfoFilePath;
        public static string DebugFilePath;

        readonly static object InfoResourceLock = new object();
        readonly static object DebugResourceLock = new object();

        public static void InitInfoLog(string filePath)
        {
            InfoFilePath = filePath;
        }

        public static void InitDebugLog(string filePath)
        {
            DebugFilePath = filePath;
        }

        // TODO: Info log should probably use a stream for performance reasons (if it is used regularly, currently it is not).
        public static void Info(string text)
        {
            lock (InfoResourceLock)
            {
                File.AppendAllText(InfoFilePath, Environment.NewLine + text + Environment.NewLine);
            }
        }

        // NOTE: Debug log doesn't use a stream since we don't want buffered data that may not get released to the disc if a crash happens.
        // Using File.AppendAllText will immediately write the bytes to disc, no buffered writing every 4096 bytes or w/e (default for FileStream).
#if DEBUG
        public static void Debug(string text)
        {
            lock (DebugResourceLock)
            {
                File.AppendAllText(DebugFilePath, text + Environment.NewLine);
            }
        }
#else
        public static void Debug(string text)
        {
        }
#endif
    }
}
