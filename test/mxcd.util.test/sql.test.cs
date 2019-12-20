using mxcd.util.sql;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace mxcd.util.test
{
    public class sql
    {
        [Fact]
        public void GetValueSql()
        {
            Assert.True(1.ToSql() == "1");//int
            Assert.True(1L.ToSql() == "1");//long
            Assert.True(1.5F.ToSql() == "1.5");//float
            Assert.True(1.5m.ToSql() == "1.5");//decimal
            Assert.True(1.5.ToSql() == "1.5");//Double

            Assert.True(true.ToSql() == "1");
            Assert.True(false.ToSql() == "0");

            Assert.True("Texto".ToSql() == "N'Texto'");

            Assert.True(DateTime.Now.ToSql() == $"'{DateTime.Now.ToString(@"yyyyMMdd HH:mm")}'");

            string aux = null;
            Assert.True(aux.ToSql() == "null");

            int? num = null;
            Assert.True(num.ToSql() == "null");

        }
    }
}
