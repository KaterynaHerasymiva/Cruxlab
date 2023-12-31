﻿using Cruxlab.Services;
using Microsoft.Win32;

using MvvmLib.Commands;
using MvvmLib.Mvvm;

using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cruxlab;

// BindableBase typical implementation of INotifyPropertyChanged
public class MainWindowViewModel : BindableBase
{
    private readonly IPasswordsValidator _passwordsValidator;
    private bool? _isNotBusy;
    private ICommand? _selectFileCommand;
    private string _selectedFileName;
    private string _selectedFileFullPath;
    private AsyncCommand? _submitCommand;
    private int _validPasswords;
    private bool _canSubmit;

    public MainWindowViewModel(IPasswordsValidator passwordsValidator)
    {
        _passwordsValidator = passwordsValidator;
    }

    public bool? IsNotBusy
    {
        get => _isNotBusy;
        set
        {
            if (SetProperty(ref _isNotBusy, value))
            {
                OnPropertyChanged(nameof(IsSpinnerVisible));
                OnPropertyChanged(nameof(IsCalculationFinished));
            }
        }
    }

    public bool IsSpinnerVisible => IsNotBusy == false;

    public bool IsCalculationFinished => !IsSpinnerVisible;

    public string SelectedFileName
    {
        get => _selectedFileName;
        set
        {
            _selectedFileName = value;
            OnPropertyChanged(nameof(SelectedFileName));
        }
    }

    public int ValidPasswords
    {
        get => _validPasswords;
        set => SetProperty(ref _validPasswords, value);
    }

    public ICommand SelectFileCommand
    {
        get { return _selectFileCommand ??= new DelegateCommand(SelectFile); }
    }

    private void SelectFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Text|*.txt"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            _selectedFileFullPath = openFileDialog.FileName;
            SelectedFileName = Path.GetFileName(_selectedFileFullPath);
            CanSubmit = true;
        }
    }

    public bool CanSubmit
    {
        get => _canSubmit;
        set
        {
            if (SetProperty(ref _canSubmit, value))
            {
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public AsyncCommand SubmitCommand => _submitCommand ??= new AsyncCommand(ExecuteSubmitCommand, () => CanSubmit);

    private async Task ExecuteSubmitCommand()
    {
        IsNotBusy = false;

        try
        {
            ValidPasswords = await _passwordsValidator.ValidatePasswordAsync(_selectedFileFullPath);
        }
        catch (System.Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally
        {
            IsNotBusy = true;
        }
    }
}