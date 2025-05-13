using AssignmentLibrary.Interfaces;

namespace AssignmentManagementApp.UI;

public class ConsoleAppLogger : IAppLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG]: {message}");
    }
}