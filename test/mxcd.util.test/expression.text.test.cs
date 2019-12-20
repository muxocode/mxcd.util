using Xunit;
using mxcd.util.expression.text;

namespace mxcd.util.test
{
    public class expression
    {
        [Fact]
        public void IsPArenthesesBalanced()
        {
            Assert.True("Texto".Expression().IsParenthesesBalanced());
            Assert.True("(Texto)".Expression().IsParenthesesBalanced());
            Assert.True("((T)ex(t)o)()".Expression().IsParenthesesBalanced());
            Assert.False("Texto)".Expression().IsParenthesesBalanced());
            Assert.False("(Texto".Expression().IsParenthesesBalanced());
            Assert.False("Te())xto".Expression().IsParenthesesBalanced());
        }

        [Fact]
        public void FindClosingParentheses()
        {
            var Texto = "((T)ex(t)o)()";

            Assert.True(Texto.Expression().FindClosedParenthese(0) == 10);
            Assert.True(Texto.Expression().FindClosedParenthese(1) == 3);
            Assert.True(Texto.Expression().FindClosedParenthese(6) == 8);
            Assert.True(Texto.Expression().FindClosedParenthese(11) == 12);

            Assert.True(Texto.Expression().FindClosedParenthese(2) == -1);
            Assert.True(Texto.Expression().FindClosedParenthese(100) == -1);

            Assert.True("(T()".Expression().FindClosedParenthese(0) == -1);
        }
    }
}
