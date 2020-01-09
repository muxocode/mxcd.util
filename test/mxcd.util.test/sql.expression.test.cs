using Moq;
using mxcd.util.test.classes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;
using mxcd.util.sql;
using System.Linq;

namespace mxcd.util.test
{
    public class ExpressionUtilTests
    {
 
        [Fact]
        public void Igual()
        {

            Expression<Func<Paciente, bool>> sResult = x => x.Id == 3;

            Assert.True(sResult.ToSql() == "[Id] = 3");

            Expression<Func<Paciente, bool>> sResult2 = x => x.Id.Equals(3);

            Assert.True(sResult2.ToSql() == "[Id] = 3");
        }

        [Fact]
        public void EqualsTo()
        {

            Expression<Func<Paciente, bool>> sResult = x => x.Id.Equals(3);

            Assert.True(sResult.ToSql()== "[Id] = 3");
        }

        [Fact]
        public void Mayor()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id > 3;

            Assert.True(sResult.ToSql()== "[Id] > 3");
        }

        [Fact]
        public void Menor()
        {

            Expression<Func<Paciente, bool>> sResult = x => x.Id < 3;

            Assert.True(sResult.ToSql()== "[Id] < 3");
        }

        [Fact]
        public void MenorIgual()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id <= 3;

            Assert.True(sResult.ToSql()== "[Id] <= 3");
        }

        [Fact]
        public void MayorIgual()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id >= 3;

            Assert.True(sResult.ToSql()== "[Id] >= 3");
        }

        [Fact]
        public void Distinto()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id != 3;

            Assert.True(sResult.ToSql()== "[Id] <> 3");
        }

        [Fact]
        public void And()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id != 3 && x.Id == 3;

            Assert.True(sResult.ToSql()== "([Id] <> 3) AND ([Id] = 3)");
        }

        [Fact]
        public void OR()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id != 3 || x.Id == 3;

            Assert.True(sResult.ToSql()== "([Id] <> 3) OR ([Id] = 3)");
        }

        [Fact]
        public void OR_AND()
        {
            Expression<Func<Paciente, bool>> sResult = x => (x.Id != 3) || (x.Id == 3 && x.Id < 4);

            Assert.True(sResult.ToSql()== "([Id] <> 3) OR (([Id] = 3) AND ([Id] < 4))");
        }

        [Fact]
        public void ANDOR()
        {
            Expression<Func<Paciente, bool>> sResult = x => (x.Id != 3) || (x.Id == 3 && x.Id < 4);

            Assert.True(sResult.ToSql()== "([Id] <> 3) OR (([Id] = 3) AND ([Id] < 4))");
        }

        [Fact]
        public void String()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre == "Ejemplo";

            Assert.True(sResult.ToSql()== "[Nombre] = N'Ejemplo'");

            Expression<Func<Paciente, bool>> sResult2 = x => x.Nombre.Equals("Ejemplo");

            Assert.True(sResult2.ToSql() == "[Nombre] = N'Ejemplo'");
        }

        [Fact]
        public void StarsWith()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "[Nombre] LIKE N'e%'");
        }


        [Fact]
        public void NotEndsWith()
        {
            Expression<Func<Paciente, bool>> sResult = x => !x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "[Nombre] NOT LIKE N'e%'");
        }

        [Fact]
        public void NotStarsWith()
        {
            Expression<Func<Paciente, bool>> sResult = x => !x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "[Nombre] NOT LIKE N'e%'");
        }


        [Fact]
        public void EndsWith()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "[Nombre] LIKE N'e%'");
        }

        [Fact]
        public void StarsWith_AND()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Id == 3 && x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "([Id] = 3) AND ([Nombre] LIKE N'e%')");
        }


        [Fact]
        public void EndsWith_OR()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre.StartsWith("e") || x.Id == 3;

            Assert.True(sResult.ToSql()== "([Nombre] LIKE N'e%') OR ([Id] = 3)");
        }

        [Fact]
        public void Bool()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.MayorEdad;

            Assert.True(sResult.ToSql()== "[MayorEdad] = 1");

            sResult = x => x.MayorEdad == true;

            Assert.True(sResult.ToSql()== "[MayorEdad] = 1");

            sResult = x => x.MayorEdad != true;

            Assert.True(sResult.ToSql()== "[MayorEdad] <> 1");

            sResult = x => x.MayorEdad.Equals(true);

            Assert.True(sResult.ToSql()== "[MayorEdad] = 1");

            sResult = x => x.MayorEdad.Equals(false);

            Assert.True(sResult.ToSql()== "[MayorEdad] = 0");
        }

        [Fact]
        public void BoolNegado()
        {
            Expression<Func<Paciente, bool>> sResult = x => !x.MayorEdad;

            Assert.True(sResult.ToSql()== "NOT ([MayorEdad] = 1)");

            sResult = x => x.MayorEdad == false;

            Assert.True(sResult.ToSql()== "[MayorEdad] = 0");

            sResult = x => x.MayorEdad != false;

            Assert.True(sResult.ToSql()== "[MayorEdad] <> 0");
        }

        [Fact]
        public void Compuesto()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.MayorEdad && x.Id == 3 || x.Nombre.EndsWith("a");

            Assert.True(sResult.ToSql()== "(([MayorEdad] = 1) AND ([Id] = 3)) OR ([Nombre] LIKE N'%a')");
        }

        [Fact]
        public void CompuestoNegado()
        {
            Expression<Func<Paciente, bool>> sResult = x => !(x.MayorEdad && x.Id == 3 || x.Nombre.EndsWith("a"));

            Assert.True(sResult.ToSql()== "NOT ((([MayorEdad] = 1) AND ([Id] = 3)) OR ([Nombre] LIKE N'%a'))");
        }

        [Fact]
        public void CompuestoNegadoParcial()
        {
            Expression<Func<Paciente, bool>> sResult = x => !(x.MayorEdad && x.Id == 3) || x.Nombre.EndsWith("a") && !(x.Id > 9);

            Assert.True(sResult.ToSql()== "(NOT (([MayorEdad] = 1) AND ([Id] = 3))) OR (([Nombre] LIKE N'%a') AND (NOT ([Id] > 9)))");
        }


        [Fact]
        public void ContainsString()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre.Contains("a");

            Assert.True(sResult.ToSql()== "[Nombre] LIKE N'%a%'");
        }

        [Fact]
        public void ContainsId()
        {
            var aIds = new List<long>() { 1, 2, 3 };

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id);
            var result = sResult.ToSql();
            Assert.True(result == "[Id] IN (1,2,3)");
        }

        [Fact]
        public void ContainsId_ENUMERABLE()
        {
            var _aIds = new List<long>() { 1, 2, 3 };

            var aIds = _aIds.Select(x => x);

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id);

            Assert.True(sResult.ToSql()== "[Id] IN (1,2,3)");
        }

        [Fact]
        public void ContainsId_ENUMERABLE_From_Entities()
        {
            var aUsuarios = new Paciente[3];

            aUsuarios[0] = new Paciente() { Id = 1 };
            aUsuarios[1] = new Paciente() { Id = 2 };
            aUsuarios[2] = new Paciente() { Id = 3 };


            var aIds = aUsuarios.Select(x => x.Id);

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id);

            Assert.True(sResult.ToSql()== "[Id] IN (1,2,3)");

            sResult = x => aUsuarios.Select(y => y.Id).Contains(x.Id);

            Assert.True(sResult.ToSql()== "[Id] IN (1,2,3)");
        }

        [Fact]
        public void ContainsId_ENUMERABLE_From_Empty()
        {
            var aUsuarios = new Paciente[0];

            var aIds = aUsuarios.Select(x => x.Id);

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id);
            var sRes = sResult.ToSql();
            Assert.True(sRes == "1 = 0");

        }

        [Fact]
        public void ContainsId_ARRAY()
        {
            var _aIds = new List<long>() { 1, 2, 3 };

            var aIds = _aIds.Select(x => x).ToArray();

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id);

            Assert.True(sResult.ToSql()== "[Id] IN (1,2,3)");
        }

        [Fact]
        public void NotContainsId()
        {
            var aIds = new List<long>() { 1, 2, 3 };

            Expression<Func<Paciente, bool>> sResult = x => !aIds.Contains(x.Id);

            Assert.True(sResult.ToSql()== "[Id] NOT IN (1,2,3)");
        }

        [Fact]
        public void ContainsListString()
        {
            var aIds = new List<string>() { "a", "b", "c", "d" };

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Nombre);

            Assert.True(sResult.ToSql()== "[Nombre] IN (N'a',N'b',N'c',N'd')");
        }

        [Fact]
        public void NotContainsListString()
        {
            var aIds = new List<string>() { "a", "b", "c", "d" };

            Expression<Func<Paciente, bool>> sResult = x => !aIds.Contains(x.Nombre);

            Assert.True(sResult.ToSql()== "[Nombre] NOT IN (N'a',N'b',N'c',N'd')");
        }

        [Fact]
        public void ContainsListString_OR()
        {
            var aIdStrings = new List<string>() { "a", "b", "c", "d" };
            var aIds = new List<long>() { 1, 2, 3 };

            Expression<Func<Paciente, bool>> sResult = x => aIds.Contains(x.Id) || aIdStrings.Contains(x.Nombre);


            Assert.True(sResult.ToSql()== "([Id] IN (1,2,3)) OR ([Nombre] IN (N'a',N'b',N'c',N'd'))");
        }

        [Fact]
        public void NULL()
        {
            Expression<Func<Paciente, bool>> sResult = null;

            Assert.True(sResult.ToSql()== null);
        }

        [Fact]
        public void ISNULL()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre == null;

            Assert.True(sResult.ToSql()== "[Nombre] IS null");
        }

        [Fact]
        public void ISNOTNULL()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Nombre != null;

            Assert.True(sResult.ToSql()== "[Nombre] IS NOT null");
        }

        [Fact]
        public void Nullable()
        {
            Expression<Func<Paciente, bool>> sResult = x => x.Fecha == DateTime.Now;

            Assert.True(sResult.ToSql()== $"[Fecha] = '{DateTime.Now.ToString("yyyyMMdd HH:mm")}'");

            sResult = x => x.Saldo == 30;

            Assert.True(sResult.ToSql()== "[Saldo] = 30");
        }

        Expression<Func<Paciente,T>> CreateSelect<T>(Expression<Func<Paciente, T>> expr)
        {
            return expr;
        }

        [Fact]
        public void ExpressionNames()
        {
            var sResult = CreateSelect(x => x.Id).ToSql();
            Assert.True(sResult== "[Id]");

            var sResult21 = CreateSelect<object>(x => x.MayorEdad).ToSql();
            Assert.True(sResult21 == "[MayorEdad]");

            var sResult2 = CreateSelect(x => new { x.Id, x.MayorEdad }).ToSql();
            Assert.True(sResult2== "[Id], [MayorEdad]");

            var sResult3 = CreateSelect(x => x.Nombre).ToSql();
            Assert.True(sResult3== "[Nombre]");

            var sResult7 = CreateSelect(x => x.Fecha).ToSql();
            Assert.True(sResult7== "[Fecha]");

            var sResult9 = CreateSelect(x=> x.Nombre).ToSql();
            Assert.True(sResult9== "[Nombre]");

            var sResult10 = CreateSelect(x => x.Saldo).ToSql();
            Assert.True(sResult10== "[Saldo]");
        }

        [Fact]
        public void BetweenProperties()
        {
            Expression<Func<Paciente, bool>> eResult = x => x.Id == x.Edad;
            var sResult = eResult.ToSql();

        }
    }
}
