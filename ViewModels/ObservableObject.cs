using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace DiveHubMauiApp.ViewModels;
public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value; Notify(name); return true;
    }
    protected void Notify([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
public sealed class AsyncCommand(Func<object?, Task> execute, Action<Exception> onError) : ICommand
{
    private bool running;
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? parameter) => !running;
    public async void Execute(object? parameter)
    {
        if (running) return;
        running = true; CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        try { await execute(parameter); }
        catch (Exception ex) { onError(ex); }
        finally { running = false; CanExecuteChanged?.Invoke(this, EventArgs.Empty); }
    }
}
public abstract class ViewModelBase : ObservableObject
{
    private string error = "";
    private bool busy;
    public string Error { get => error; set { if (Set(ref error, value)) Notify(nameof(HasError)); } }
    public bool HasError => !string.IsNullOrEmpty(Error);
    public bool IsBusy { get => busy; set => Set(ref busy, value); }
    protected AsyncCommand Command(Func<object?, Task> action) => new(action, ex => Error = ex is ArgumentException or InvalidOperationException ? ex.Message : "Something went wrong. Please try again.");
    protected async Task RunAsync(Func<Task> action)
    {
        IsBusy = true; Error = "";
        try { await action(); }
        catch (Exception ex) { Error = ex is ArgumentException or InvalidOperationException ? ex.Message : "Unable to load your local data. Please try again."; }
        finally { IsBusy = false; }
    }
}
