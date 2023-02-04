namespace Spectre.Console.Menu;

public interface IConsole
{
    public T Prompt<T>();
}