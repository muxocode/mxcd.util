using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using mxcd.util.statistics;

namespace mxcd.util.test
{
    public class MathTest
    {
        public List<decimal> aLista = new List<decimal>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        [Fact]
        public void DesviacionTipica()
        {
            var oResult = aLista.TypicalDeviation();
            Assert.True(oResult > 2.58m && oResult < 2.59m);

            var oResultDouble = aLista.Select(x => Convert.ToDouble(x)).TypicalDeviation();
            Assert.True(oResultDouble > 2.58 && oResultDouble < 2.59);

            var oResultInt = aLista.Select(x => Convert.ToInt32(x)).TypicalDeviation();
            Assert.True(oResultInt > 2.58m && oResultInt < 2.59m);
        }


        [Fact]
        public void Mediana()
        {
            var oResult = aLista.Median();
            Assert.True(oResult == 5);

            Assert.True(aLista.Select(x => Convert.ToDouble(x)).Median() == 5.0);
            Assert.True(aLista.Select(x => Convert.ToInt32(x)).Median() == 5);

        }
    }
}
