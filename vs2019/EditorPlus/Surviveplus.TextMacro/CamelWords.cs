using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Net.Surviveplus.TextMacro
{
    public class CamelWords
    {
        private static Regex reg;

        public static IEnumerable<string> GetWords(string text)
        {
            if(CamelWords.reg == null){
                CamelWords.reg = new Regex("([a-z])([A-Z])");
            } // end if

            var r =
                from word in text.Split(new string[] { " ", "\t", "-", "_", "\"", "'" }, StringSplitOptions.RemoveEmptyEntries)
                from word2 in CamelWords.reg.Replace(word, "$1\n$2").Split('\n')
                select word2;

            return r;
        } // end function

        public static string GetUpperCamelWord( string text)
        {
            var r =
                from word in CamelWords.GetWords(text)
                select Strings.Left(word, 1).ToUpper() + Strings.Mid(word, 2).ToLower();

            return string.Join("", r);
        } // end function

        public static string GetLowerCamelWord(string text)
        {
            var pascal = CamelWords.GetUpperCamelWord(text);
            return Strings.Left(pascal, 1).ToLower() + Strings.Mid(pascal, 2);
        } // end function
    }
}
