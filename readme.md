# mxcd.util
Extension methods that makes your live easier and you work faster
## mxcd.util.sql
Convert object values and LINQ expressions to string SQL
### `.ToSql()`
#### Struct, String & Nullable\<T>
Convert a object value into a sql value
```csharp
using mxcd.util.sql;
...
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
...

```
#### Expression
Convert a boolean expression value into a sql expression
```csharp
using mxcd.util.sql;
...
            Expression<Func<Paciente, bool>> sResult = x => x.Id <= 3;

            Assert.True(sResult.ToSql()== "Id <= 3");
            ...

            Expression<Func<Paciente, bool>> sResult = x => x.Id != 3 && x.Id == 3;

            Assert.True(sResult.ToSql()== "(Id <> 3) AND (Id = 3)");
            ...

            Expression<Func<Paciente, bool>> sResult = x => !x.Nombre.StartsWith("e");

            Assert.True(sResult.ToSql()== "Nombre NOT LIKE N'e%'");

```
It works with string select (Expression<Func<T,object>>) as well
```csharp
using mxcd.util.sql;
...
            public class Paciente
            {
                public int Id { get; set; }
                public string Nombre { get; set; }
                public bool MayorEdad { get; set; }
                public DateTime Fecha { get; set; }
                public decimal Saldo { get; set; }
            }
            ...

            var sResult = CreateSelect(x => x.Id).ToSql();
            Assert.True(sResult== "Id");

            var sResult21 = CreateSelect<object>(x => x.MayorEdad).ToSql();
            Assert.True(sResult21 == "MayorEdad");

            var sResult2 = CreateSelect(x => new { x.Id, x.MayorEdad }).ToSql();
            Assert.True(sResult2== "Id, MayorEdad");

            var sResult3 = CreateSelect(x => x.Nombre).ToSql();
            Assert.True(sResult3== "Nombre");

            var sResult7 = CreateSelect(x => x.Fecha).ToSql();
            Assert.True(sResult7== "Fecha");

            var sResult9 = CreateSelect(x=> x.Nombre).ToSql();
            Assert.True(sResult9== "Nombre");

            var sResult10 = CreateSelect(x => x.Saldo).ToSql();
            Assert.True(sResult10== "Saldo");
```
## mxcd.util.entity
Entity extension to help object porpouses

```csharp
using mxcd.util.entity;
```

### `GetKeysValues([bool includeProps] = true, [bool includeFields] = false, [IEnumerable<string> excludedNames] = null)`
Get keys and values from an object
## mxcd.util.text
String functions
*See more en developerInfo.md*
## mxcd.util.expression.text
String expression extension methods
```csharp
using mxcd.util.expression.text;
```
#### `.Expression().IsPArenthesesBalanced()`
Cheks if parentheses al well opened and well closed
```csharp
            Assert.True("Texto".Expression().IsParenthesesBalanced());
            Assert.True("(Texto)".Expression().IsParenthesesBalanced());
            Assert.True("((T)ex(t)o)()".Expression().IsParenthesesBalanced());
            Assert.False("Texto)".Expression().IsParenthesesBalanced());
            Assert.False("(Texto".Expression().IsParenthesesBalanced());
            Assert.False("Te())xto".Expression().IsParenthesesBalanced());
```
#### `.Expression().FindClosingParentheses()`
Finds the closed parenthese from the openeds position given
```csharp
            var Texto = "((T)ex(t)o)()";

            Assert.True(Texto.Expression().FindClosedParenthese(0) == 10);
            Assert.True(Texto.Expression().FindClosedParenthese(1) == 3);
            Assert.True(Texto.Expression().FindClosedParenthese(6) == 8);
            Assert.True(Texto.Expression().FindClosedParenthese(11) == 12);

            Assert.True(Texto.Expression().FindClosedParenthese(2) == -1);
            Assert.True(Texto.Expression().FindClosedParenthese(100) == -1);

            Assert.True("(T()".Expression().FindClosedParenthese(0) == -1);
```
## mxcd.util.statistics
Statistics functions to IEnumerable numbers

<hr/>

Learn more in https://muxocode.com

<p align="center">
  <img src="https://muxocode.com/branding.png">
</p>