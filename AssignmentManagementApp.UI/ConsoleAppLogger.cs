using AssignmentLibrary.Interfaces;

namespace AssignmentManagementApp.UI;

/// <summary>
/// Handles logging of messages to the console.
/// </summary>
public class ConsoleAppLogger : IAppLogger
{
    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}