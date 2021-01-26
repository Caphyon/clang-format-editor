using ClangFormatEditor.Enums;
using ClangFormatEditor.Interfaces;
using ClangFormatEditor.MVVM.Models;
using ClangFormatEditor.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ClangFormatEditor
{
  public abstract class CommonFormatEditorFunctionality : CommonSettingsFunctionality
  {
    #region Members

    protected event EventHandler CloseMultipleInput;
    protected List<IFormatOption> formatStyleOptions;
    protected IFormatOption selectedOption;
    protected FormatStyle selectedStyle = FormatStyle.Custom;

    #endregion

    #region Protected Methods

    protected void OpenMultipleInput(IFormatOption selectedOption, Window owner)
    {
      if (selectedOption is FormatOptionMultipleInputModel multiInputModel)
      {
        OpenInputDataView(multiInputModel, owner);
      }
      else if (selectedOption is FormatOptionMultipleToggleModel multiToggelModel)
      {
        OpenToggleDataView(multiToggelModel, owner);
      }
    }

    #endregion

    #region Private Methods

    private void OpenInputDataView(FormatOptionMultipleInputModel multipleInputModel, Window owner)
    {
      var inputMultipleDataView = new InputMultipleDataView(multipleInputModel.MultipleInput, owner);
      inputMultipleDataView.Closed += CloseInputDataView;
      inputMultipleDataView.Show();
    }

    private void OpenToggleDataView(FormatOptionMultipleToggleModel multipleToggleModel, Window owner)
    {
      var toggleMultipleDataView = new ToggleMultipleDataView(multipleToggleModel.ToggleFlags, owner);
      toggleMultipleDataView.Closed += CloseToggleDataView;
      toggleMultipleDataView.Show();
    }

    private void CloseInputDataView(object sender, EventArgs e)
    {
      var multipleInputModel = (FormatOptionMultipleInputModel)selectedOption;
      var inputMultipleDataView = (InputMultipleDataView)sender;
      multipleInputModel.MultipleInput = ((InputMultipleDataViewModel)inputMultipleDataView.DataContext).Input;
      inputMultipleDataView.Closed -= CloseInputDataView;
      if (CloseMultipleInput != null)
      {
        CloseMultipleInput.Invoke(sender, e);
      }
    }

    private void CloseToggleDataView(object sender, EventArgs e)
    {
      var multipleToggleModel = (FormatOptionMultipleToggleModel)selectedOption;
      var toggleMultipleDataView = (ToggleMultipleDataView)sender;
      multipleToggleModel.ToggleFlags = ((ToggleMultipleDataViewModel)toggleMultipleDataView.DataContext).Input;
      toggleMultipleDataView.Closed -= CloseInputDataView;
      if (CloseMultipleInput != null)
      {
        CloseMultipleInput.Invoke(sender, e);
      }
    }

    #endregion
  }
}
