using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace mxcd.util.sql
{
    public static class SqlUtil
    {
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <typeparam name="T">Types</typeparam>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql<T>(this T val) where T : IConvertible
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this string val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this int? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this short? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this long? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this decimal? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this double? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static string ToSql(this float? val)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <param name="type">Tipo</param>
        /// <returns></returns>
        public static string ToSql(this object val, Type type)
        {
            return SqlValue.GetValue(val);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static String ToSql<T, TResult>(this Expression<Func<T, TResult>> expression) where T:class
        {
            return SqlExpressionUtil.ToSql(expression);
        }
        /// <summary>
        /// Converts the current value into a sql value
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns></returns>
        public static String ToSql<T>(this Expression<Func<T, bool>> expression) where T : class
        {
            return SqlExpressionUtil.ToSql(expression);
        }
    }
}
