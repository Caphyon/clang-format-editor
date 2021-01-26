using ClangFormatEditor;
using System;
using System.IO;
using Xunit;

namespace ClangFormatEditorTests
{
  public class ClangFormatExeTests
  {
    [Fact]
    public void Check_ClangFormatExe_Exists()
    {
      //Arrange
      string executableName = AppConstants.ClangFormatExe;
      var projectPath = Environment.CurrentDirectory.Replace("ClangFormatEditorTests", "ClangFormatEditor");

      //Act
      string path = Path.Combine(projectPath, executableName);

      //Assert
      Assert.True(File.Exists(path));
    }
  }
}
