using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.Helpers
{
    public static class StringTruncatorHelper
    {
        public static string TruncateToSingleLine(string inputString)
        {
            var maxCharacters = 40;

            if (string.IsNullOrWhiteSpace(inputString))
            {
                return string.Empty;
            }

            string firstLine = inputString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)[0];

            if (firstLine.Length > maxCharacters)
            {
                return firstLine.Substring(0, maxCharacters - 3) + "...";
            }

            return firstLine;
        }
    }
}
