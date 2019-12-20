using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mxcd.util.expresion.text.ParentesisValidator
{
    internal static class ParenthesesValidator
    {
        class OpenClose
        {
            public OpenClose(char Opening, char Closing)
            {
                this.Opening = Opening;
                this.Closing = Closing;
            }

            public char Opening
            {
                get;
                private set;
            }

            public char Closing
            {
                get;
                private set;

            }
        }

        private static readonly IEnumerable<OpenClose> Parentheses = new List<OpenClose>() { new OpenClose('{', '}'), new OpenClose('(', ')'), new OpenClose('[', ']') };
        private static readonly IEnumerable<char> OpeningParentheses = new HashSet<char>(Parentheses.Select(p => p.Opening));
        private static readonly IEnumerable<char> ClosingParentheses = new HashSet<char>(Parentheses.Select(p => p.Closing));
        private static readonly IEnumerable<char> AllParentheses = new HashSet<char>(OpeningParentheses.Concat(ClosingParentheses));
        private static readonly IDictionary<char, char> ParenthesesMap = Parentheses.ToDictionary(p => p.Opening, p => p.Closing);

        public static bool IsParenthesesBalanced(string text)
        {
            var stack = new Stack<char>();

            foreach (char letter in text.Where(IsParentheses))
            {
                if (OpeningParentheses.Contains(letter))
                {
                    stack.Push(letter);
                }
                else if (stack.Count > 0)
                {
                    // Stack contains opening parentheses so {,(,[
                    // We pop elements when we find closing parenthese.
                    var top = stack.Peek();

                    if (!IsCorrectClosingParenthese(top, letter))
                    {
                        return false;
                    }

                    stack.Pop();
                }
                else
                {
                    // Stack should we should a opening parenthese otherwise if we
                    // Pop when stack is empty it will throw an error.

                    // This handles when user provide first letter as a closing parenthese 
                    // rather then opening so ]()[
                    return false;
                }
            }

            return stack.Count == 0;
        }

        private static bool IsParentheses(char letter)
        {
            return AllParentheses.Contains(letter);
        }

        private static bool IsCorrectClosingParenthese(char openingParenthese, char closingParenthese)
        {
            return ParenthesesMap[openingParenthese] == closingParenthese;
        }

        public static int FindClosedParenthese(string Input, int IndexIzq)
        {
            var oResult = 0;

            if (Input.Length < IndexIzq || Input[IndexIzq] != '(')
            {
                oResult = -1;
            }
            else
            {
                oResult = Input.LastIndexOf(')');
                var aux = IndexIzq + 1;

                while (aux > IndexIzq && !ParenthesesValidator.IsParenthesesBalanced(Input.Substring(IndexIzq + 1, oResult - IndexIzq - 1)))
                {
                    aux = Input.Substring(IndexIzq + 1, oResult - IndexIzq - 1).LastIndexOf(')') + IndexIzq + 1;

                    if (aux > 0)
                        oResult = aux;
                    else
                        oResult = -1;
                }
            }

            return oResult;
        }
    }
}
