using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class SpeakerTool
{
    private static Dictionary<string, string> Speakers = new()
    {
        { "Callum Whyte", "Callum Whyte is a Microsoft MVP specialising in building robust scalable solutions on Azure and the .NET stack." },
        { "Dylan Beattie", "Dylan Beattie is an independent consultant who has been building data-driven web applications since the 1990s." },
        { "Jodie Burchell", "Dr. Jodie Burchell is the Developer Advocate in Data Science at JetBrains, and was previously a Lead Data Scientist at Verve Group Europe." },
        { "Lars Klint", "Lars is a developer advocate, an author, trainer, Microsoft Azure MVP, community leader, aspiring YouTube host and part time classic car collector." },
        { "Rendle", "Rendle is the founder of RendleLabs, which provides consulting services and workshops to .NET development teams across all industries." }
    };

    private static Dictionary<string, IList<int>> SpeakerRatings = new();

    [McpServerTool]
    [Description("Gets the biography of a speaker")]
    public static string? GetSpeaker(string name)
    {
        Speakers.TryGetValue(name, out var bio);

        return bio;
    }

    [McpServerTool]
    [Description("Gets the average ratings for each speaker")]
    public static IEnumerable<string> GetSpeakerRatings()
    {
        foreach (var (name, ratings) in SpeakerRatings)
        {
            yield return $"{name} has a score of {ratings.Average()}";
        }
    }

    [McpServerTool]
    [Description("Rate or score a speaker from 1 to 5")]
    public static void RateSpeaker(string name, int score)
    {
        if (SpeakerRatings.TryGetValue(name, out var ratings) == false)
        {
            ratings = [];
        }

        ratings.Add(score);

        SpeakerRatings[name] = ratings;
    }
}