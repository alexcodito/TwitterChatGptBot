namespace Spectre.Console.Menu;

public sealed class MenuOption
{
    public string Title { get; }

    public Menu? SubMenu { get; }

    public ActionType? ActionType { get; }

    public Func<Task>? Callback { get; }

    public MenuOption(string title, Menu? subMenu)
    {
        Title = title;
        SubMenu = subMenu;
        ActionType = Console.Menu.ActionType.LoadSubMenu;
    }

    public MenuOption(string title, Func<Task> callback)
    {
        Title = title;
        Callback = callback;
        ActionType = Console.Menu.ActionType.ExecuteCallback;
    }
    
    public MenuOption(string title, Action callback)
    {
        Task FuncCallback() => Task.Run(callback);

        Title = title;
        Callback = FuncCallback;
        ActionType = Console.Menu.ActionType.ExecuteCallback;
    }

    public MenuOption(string title)
    {
        Title = title;
    }

    public MenuOption(string title, ActionType actionType)
    {
        Title = title;
        ActionType = actionType;
    }
}
