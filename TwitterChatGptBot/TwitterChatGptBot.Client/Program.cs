using Spectre.Console;
using Spectre.Console.Menu;
using TwitterChatGptBot.Client.Configuration;

var ansiConsole = AnsiConsole.Create(new AnsiConsoleSettings());
var console = new AnsiConsoleAdapter(ansiConsole);
var menuHandler = new MenuHandler(console);

AnsiConsole.Markup($"[bold blue]Twitter ChatGPT Bot client version {AppInfo.Version}[/]");
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();

var configurationMenuOptions = new Menu()
    .AddMenuOption(new MenuOption("ChatGPT"))
    .AddMenuOption(new MenuOption("Twitter"))
    .AddMenuOption(new MenuOption("Back", actionType: ActionType.Back));

var mainMenuOptions = new Menu()
    .AddMenuOption(new MenuOption("Post a tweet from a topic"))
    .AddMenuOption(new MenuOption("Configuration", configurationMenuOptions))
    .AddMenuOption(new MenuOption("Exit"));

menuHandler.ProcessMenuSelection(mainMenuOptions);

