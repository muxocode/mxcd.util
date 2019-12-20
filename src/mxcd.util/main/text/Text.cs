using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static mxcd.util.enums.UtilEnums;

namespace mxcd.util.text
{
    public sealed class StringChecker
    {
        string Text;
        public StringChecker(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Check if a string is a valid email
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid email or not </returns>
        public bool IsEmail()
        {
            var match = Regex.Match(this.Text, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Check if a string is a valid phone
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid phone or not </returns>
        public bool IsPhone()
        {
            var match = Regex.Match(Text, @"[0-9]{7,}", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Check if a string is a valid mobile
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> If it's a valid phone or not </returns>
        public bool IsMobile()
        {
            var match = Regex.Match(Text, @"[6-7]{1}[0-9]{6,}", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Format a string according to a given input mask
        /// </summary>
        /// <param name = "text"> The input text. </param>
        /// <param name = "Mask"> The input mask. eg "A ## - ## - T - ### Z" </param>
        /// <example>
        /// var s = "aaaaaaaabbbbccccddddeeeeeeeeeeee" .FormatWithMask ("Hello ######## - # A ### - #### - #### - ############ Oww ");
        /// "Hello aaaaaaaa-bAbbb-cccc-dddd-eeeeeeeeeeee Oww";
        /// var s = "abc" .FormatWithMask ("### - #");
        /// "abc-";
        /// var s = "" .FormatWithMask ("Hello ######## - # A ### - #### - #### - ############ Oww ");
        /// "";
        /// </example>
        /// <returns> The formatted text </returns>
        public string IsFormatWithMask(string Mask)
        {
            if (string.IsNullOrEmpty(Text)) return Text;
            var output = string.Empty;
            var index = 0;
            foreach (var m in Mask)
            {
                if (m == '#')
                {
                    if (index < Text.Length)
                    {
                        output += Text[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }
        /// <summary>
        /// Check if a string is a valid ISIN (International Securities Identification Number)
        /// <param name = "text"> the text to check </param>
        /// <returns> If it is a valid ISIN or not ... </returns>
        /// </summary>
        public bool IsIsin()
        {
            Regex Pattern = new Regex("[A-Z]{2}([A-Z0-9]){10}", RegexOptions.Compiled);

            if (string.IsNullOrEmpty(Text))
            {
                return false;
            }
            if (!Pattern.IsMatch(Text))
            {
                return false;
            }

            var digits = new int[22];
            int index = 0;
            for (int i = 0; i < 11; i++)
            {
                char c = Text[i];
                if (c >= '0' && c <= '9')
                {
                    digits[index++] = c - '0';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    int n = c - 'A' + 10;
                    int tens = n / 10;
                    if (tens != 0)
                    {
                        digits[index++] = tens;
                    }
                    digits[index++] = n % 10;
                }
                else
                {
                    // Not a digit or upper-case letter.
                    return false;
                }
            }
            int sum = 0;
            for (int i = 0; i < index; i++)
            {
                int digit = digits[index - 1 - i];
                if (i % 2 == 0)
                {
                    digit *= 2;
                }
                sum += digit / 10;
                sum += digit % 10;
            }

            int checkDigit = Text[11] - '0';
            if (checkDigit < 0 || checkDigit > 9)
            {
                // Not a digit.
                return false;
            }
            int tensComplement = (sum % 10 == 0) ? 0 : ((sum / 10) + 1) * 10 - sum;
            return checkDigit == tensComplement;
        }

        /// <summary>
        /// Find out if the last text is a valid URL
        /// </summary>
        /// <param name = "text"> </param>
        /// <returns> true in case it is </returns>
        public bool IsUrl()
        {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(Text);
        }
    }

    public sealed class HtmlUtil
    {
        string Text;
        public HtmlUtil(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Removes html tags
        /// </summary>
        /// <param name = "text"> </param>
        /// <example>
        /// var htmlText = "<p> Text that remains. <span class =" bold "> Bold. </span> Values ​​</p>;
        /// result: Text that is maintained. Bold font. Values
        /// </example>
        /// <returns> </returns>
        public string RemoveTags()
        {
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(Text, " ");
        }
    }

    /// <summary>
    /// Funciones para String
    /// </summary>
    public static class Text
    {
        public static StringChecker Check(this string text)
        {
            return new StringChecker(text);
        }

        public static HtmlUtil Html(this string text)
        {
            return new HtmlUtil(text);
        }

        /// <summary>
        /// Convert an array byte to string
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <param name="encode">encode</param>
        /// <returns></returns>
        public static string ToString(this Byte[] bytes, Encode encode)
        {
            string result = null;

            switch (encode)
            {
                case Encode.UTF8:
                    result = System.Text.Encoding.UTF8.GetString(bytes);
                    break;
                case Encode.Base64:
                    result = Convert.ToBase64String(bytes, 0, bytes.Length);
                    break;
                case Encode.Bit:
                    result = BitConverter.ToString(bytes);
                    break;
                case Encode.ASCII:
                    result = new string(ASCIIEncoding.Default.GetChars(bytes));
                    break;
            }

            return result;
        }

        /// <summary> Converts a camelCase into a sentence
        /// </summary>
        /// <example>
        /// input : HelloWorld
        /// output : Hello World
        /// </example>
        /// <example>
        /// input : BBC
        /// output : BBC
        /// </example>
        /// <param name="text"></param>
        /// <returns>una sentencia</returns>
        public static string ToSentence(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            //return as is if the input is just an abbreviation
            if (Regex.Match(text, "[0-9A-Z]+$").Success)
                return text;
            //add a space before each capital letter, but not the first one.
            var result = Regex.Replace(text, "(\\B[A-Z])", " $1");
            return result;
        }

        /// <summary>
        /// Get last positions from a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="number"></param>
        /// <returns>El string resultado</returns>
        public static string Last(this string text, int number)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var value = text.Trim();
            return number >= value.Length ? value : value.Substring(value.Length - number);
        }

        /// <summary>
        /// Get first positions from a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="number"></param>
        /// <returns>El string resultado</returns>
        public static string First(this string text, int number)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var value = text.Trim();
            return number >= value.Length ? value : text.Substring(0, number);
        }

        /// <summary>
        /// Find out if a text contains any of the characters passed as parameters
        /// </summary>
        /// <param name = "text"> </param>
        /// <param name = "characters"> </param>
        /// <returns> true if it contains any of the Characters </returns>
        public static bool ContainsAny(this string text, char[] characters)
        {
            foreach (char character in characters)
            {
                if (text.Contains(character.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Repeat the string as many times as the parameters indicate
        /// </summary>
        /// <param name = "text"> </param>
        /// <param name = "number"> </param>
        /// <returns> </returns>
        public static string Repeat(this string text, int number)
        {
            if (text == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            for (var repeat = 0; repeat < number; repeat++)
            {
                sb.Append(text);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert a text to a byte array
        /// </summary>
        /// <param name = "text"> </param>
        /// <param name = "codification"> </param>
        /// <returns> </returns>
        public static Byte[] ToBytes(this string text, Encode codification = Encode.UTF8)
        {
            Byte[] Resultado = null;

            switch (codification)
            {
                case Encode.UTF8:
                    Resultado = System.Text.Encoding.UTF8.GetBytes(text);
                    break;
                case Encode.ASCII:
                    Resultado = ASCIIEncoding.Default.GetBytes(text);
                    break;
                case Encode.Base64:
                    Resultado = Convert.FromBase64String(text);
                    break;
            }

            return Resultado;
        }

        static Random random;
        static void initRandom()
        {
            random = random ?? new Random();
        }
        /// <summary>
        /// Gets a randomize string with size
        /// </summary>
        /// <returns></returns>
        public static string Random(int size = 7, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            initRandom();
            return new string(Enumerable.Repeat(chars, size).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Randomize a text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Random(this string text)
        {
            return Random(text.Length, text);
        }
        /// <summary>
        /// Find all the indexes where the searched text appears within the String
        /// </summary>
        /// <param name = "text"> </param>
        /// <param name = "textSearch"> </param>
        /// <returns> </returns>
        public static IEnumerable<int> GetIndexOf(this string text, string textSearch)
        {
            List<int> colEncontrados = new List<int>();
            if (string.IsNullOrEmpty(textSearch))
                return colEncontrados;

            int lastLoc = text.IndexOf(textSearch);
            while (lastLoc != -1)
            {
                colEncontrados.Add(lastLoc);
                lastLoc = text.IndexOf(textSearch, startIndex: lastLoc + 1);
            }

            return colEncontrados;
        }
    }
}
