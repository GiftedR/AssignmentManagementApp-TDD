using System;

namespace Week6NoteApp.Services;

public class ConsoleLogger : ILogger
{
	public void Log(string message)
	{
		Console.WriteLine($"[LOG] {message}");
	}
}
