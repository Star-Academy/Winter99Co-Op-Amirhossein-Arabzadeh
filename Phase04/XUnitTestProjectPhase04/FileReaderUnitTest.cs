using Phase04;
using Xunit;

namespace XUnitTestProjectPhase04
{
    public class FileReaderUnitTest
    {
        [Fact]
        public void GetTextOfFile()
        {
            IFileReader fileReader = new FileReader();
            Assert.Equal("[{Ali\r\nReza, Javad,8531646@#}]", fileReader.GetTextOfFile("../../../../Resources/ali.txt"));
        }
    }
}