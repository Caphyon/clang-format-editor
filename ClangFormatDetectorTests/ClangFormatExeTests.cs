using ClangFormatDetector;
using System;
using System.IO;
using Xunit;

namespace ClangFormatDetectorTests
{
  public class ClangFormatExeTests
  {
    [Fact]
    public void Check_ClangFormatExe_Exists()
    {
      //Arrange
      string executableName = FormatConstants.ClangFormatExe;
      var projectPath = Environment.CurrentDirectory.Replace("ClangFormatDetectorTests", "ClangFormatDetector");

      //Act
      string path = Path.Combine(projectPath, executableName);

      //Assert
      Assert.True(File.Exists(path));
    }
  }
}
