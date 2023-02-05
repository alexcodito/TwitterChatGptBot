namespace TwitterChatGptBot.Client.Configuration;

public interface IBotConfiguration
{
    public IChatGptConfiguration ChatGptConfiguration { get; }
    public ITwitterConfiguration TwitterConfiguration { get; }
}