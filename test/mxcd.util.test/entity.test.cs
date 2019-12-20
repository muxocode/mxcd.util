using mxcd.util.test.classes;
using System;
using Xunit;
using mxcd.util.entity;
using System.Linq;
using System.Collections.Generic;
using Moq;
using mxcd.util.exception;

namespace mxcd.util.test
{
    public class EntityTest
    {
        [Fact]
        public void Entity()
        {
            var user = new User
            {
                Id = 5,
                nombre = "Miguel Angel",
                apelido = "del Campo",
                birth = new DateTime(1982, 3, 26),
            };

            Assert.True(user.GetKeysValues().Count() == 3);
            Assert.True(user.GetKeysValues(includeFields: true).Count() == 6);
            Assert.True(!user.GetKeysValues(false).Any());
            Assert.True(user.GetKeysValues(false, true).Count() == 3);
            Assert.True(user.GetKeysValues(includeFields: true, excludedNames: new List<string>() { "nombre" }).Count() == 5);
            Assert.True(user.GetKeysValues(includeFields: true, excludedNames: new List<string>() { "name" }).Count() == 6);
            Assert.True(user.GetKeysValues(includeFields: true, excludedNames: new List<string>() { "NombreCompleto" }).Count() == 5);
            Assert.True(user.GetKeysValues(includeFields: true, excludedNames: new List<string>() { "nombre", "NombreCompleto" }).Count() == 4);
        }

        [Fact]
        public void Interface()
        {
            var user = new User
            {
                Id = 5,
                nombre = "Miguel Angel",
                apelido = "del Campo",
                birth = new DateTime(1982, 3, 26),
            };

            Assert.True(user.GetKeysValues<IUser>().Count() == 3);
            Assert.True(user.GetKeysValues<IUser>(includeFields: true).Count() == 3);
            Assert.True(!user.GetKeysValues<IUser>(false).Any());
            Assert.True(!user.GetKeysValues<IUser>(false, true).Any());
            Assert.True(user.GetKeysValues<IUser>(includeFields: true, excludedNames: new List<string>() { "nombre" }).Count() == 3);
            Assert.True(user.GetKeysValues<IUser>(includeFields: true, excludedNames: new List<string>() { "name" }).Count() == 3);
            Assert.True(user.GetKeysValues<IUser>(includeFields: true, excludedNames: new List<string>() { "NombreCompleto" }).Count() == 2);
            Assert.True(user.GetKeysValues<IUser>(includeFields: true, excludedNames: new List<string>() { "nombre", "NombreCompleto" }).Count() == 2);
        }

        [Fact]
        public void Exception()
        {
            var mock = new Mock<IUser>();
            mock.SetupGet(x => x.NombreCompleto).Throws<ArgumentNullException>();

            var user = mock.Object;

            Assert.Throws<UtilException>(() => { user.GetKeysValues<IUser>(); });
        }
    }
}
