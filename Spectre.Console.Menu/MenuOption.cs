namespace Spectre.Console.Menu;

public sealed class MenuOption
{
    public string Title { get; }

    public Menu? SubMenu { get; }

    public ActionType? ActionType { get; }

    public Action? Callback { get; }

    public MenuOption(string title, Menu? subMenu)
    {
        Title = title;
        SubMenu = subMenu;
        ActionType = Console.Menu.ActionType.LoadSubMenu;
    }

    public MenuOption(string title, Action callback)
    {
        Title = title;
        Callback = callback;
        ActionType = Console.Menu.ActionType.ExecuteCallback;
    }

    public MenuOption(string title)
    {
        Title = title;
        ActionType = Console.Menu.ActionType.LoadParentMenu;
    }
}
