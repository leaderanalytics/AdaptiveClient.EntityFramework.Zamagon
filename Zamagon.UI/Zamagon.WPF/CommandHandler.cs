namespace Zamagon.WPF;

public class CommandHandler : ICommand
{
    public event EventHandler CanExecuteChanged;
    private Func<object, Task> ExecuteAction;
    private Func<object, bool> CanExecuteAction;

    public CommandHandler(Func<object, Task> execute, Func<object, bool> canExecute)
    {
        ExecuteAction = execute;
        CanExecuteAction = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return CanExecuteAction(parameter);
    }

    public void Execute(object parameter)
    {
        App.Current.Dispatcher.InvokeAsync(async () => await ExecuteAction(parameter)).Wait();
    }
}
