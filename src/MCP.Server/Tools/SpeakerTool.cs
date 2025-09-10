using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class SpeakerTool
{
    private static Dictionary<string, string> Speakers = new()
    {
        { "Callum Whyte", "Callum Whyte is a Microsoft MVP specialising in building robust scalable solutions on Azure and the .NET stack." },
        { "Heather Downing", "Heather is a passionate coder and entrepreneur. She has experience working with Fortune 500 companies building enterprise level mobile and .NET applications." },
        { "Lars Klint", "Lars is a developer advocate, an author, trainer, Microsoft Azure MVP, community leader, aspiring YouTube host and part time classic car collector." },
        { "Richard Campbell", "Richard is the host of .NET Rocks, RunAs Radio and Windows Weekly" }
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