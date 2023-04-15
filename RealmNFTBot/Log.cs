using Newtonsoft.Json.Linq;
using Pastel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RealmNFTBot
{
    class Log
    {
        private static readonly object _ConsoleLock = new object();
        public enum LogType
        {
            Success,
            Information,
            Error,
            CriticalError,
            Warning
        };

        /// <summary>
        /// Writes text to the log
        /// </summary>
        /// <param name="Message">The text to write to the log</param>
        /// <param name="logType">1 = Success / Green, 2 = Information / Default Color, 3 = Error / Red, 4 = Critical Error / DarkViolet, 5 = Warning / Orange, default = Default Color</param>
        public static void WriteToLog(string message, LogType logType = LogType.Information, bool debugOnly = false)
        {
            string messagePrefix = $"[{ DateTime.Now }] ";

            Color textColor;

            switch (logType)
            {
                case LogType.Success:
                    textColor = Color.Green;
                    break;
                case LogType.Information:
                    textColor = Color.LightGray;
                    break;
                case LogType.Error:
                    textColor = Color.Red;
                    messagePrefix += "Error: ".Pastel(textColor);
                    break;
                case LogType.CriticalError:
                    textColor = Color.Magenta;
                    messagePrefix += "Critical Error: ".Pastel(textColor);
                    break;
                case LogType.Warning:
                    textColor = Color.Yellow;
                    messagePrefix += "Warning: ".Pastel(textColor);
                    break;
                default:
                    textColor = Color.LightGray;
                    break;
            }

            lock (_ConsoleLock)
            {
                Console.WriteLine(messagePrefix + message.Pastel(textColor));

                //if (Settings.WriteLogToFile)
                //{
                //    System.IO.File.AppendAllText(Settings.StartupPath + @"/log.txt", messagePrefix + message + Environment.NewLine);
                //}
            }
        }
    }
}