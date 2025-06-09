using AssignmentLibrary;
using AssignmentManagementApp.UI;

using System;
using Microsoft.Extensions.DependencyInjection;
using AssignmentLibrary.Interfaces;

class Program
{
	public static void Main(string[] args)
	{
		ServiceCollection services = new();

        services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
        services.AddSingleton<IAppLogger, FileAppLogger>();
		services.AddSingleton<IAssignmentService, AssignmentService>();
		services.AddSingleton<ConsoleUI>();

		ServiceProvider serviceProvider = services.BuildServiceProvider();
		ConsoleUI consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();

		consoleUI.Run();
	}
}
