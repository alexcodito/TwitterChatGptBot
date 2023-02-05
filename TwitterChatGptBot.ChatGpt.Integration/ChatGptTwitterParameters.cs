using OpenAI.Models;

namespace TwitterChatGptBot.ChatGpt.Integration;

public sealed class ChatGptTwitterParameters
{
    private Model _model = Model.Ada;
    private double? _temperature = 0.1;

    public ChatGptTwitterParameters(Model model, double temperature)
    {
        _model = model;
        _temperature = temperature;
    }

    public void SetModel(Model model)
    {
        _model = model;
    }

    public void SetTemperature(double? temperature)
    {
        _temperature = temperature;
    }

    public Model Model => _model;

    public double? Temperature => _temperature;
}