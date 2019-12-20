using mxcd.util.expresion.text.ParentesisValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace mxcd.util.expression.text
{
    internal class StringExpression : IStringExpression
    {
        string Text;

        public StringExpression(string text)
        {
            Text = text;
        }

        public int FindClosedParenthese(int indexIzq)
        {
            return ParenthesesValidator.FindClosedParenthese(this.Text, indexIzq);
        }

        public bool IsParenthesesBalanced()
        {
            return ParenthesesValidator.IsParenthesesBalanced(this.Text);
        }
    }
    public static class UtilExpressionText
    {
        public static IStringExpression Expression(this string text)
        {
            return new StringExpression(text);
        }
    }
}
