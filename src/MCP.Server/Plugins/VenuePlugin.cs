using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.SemanticKernel;

namespace MCP_Server.Plugins;

public class VenuePlugin
{
    [KernelFunction]
    [Description("Gets todays lunch menu")]
    public static string GetLunchMenu()
    {
        var random = new Random();

        var options = new[] { "Pizza", "Salad", "Burger" };

        return options[random.Next(options.Length)];
    }

    [KernelFunction]
    [Description("Finds the room for a specific speaker")]
    [Authorize(Roles = "Speaker")]
    public static string FindSpeakersRoom()
    {
        var random = new Random();

        return $"Room #{random.Next(100)}";
    }
}