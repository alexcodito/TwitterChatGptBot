using OpenAI;

namespace TwitterChatGptBot.ChatGpt.Integration
{
    public sealed class ChatGptTwitterIntegration
    {
        private readonly OpenAIClient _openAiClient;
        private readonly ChatGptTwitterParameters _parameters;

        public ChatGptTwitterIntegration(string apiKey, ChatGptTwitterParameters parameters)
        {
            _openAiClient = new OpenAIClient(new OpenAIAuthentication(apiKey));
            _parameters = parameters;
        }

        public async Task<string> GenerateTweetFromTopic(string topic, int maxTweetLength = 280)
        {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic), "Topic is required");

            if (topic.Length <= 2)
            {
                throw new ArgumentException("The supplied topic is too short", nameof(topic));
            }

            if (maxTweetLength < 10)
            {
                throw new ArgumentException("Tweets must be at least 10 characters in length", nameof(maxTweetLength));
            }
            
            if (maxTweetLength > 280)
            {
                throw new ArgumentException("Tweets cannot exceed 280 characters in length", nameof(maxTweetLength));
            }

            var prompt = $"Write a {maxTweetLength} character tweet on this topic: {topic}";

            var result = await _openAiClient.CompletionsEndpoint
                .CreateCompletionAsync(prompt, temperature: _parameters.Temperature, model: _parameters.Model);

            return result.Object;
        }
    }
}