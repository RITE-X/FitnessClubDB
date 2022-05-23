using System;
using System.Windows.Input;
using FitnessClubDB.Commands.Base;

namespace FitnessClubDB.Commands;

public class RoutedCommand : Command
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool>? _canExecute;
    
    public RoutedCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }


    public override bool CanExecute(object parameter) => _canExecute is null || _canExecute(parameter);

    public override void Execute(object parameter) => _execute(parameter);
}