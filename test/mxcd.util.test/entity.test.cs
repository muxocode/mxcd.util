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

        [Fact]
        public void Assign()
        {
            var user = new User
            {
                Id = 5,
                nombre = "Miguel Angel",
                apelido = "del Campo",
                birth = new DateTime(1982, 3, 26),
            };

            var otherUser = new User();

            user.Assign(otherUser);

            Assert.True(otherUser.Id == 5);
            Assert.True(otherUser.nombre == null);
            Assert.True(otherUser.apelido == null);
            Assert.True(otherUser.birth.Date == new DateTime(1982, 3, 26));

            var otherUser1 = new User();
            (new { Id = 4, birth = new DateTime(1982, 3, 25) }).Assign(otherUser1);

            Assert.True(otherUser1.Id == 4);
            Assert.True(otherUser1.nombre == null);
            Assert.True(otherUser1.apelido == null);
            Assert.True(otherUser1.birth.Date == new DateTime());

            var otherUser2 = new User();
            user.Assign(otherUser2, false, true);

            Assert.True(otherUser2.Id == default(int));
            Assert.True(otherUser2.nombre == "Miguel Angel");
            Assert.True(otherUser2.apelido == "del Campo");
            Assert.True(otherUser2.birth.Date == new DateTime(1982, 3, 26));

            var otherUser3 = new User();
            user.Assign(otherUser3, false, true, new string[] { "nombre" });

            Assert.True(otherUser3.Id == default(int));
            Assert.True(otherUser3.nombre == null);
            Assert.True(otherUser3.apelido == "del Campo");
            Assert.True(otherUser3.birth.Date == new DateTime(1982, 3, 26));

        }
    }
}
