using System;
using System.Collections.Generic;
using System.Linq;

namespace mxcd.util.statistics
{
    /// <summary>
    /// Funciones matemáticas
    /// </summary>
    public static class StatisticsFunctions
    {
        private static bool BasicComprobation<T>(IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }
        /// <summary>
        /// Typical deviation
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static decimal TypicalDeviation(this IEnumerable<decimal> list)
        {
            if (BasicComprobation(list))
                return 0;

            decimal average = list.Average();
            decimal sumOfSquaresOfDifferences = list.Select(val => (val - average) * (val - average)).Sum();
            decimal sd = Convert.ToDecimal(System.Math.Sqrt(Convert.ToDouble(sumOfSquaresOfDifferences / list.Count())));

            return sd;
        }
        /// <summary>
        /// Typical deviation
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static decimal TypicalDeviation(this IEnumerable<int> list)
        {
            if (BasicComprobation(list))
                return 0;

            return list.Select(x => Convert.ToDecimal(x)).TypicalDeviation();
        }
        /// <summary>
        /// Typical deviation
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double TypicalDeviation(this IEnumerable<double> list)
        {
            if (BasicComprobation(list))
                return 0;

            return Decimal.ToDouble(list.Select(x => Convert.ToDecimal(x)).TypicalDeviation());
        }
        /// <summary>
        /// Typical deviation
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double TypicalDeviation(this IEnumerable<float> list)
        {
            if (BasicComprobation(list))
                return 0;

            return Decimal.ToDouble(list.Select(x => Convert.ToDecimal(x)).TypicalDeviation());
        }
        /// <summary>
        /// Median
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static decimal Median(this IEnumerable<decimal> list)
        {
            if (BasicComprobation(list))
                return 0;

            decimal dResult = 0;

            // Create a copy of the input, and sort the copy
            decimal[] temp = list.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count % 2 == 0)
            {
                // count is even, average two middle elements
                decimal a = temp[count / 2 - 1];
                decimal b = temp[count / 2];
                dResult = (a + b) / 2;
            }
            else
            {
                // count is odd, return the middle element
                dResult = temp[count / 2];
            }

            return dResult;
        }
        /// <summary>
        /// Median
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Median(this IEnumerable<int> list)
        {
            if (BasicComprobation(list))
                return 0;

            return Decimal.ToInt32(list.Select(x => Convert.ToDecimal(x)).Median());
        }
        /// <summary>
        /// Median
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double Median(this IEnumerable<double> list)
        {
            if (BasicComprobation(list))
                return 0;

            return Decimal.ToDouble(list.Select(x => Convert.ToDecimal(x)).Median());
        }
        /// <summary>
        /// Median
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double Median(this IEnumerable<float> list)
        {
            if (BasicComprobation(list))
                return 0;

            return Decimal.ToDouble(list.Select(x => Convert.ToDecimal(x)).Median());
        }
    }
}
