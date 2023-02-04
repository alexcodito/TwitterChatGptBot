namespace Spectre.Console.Menu;

public sealed class MenuOption
{
    public string Title { get; set; }

    public Menu? SubMenu { get; set; }

    public ActionType? ActionType { get; set; }

    public MenuOption(string title, Menu? subMenu = null, ActionType? actionType = null)
    {
        Title = title;
        SubMenu = subMenu;
        ActionType = actionType;
    }
}
