namespace mxcd.util.expression.text
{
    public interface IStringExpression
    {
        bool IsParenthesesBalanced();
        int FindClosedParenthese(int indexIzq);
    }
}
