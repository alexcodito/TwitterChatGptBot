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
    private Mock<IPrompt<SelectionPrompt<string>>> _selectionPromptMock;

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
    [Ignore("Functionality not yet implemented")]
    public void MenuOptionTriggersAction()
    {
        this.Given(_ => AMenuOptionWithATriggerAction())
            .When(_ => TheOptionIsSelected())
            .Then(_ => TheActionIsTriggered())
            .BDDfy();
    }

    private void TheParentMenuIsPresented()
    {
        throw new NotImplementedException();
    }

    private void TheBackOptionIsSelected()
    {
        throw new NotImplementedException();
    }

    private void AMenuOptionWithAParentMenu()
    {
        throw new NotImplementedException();
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
            .AddMenuOption(new MenuOption("SubMenu Option 1"))
            .AddMenuOption(new MenuOption("SubMenu Option 2"))
            .AddMenuOption(new MenuOption("Back", actionType: ActionType.Back));

        _menu = new Menu()
            .AddMenuOption(new MenuOption("Menu Option 1"))
            .AddMenuOption(new MenuOption("Menu Option 2", _subMenu))
            .AddMenuOption(new MenuOption("Exit"));

        _consoleMock
            .SetupSequence(x => x.Prompt(It.IsAny<SelectionPrompt<string>>()))
            .Returns("Menu Option 2")
            .Returns("SubMenu Option 2");
    }

    private void TheActionIsTriggered()
    {
        throw new NotImplementedException();
    }

    private void AMenuOptionWithATriggerAction()
    {
        throw new NotImplementedException();
    }

}