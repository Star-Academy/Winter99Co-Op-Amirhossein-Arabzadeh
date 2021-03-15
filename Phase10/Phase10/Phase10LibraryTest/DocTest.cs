using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Xunit;
using Moq;


namespace InvertedIndexTest
{
    public class DocTest
    {
        public static IEnumerable<object[]> GetDocArguments = new List<object[]>
        {
            new object[] {It.IsAny<int>(),"    "},
            new object[] {It.IsAny<int>(),null},
            new object[] {-1055,It.IsAny<string>()},
        };
        [Theory]
        [MemberData(nameof(GetDocArguments))]
        public void Doc_ShouldThrowArgumentException_WhenParametersAreInvalid(int id, string content)
        {
            void Action() => new Doc(id, content);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}