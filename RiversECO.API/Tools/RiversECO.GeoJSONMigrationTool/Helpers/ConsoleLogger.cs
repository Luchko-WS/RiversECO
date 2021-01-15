using System;
using System.Text;

namespace RiversECO.Tools.GeoJSONMigrationTool.Helpers
{
    internal class ConsoleLogger
    {
        private static ConsoleColor _foreColorBefore = Console.ForegroundColor;

        public static void WriteText(string message)
        {
            WriteInConsoleWithColor(message, ConsoleColor.White);
        }

        public static void WriteSuccess(string message)
        {
            WriteInConsoleWithColor(message, ConsoleColor.Green);
        }

        public static void WriteWarning(string message, Exception exception = null)
        {
            if (exception != null)
            {
                message = GetFormattedMessageWithException(message, exception);
            }

            WriteInConsoleWithColor(message, ConsoleColor.Yellow);
        }

        public static void WriteError(string message, Exception exception = null)
        {
            if (exception != null)
            {
                message = GetFormattedMessageWithException(message, exception);
            }

            WriteInConsoleWithColor(message, ConsoleColor.Red);
        }

        private static void WriteInConsoleWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = _foreColorBefore;
        }

        private static string GetFormattedMessageWithException(string message, Exception exception)
        {
            var messageBuilder = new StringBuilder();

            messageBuilder.AppendLine(message);
            messageBuilder.Append(exception.ToString());

            return messageBuilder.ToString();
        }
    }
}
