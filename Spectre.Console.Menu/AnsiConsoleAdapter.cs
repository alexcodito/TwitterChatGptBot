namespace Spectre.Console.Menu;

public class AnsiConsoleAdapter : IConsole
{
    private IAnsiConsole _ansiConsole;

    public AnsiConsoleAdapter(IAnsiConsole ansiConsole)
    {
        _ansiConsole = ansiConsole;
    }

    public T Prompt<T>(IPrompt<T> prompt)
    {
        return _ansiConsole.Prompt(prompt);
    }
}

public interface IConsole
{
    public T Prompt<T>(IPrompt<T> prompt);
}