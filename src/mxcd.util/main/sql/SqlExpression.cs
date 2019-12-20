using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace mxcd.util.sql
{
    internal static class SqlExpressionUtil
    {
        /// <summary>
        /// Obtiene el SQL string selection
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <typeparam name="TResult">Tipo de la selección</typeparam>
        /// <param name="expression">Expresión</param>
        /// <returns></returns>
        public static String ToSql<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            try
            {
                string result = null;

                if (expression.Body is MemberExpression member)
                {
                    result = member.Member.Name;
                }
                if (expression.Body is NewExpression newMember)
                {
                    result = String.Join(", ", newMember.Members.Select(x => x.Name));
                }
                if (expression.Body is UnaryExpression unary)
                {
                    result = unary.Operand.ToString().Split('.').Last();
                }

                return result ?? throw new InvalidOperationException("Expression not compatible");
            }
            catch(Exception oEx)
            {
                throw new exception.UtilException("Error on calculating SQL", oEx);
            }
        }
        /// <summary>
        /// Obtiene el SQL string expression
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad</typeparam>
        /// <param name="expression">Expresión</param>
        /// <returns></returns>
        public static String ToSql<T>(this Expression<Func<T, bool>> expression)
        {
            try
            {

                String sResult = null;

                if (expression != null)
                {

                    var body = expression.Body;

                    if (body is BinaryExpression)
                    {
                        sResult = GetExpresion<BinaryExpression>(body);
                    }
                    else if (body is MethodCallExpression)
                    {
                        sResult = GetExpresion<MethodCallExpression>(expression.Body);
                    }
                    else if (body is MemberExpression)
                    {
                        sResult = GetExpresion<MemberExpression>(expression.Body);
                    }
                    else if (body is UnaryExpression)
                    {
                        sResult = GetExpresion<UnaryExpression>(expression.Body);
                    }

                }

                return sResult;
            }
            catch (Exception oEx)
            {
                throw new exception.UtilException("Error at calculating SQL", oEx);
            }
        }

        private static string GetExpresion<T>(Expression Body) where T : Expression
        {
            var builder = String.Empty;

            switch (typeof(T).Name)
            {
                case "BinaryExpression":
                    builder = ToSql((BinaryExpression)Body);

                    break;
                case "MethodCallExpression":
                    builder = ToSql((MethodCallExpression)Body);

                    break;

                case "MemberExpression":
                    builder = ToSql((MemberExpression)Body);

                    break;

                case "UnaryExpression":
                    builder = ToSql((UnaryExpression)Body, ComparationType.Default);

                    break;
            }

            return builder;
        }

        private static string ToSql(UnaryExpression Body, ComparationType linkingType)
        {
            string sResult = null;
            if (Body.Operand is MethodCallExpression)
            {
                sResult = ToSql(Body.Operand as MethodCallExpression);
                sResult = sResult
                    .Replace(" IN (", " NOT IN (")
                    .Replace(" LIKE N", " NOT LIKE N");
            }
            else if (Body.Operand is BinaryExpression)
            {
                sResult = $"NOT ({ToSql(Body.Operand as BinaryExpression)})";
            }
            else if (Body.Operand is MemberExpression)
            {
                sResult = $"NOT ({ToSql(Body.Operand as MemberExpression)})";
            }
            else if (Body.Operand is UnaryExpression)
            {
                sResult = $"NOT ({ToSql(Body.Operand as UnaryExpression, linkingType)})";
            }
            else
            {
                throw new InvalidOperationException("No se admite el tipo de negación");
            }

            return sResult;
        }

        private static string ToSql(MemberExpression Body)
        {
            string propertyValueResult;
            string propertyName;

            propertyValueResult = true.ToSql();

            propertyName = Body.Member.Name;

            return string.Format("{0} {1} {2}", propertyName, "=", propertyValueResult);
        }

        private static string ToSql(MethodCallExpression MethodBody)
        {
            string propertyValueResult;
            string propertyName;
            string sResult = null;

            switch (MethodBody.Method.Name)
            {
                case "StartsWith":
                    propertyValueResult = MethodBody.Arguments.First().ToString();
                    propertyValueResult = propertyValueResult.Replace("\"", "");
                    propertyValueResult = $"{propertyValueResult}%";
                    propertyValueResult = propertyValueResult.ToSql();

                    propertyName = MethodBody.Object.ToString().Split('.').Last();

                    sResult = string.Format("{0} {1} {2}", propertyName, "LIKE", propertyValueResult);

                    break;
                case "EndsWith":
                    propertyValueResult = MethodBody.Arguments.First().ToString();
                    propertyValueResult = propertyValueResult.Replace("\"", "");
                    propertyValueResult = $"%{propertyValueResult}";
                    propertyValueResult = propertyValueResult.ToSql();

                    propertyName = MethodBody.Object.ToString().Split('.').Last();

                    sResult = string.Format("{0} {1} {2}", propertyName, "LIKE", propertyValueResult);

                    break;

                case "Contains":
                    var Nombres = new List<string>() { "List", "Enumerable", "Array" };

                    if (Nombres.Any(MethodBody.Method.DeclaringType.Name.StartsWith))
                    {
                        IEnumerable Lista = null;

                        if (MethodBody.Method.DeclaringType.Name.StartsWith("List", StringComparison.Ordinal))
                        {
                            Lista = GetValueExpression(MethodBody.Object) as IEnumerable;
                            propertyName = MethodBody.Arguments.First().ToString().Split('.').Last().Split(',').First();
                        }
                        else
                        {
                            Lista = GetValueExpression(MethodBody.Arguments.First()) as IEnumerable;
                            propertyName = MethodBody.Arguments.Last().ToString().Split('.').Last().Split(',').First();
                        }


                        string sValueList = string.Empty;
                        foreach (var oL in Lista)
                        {
                            string sComa = (sValueList == string.Empty) ? "" : ",";
                            sValueList += $"{sComa}{oL.ToSql()}";
                        }
                        if (sValueList == string.Empty)
                        {
                            return string.Format("{0} {1} {2}", "1", "=", "0");
                        }
                        else
                        {
                            sResult = string.Format("{0} {1} ({2})", propertyName, "IN", sValueList);

                        }

                    }
                    else if (MethodBody.Method.DeclaringType.Name == "String")
                    {
                        propertyValueResult = MethodBody.Arguments.First().ToString();
                        propertyValueResult = propertyValueResult.Replace("\"", "");
                        propertyValueResult = $"%{propertyValueResult}%";
                        propertyValueResult = propertyValueResult.ToSql();

                        propertyName = MethodBody.Object.ToString().Split('.').Last();

                        sResult = string.Format("{0} {1} {2}", propertyName, "LIKE", propertyValueResult);
                    }
                    else
                    {
                        throw new InvalidOperationException("No existe implemenetación para ese tipo de objeto");
                    }
                    break;
                case "Equals":
                    propertyValueResult = MethodBody.Arguments.First().ToString();
                    propertyValueResult = propertyValueResult.Replace("\"", "");
                    propertyValueResult = Convert.ChangeType(propertyValueResult, MethodBody.Method.DeclaringType).ToSql();

                    propertyName = MethodBody.Object.ToString().Split('.').Last();

                    sResult = string.Format("{0} {1} {2}", propertyName, "=", propertyValueResult);
                    break;
            }

            if (sResult == null)
            {
                throw new InvalidOperationException("No existe converisón SQL para ese método");
            }

            return sResult;
        }

        private static string ToSql(BinaryExpression body)
        {

            if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
            {

                string propertyName = GetPropertyName(body);
                Expression propertyValue = body.Right;
                string propertyValueResult = GetValueExpression(propertyValue).ToSql();
                string opr = GetOperator((ComparationType)body.NodeType, propertyValueResult);



                return string.Format("{0} {1} {2}", propertyName, opr, propertyValueResult);
            }
            else
            {
                string link = GetOperator((ComparationType)body.NodeType, null);

                var Left = body.Left;
                var Right = body.Right;

                return string.Format("({0}) {1} ({2})", AnalizePart(Left, body), link, AnalizePart(Right, body));
            }
        }

        private static string AnalizePart(Expression Expr, BinaryExpression body)
        {
            String sResult = null;

            if (Expr is BinaryExpression)
            {
                sResult = ToSql(Expr as BinaryExpression);
            }
            else if (Expr is MethodCallExpression)
            {
                var MethodBody = Expr as MethodCallExpression;
                sResult = ToSql(MethodBody);
            }
            else if (Expr is MemberExpression)
            {
                var MethodBody = Expr as MemberExpression;
                sResult = ToSql(MethodBody);
            }
            else if (Expr is UnaryExpression)
            {
                var MethodBody = Expr as UnaryExpression;
                sResult = ToSql(MethodBody, (ComparationType)body.NodeType);
            }

            return sResult;
        }

        private static object GetValueExpression(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }

        private static string GetPropertyName(BinaryExpression body)
        {
            string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }

        private static string GetOperator(ComparationType type, string value)
        {
            switch (type)
            {
                case ComparationType.Equal:
                    return value != "null" ? "=" : "IS";
                case ComparationType.NotEqual:
                    return value != "null" ? "<>" : "IS NOT";
                case ComparationType.LessThan:
                    return "<";
                case ComparationType.GreaterThan:
                    return ">";
                case ComparationType.LessThanOrEqual:
                    return "<=";
                case ComparationType.GreaterThanOrEqual:
                    return ">=";
                case ComparationType.AndAlso:
                case ComparationType.And:
                    return "AND";
                case ComparationType.Or:
                case ComparationType.OrElse:
                    return "OR";
                case ComparationType.Default:
                    return string.Empty;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
