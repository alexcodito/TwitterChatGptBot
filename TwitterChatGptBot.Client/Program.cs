using Config.Net;
using Spectre.Console;
using Spectre.Console.Menu;
using TwitterChatGptBot.Client.Configuration;

IBotConfiguration configuration;

var ansiConsole = AnsiConsole.Create(new AnsiConsoleSettings());
var console = new AnsiConsoleAdapter(ansiConsole);
var menuHandler = new MenuHandler(console);

void InitializeConsole()
{
    ansiConsole.Clear(false);
    ansiConsole.MarkupLine($"[bold blue]Twitter ChatGPT Bot client version {AppInfo.Version}[/]");
    ansiConsole.WriteLine();
}
void InitializeConfiguration()
{
    configuration = new ConfigurationBuilder<IBotConfiguration>()
        .UseJsonFile(AppInfo.ConfigurationPath)
        .Build();
}

void PromptSetApiKey()
{
    configuration.ChatGptConfiguration.ApiKey =
        ansiConsole.Prompt(
            new TextPrompt<string>("Please provide your [blue]ChatGPT API[/] key?")
                .Secret());
    InitializeConsole(); // TODO: Find a way to not have to use this
}

async Task InitializeMenu()
{
    var chatGptMenuOptions = new Menu()
        .AddMenuOption(new MenuOption("Set ChatGPT API Key", PromptSetApiKey))
        .AddMenuOption(new MenuOption("Back", ActionType.LoadParentMenu));

    var twitterMenuOptions = new Menu()
        .AddMenuOption(new MenuOption("Back", ActionType.LoadParentMenu));

    var configurationMenuOptions = new Menu()
        .AddMenuOption(new MenuOption("ChatGPT", chatGptMenuOptions))
        .AddMenuOption(new MenuOption("Twitter", twitterMenuOptions))
        .AddMenuOption(new MenuOption("Back", ActionType.LoadParentMenu));

    var mainMenuOptions = new Menu()
        .AddMenuOption(new MenuOption("Post a tweet from a topic", () => { }))
        .AddMenuOption(new MenuOption("Configuration", configurationMenuOptions))
        .AddMenuOption(new MenuOption("Exit"));

    await menuHandler.ProcessMenuSelection(mainMenuOptions);
}

InitializeConsole();
InitializeConfiguration();
await InitializeMenu();