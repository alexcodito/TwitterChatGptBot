using FluentAssertions;
using Moq;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Spectre.Console.Menu.UnitTest;

public class MenuOptionTests
{
    private Menu _menu;
    private Menu _subMenu;
    private MenuHandler _sut;
    private Mock<IConsole> _consoleMock;
    private bool _callbackExecuted;

    [SetUp]
    public void SetUp()
    {
        _consoleMock = new Mock<IConsole>();
        _sut = new MenuHandler(_consoleMock.Object);
    }

    [Test]
    public void MenuOptionPresentsSubMenu()
    {
        this.Given(_ => AMenuOptionWithASubMenu())
            .When(_ => TheOptionIsSelected())
            .Then(_ => TheSubMenuIsPresented())
            .BDDfy();
    }

    [Test]
    public void BackMenuOptionCanReturnToParentMenu()
    {
        this.Given(_ => AMenuOptionWithAParentMenu())
            .When(_ => TheBackOptionIsSelected())
            .Then(_ => TheParentMenuIsPresented())
            .BDDfy();
    }

    [Test]
    public void MenuOptionTriggersAction()
    {
        this.Given(_ => AMenuOptionWithATriggerAction())
            .When(_ => TheOptionIsSelected())
            .Then(_ => TheActionIsTriggered())
            .BDDfy();
    }

    private void TheParentMenuIsPresented()
    {
        _sut.SelectedOption.Should().Be("Menu Option 1");
    }

    private void TheBackOptionIsSelected()
    {
        _sut.ProcessMenuSelection(_subMenu);
    }

    private void AMenuOptionWithAParentMenu()
    {
        _subMenu = new Menu()
           .AddMenuOption(new MenuOption("SubMenu Option 1", () => {}))
           .AddMenuOption(new MenuOption("SubMenu Option 2", () => {}))
           .AddMenuOption(new MenuOption("Back"));

        _menu = new Menu()
            .AddMenuOption(new MenuOption("Menu Option 1", () => {}))
            .AddMenuOption(new MenuOption("Menu Option 2", _subMenu))
            .AddMenuOption(new MenuOption("Exit", () => {}));

        _consoleMock
            .SetupSequence(x => x.Prompt(It.IsAny<SelectionPrompt<string>>()))
            .Returns("Back")
            .Returns("Menu Option 1");
    }

    private void TheSubMenuIsPresented()
    {
        _sut.SelectedOption.Should().Be("SubMenu Option 2");
    }

    private void TheOptionIsSelected()
    {
        _sut.ProcessMenuSelection(_menu);
    }

    private void AMenuOptionWithASubMenu()
    {
        _subMenu = new Menu()
            .AddMenuOption(new MenuOption("SubMenu Option 1", () => {}))
            .AddMenuOption(new MenuOption("SubMenu Option 2", () => {}))
            .AddMenuOption(new MenuOption("Back"));

        _menu = new Menu()
            .AddMenuOption(new MenuOption("Menu Option 1", () => {}))
            .AddMenuOption(new MenuOption("Menu Option 2", _subMenu))
            .AddMenuOption(new MenuOption("Exit"));

        _consoleMock
            .SetupSequence(x => x.Prompt(It.IsAny<SelectionPrompt<string>>()))
            .Returns("Menu Option 2")
            .Returns("SubMenu Option 2");
    }
    
    private void TheActionIsTriggered()
    {
        _callbackExecuted.Should().Be(true);
    }

    private void AMenuOptionWithATriggerAction()
    {
        _menu = new Menu()
            .AddMenuOption(new MenuOption("Menu Option 1", () => { _callbackExecuted = true; }))
            .AddMenuOption(new MenuOption("Exit"));

        _consoleMock
            .SetupSequence(x => x.Prompt(It.IsAny<SelectionPrompt<string>>()))
            .Returns("Menu Option 1");
    }

}