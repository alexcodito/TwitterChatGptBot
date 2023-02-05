namespace Spectre.Console.Menu;

public sealed class Menu
{
    public IList<MenuOption> Options { get; set; }
    public Menu? Parent { get; set; }

    public Menu()
    {
        Options = new List<MenuOption>();
    }

    public Menu AddMenuOption(MenuOption menuOption)
    {
        if (menuOption == null)
            throw new ArgumentNullException(nameof(menuOption));
        
        if (menuOption.SubMenu != null)
            menuOption.SubMenu.Parent = this;

        Options.Add(menuOption);
        return this;
    }
}