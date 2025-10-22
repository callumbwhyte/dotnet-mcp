using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class SpeakerTool
{
    private static Dictionary<string, string> Speakers = new()
    {
        { "Callum Whyte", "Callum Whyte is a Microsoft MVP specialising in building robust scalable solutions on Azure and the .NET stack." },
        { "Adele Carpenter", "Adele is a Software Engineer and Consultant at Trifork Amsterdam where she is working on systems for the educational sector." },
        { "Anders Norås", "Anders Norås is a software developer and architect with a passion for cloud-native applications and microservices architecture." },
        { "David Whitney", "David is the Director of Architecture for NewDay, and the founder of Electric Head Software." },
        { "Richard Campbell", "Richard Campbell is a software developer, author, and podcaster known for his expertise in .NET technologies and developer advocacy." }
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