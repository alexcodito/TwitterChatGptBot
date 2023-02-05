namespace Spectre.Console.Menu;

public class MenuHandler
{
    private readonly IConsole _console;
    public string SelectedOption;

    public MenuHandler(IConsole console)
    {
        _console = console;
    }

    public async Task ProcessMenuSelection(Menu? menuOptions)
    {
        if (menuOptions == null)
            throw new ArgumentNullException(nameof(menuOptions));

        var optionTitles = menuOptions.Options.Select(x => x.Title);

        SelectedOption = _console.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option")
                .PageSize(10)
                .AddChoices(optionTitles));

        var selectedMenuOption = menuOptions.Options.First(x => x.Title == SelectedOption);

        if (selectedMenuOption.ActionType == ActionType.LoadParentMenu)
        {
            await ProcessMenuSelection(menuOptions.Parent);
        }
        else if (selectedMenuOption.ActionType == ActionType.ExecuteCallback)
        {
            if (selectedMenuOption.Callback != null) 
                await Task.Run(selectedMenuOption.Callback);

            await ProcessMenuSelection(menuOptions);
        }
        else if(selectedMenuOption.SubMenu != null)
        {
            await ProcessMenuSelection(selectedMenuOption.SubMenu);
        }
    }
}