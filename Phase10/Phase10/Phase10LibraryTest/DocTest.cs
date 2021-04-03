using System.Collections.Generic;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class DocTest
    {
        private const string ProperContent = "ProperContent";
        private const string WhiteSpace = "    ";
        private const string ProperName = "ProperName";
        
        public static IEnumerable<object[]> GetDocArguments = new List<object[]>
        {
            new object[] {ProperName,WhiteSpace},
            new object[] {ProperName,null},
            new object[] {WhiteSpace,ProperContent},
            new object[] {null,ProperContent},
        };

        [Fact]
        public void Doc_ShouldCreateDocWithoutException_WhenParametersAreValid()
        {
            const string docContent = "sdjfnkjdf{POPDJODJ^$R^&^(**865365756IUHGGVHBIJ\n\r\"dfgsfdgdfgfds";
            const string docName = "152";
            var doc = new Doc(docName, docContent);
            Assert.Equal(docName, doc.Name);
            Assert.Equal(docContent, doc.Content);
        }
    }
}