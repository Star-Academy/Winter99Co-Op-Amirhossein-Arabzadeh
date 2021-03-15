using System;
using System.Collections.Generic;
using Moq;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class DocTest
    {
        public static IEnumerable<object[]> GetDocArguments = new List<object[]>
        {
            new object[] {It.IsAny<string>(),"    "},
            new object[] {It.IsAny<string>(),null},
            new object[] {"   ",It.IsAny<string>()},
            new object[] {null,It.IsAny<string>()},
        };
        [Theory]
        [MemberData(nameof(GetDocArguments))]
        public void Doc_ShouldThrowArgumentException_WhenParametersAreInvalid(string id, string content)
        {
            void Action() => new Doc(id, content);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}