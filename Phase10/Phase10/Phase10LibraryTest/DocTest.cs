using System;
using System.Collections.Generic;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class DocTest
    {
        public static IEnumerable<object[]> GetDocArguments = new List<object[]>
        {
            new object[] {"ProperName","    "},
            new object[] {"ProperName",null},
            new object[] {"   ","ProperContent"},
            new object[] {null,"ProperContent"},
        };
        [Theory]
        [MemberData(nameof(GetDocArguments))]
        public void Doc_ShouldThrowArgumentException_WhenParametersAreInvalid(string id, string content)
        {
            void Action() => new Doc(id, content);
            Assert.Throws<ArgumentException>(Action);
        }

        [Fact]
        public void Doc_ShouldCreateDocWithoutException_WhenParametersAreValid()
        {
            var doc = new Doc("152", "sdjfnkjdf{POPDJODJ^$R^&^(**865365756IUHGGVHBIJ\n\r\"dfgsfdgdfgfds");
            Assert.Equal("152", doc.Name);
            Assert.Equal("sdjfnkjdf{POPDJODJ^$R^&^(**865365756IUHGGVHBIJ\n\r\"dfgsfdgdfgfds", doc.Content);
        }
    }
}