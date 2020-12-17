using System.IO;
using System.Reflection;
using Xunit;

namespace ClangFormatDetectorTests
{
  public class ClangFormatExeTests
  {
    [Fact]
    public void Check_ClangFormatExe_Exists()
    {
      //Arrange
      string executableName = "clang-format.exe";
      string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      //Act
      string path = Path.Combine(directory, executableName);

      //Assert
      Assert.True(File.Exists(path));
    }
  }
}
