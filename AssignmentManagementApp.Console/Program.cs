using AssignmentLibrary;
using AssignmentManagementApp.UI;

using System;
using Microsoft.Extensions.DependencyInjection;

class Program
{
	public static void Main(string[] args)
	{
		ServiceCollection services = new();

		services.AddSingleton<AssignmentService>();
		services.AddSingleton<ConsoleUI>();

		ServiceProvider serviceProvider = services.BuildServiceProvider();
		ConsoleUI consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();

		consoleUI.Run();
	}
}
