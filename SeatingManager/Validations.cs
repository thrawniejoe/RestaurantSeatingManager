using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeatingManager
{
    class Validations
    {
        public static bool CheckEmptyString(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            return true;
        }

        public static bool CheckStringMinMax(string text, int min, int max)
        {
            var test = text.Trim();

            if (text.Length >= min && text.Length <= max)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfAlpha(string text)
        {
            if (Regex.IsMatch(text.ToString(), "[a-z]", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfNumeric(string text)
        {
            if (Regex.IsMatch(text.ToString(), "[0-9]"))
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfExact(string text, int min)
        {
            
            if (text.Trim().Length == min)
            {
                return true;
            }
            return false;
        }


    }
}
