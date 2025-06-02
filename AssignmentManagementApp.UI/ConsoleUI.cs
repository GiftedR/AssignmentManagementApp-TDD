using System.Drawing;
using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentManagementApp.UI;

/// <summary>
/// Handles user interaction between the console and AssignmentService.
/// </summary>
public class ConsoleUI
{
	/// <summary>
	/// The stored dependancy of the service.
	/// </summary>
	private IAssignmentService _assignmentService;
	/// <summary>
	/// The stored dependancy of the formatter.
	/// </summary>
	private IAssignmentFormatter _assignmentFormatter;
	/// <summary>
	/// Handles checking if the console app is still running.
	/// </summary>
	public bool isRunning = true;
	/// <summary>
	/// Creates a new ConsoleUI.
	/// </summary>
	/// <param name="assignmentservice">The injected service dependancy.</param>
	/// <param name="assignmentFormatter">The injected formatter dependancy.</param>
	public ConsoleUI(IAssignmentService assignmentservice, IAssignmentFormatter assignmentFormatter)
	{
		_assignmentService = assignmentservice;
		_assignmentFormatter = assignmentFormatter;
	}

	/// <summary>
	/// Begins the interaction with the user. Handles prompts and input to action routing.
	/// </summary>
	public void Run()
	{
		const string MenuOptions = @"
Assignment Manager Menu:
	1. Add Assignment
	2. List All Assignments
	3. List Incomplete Assignments
	4. Mark Assignment as Complete
	5. Search Assignment by Title
	6. Update Assignment
	7. Delete Assignment
	0. Exit
";
		string input = "";
		do
		{
			CustomConsole.WriteInfo(MenuOptions);
			input = CustomConsole.ReadInput("Choose an Option: ");

			switch (input)
			{
				case "0":
					isRunning = false;
					CustomConsole.WriteInfo("Goodbye (: \n");
					break;
				case "1":
					AddAssignment();
					break;
				case "2":
					ListAllAssignments();
					break;
				case "3":
					ListIncompleteAssignments();
					break;
				case "4":
					MarkAssignmentComplete();
					break;
				case "5":
					SearchAssignmentByTitle();
					break;
				case "6":
					UpdateAssignment();
					break;
				case "7":
					DeleteAssignment();
					break;
				default:
					CustomConsole.WriteInfo("Unknown Option. Try Again.\n");
					break;
			}
		}
		while (isRunning);
	}

	/// <summary>
	/// Collects the user input, and attempts to add the created assignment using the injected <see cref="AssignmentService"/>
	/// </summary>
	private void AddAssignment()
	{
		string title = CustomConsole.ReadInput("Enter Assignment Title: ");
		string description = CustomConsole.ReadInput("Enter Assignment Description: ");
		string enteredNewPriority = CustomConsole.ReadInputOnlyAllowed("Available Options: 1. (VL)Very Low 2. (L)ow 3. (M)edium 4. (H)igh 5.(XH)Extra High, or leave empty to default\nEnter priority: ", ["", "1", "2", "3", "4", "5", "vl", "l", "m", "h", "xh", "extra low", "low", "medium", "high", "extra high"]);
		Console.WriteLine("Enter Note:");
		string? enteredNote = Console.ReadLine();

		try
		{
			Assignment assi = new(title, description, _assignmentFormatter.AssignmentPriorityFromString(enteredNewPriority) ?? Priority.Medium, enteredNote);
			if (_assignmentService.AddAssignment(assi))
			{
				CustomConsole.WriteSuccess("Assignment Added!");
			}
			else
			{
				CustomConsole.WriteFail("Assignment is Duplicate.");
			}
		}
		catch (Exception e)
		{
			CustomConsole.WriteFail($"Exception: {e}");
		}
	}

	/// <summary>
	/// Lists all the currently stored assignments.
	/// </summary>
	private void ListAllAssignments()
	{
		List<Assignment> assignments = _assignmentService.ListAll();
		if (assignments.Count == 0)
		{
			CustomConsole.WriteInfo("No Assignments Found.");
			return;
		}
		assignments.ForEach(a => CustomConsole.WriteInfo($"{(a.IsCompleted ? "[X]" : "[ ]")} {a}\n"));
	}

	/// <summary>
	/// Lists all the assignments that aren't marked as completed.
	/// </summary>
	private void ListIncompleteAssignments()
	{
		List<Assignment> assignments = _assignmentService.ListIncomplete();
		if (assignments.Count == 0)
		{
			CustomConsole.WriteInfo("No Assignments Found.");
			return;
		}
		assignments.ForEach(a => CustomConsole.WriteInfo($"Incomplete: {a}\n"));
	}

	/// <summary>
	/// Collects the user input, and attempts to mark a specific assignment completed using the injected <see cref="AssignmentService"/> 
	/// </summary>
	private void MarkAssignmentComplete()
	{
		string enteredTitle = CustomConsole.ReadInput("Enter the title of the assignment to mark as completed: ");
		if (enteredTitle != null && _assignmentService.MarkAssignmentComplete(enteredTitle))
		{
			CustomConsole.WriteSuccess("Assignment Completed!");
		}
		else
		{
			CustomConsole.WriteFail("Assignment Not Found");
		}
	}

	/// <summary>
	/// Collects the user input, and searches an assignment by the title. Needs to match words, but not case.
	/// </summary>
	private void SearchAssignmentByTitle()
	{
		string enteredTitle = CustomConsole.ReadInput("Enter Title To Search: ");
		Assignment? foundAssignment = _assignmentService.FindAssignmentByTitle(enteredTitle);
		if (foundAssignment == null)
		{
			CustomConsole.WriteFail("Assignment Not Found");
		}
		else
		{
			CustomConsole.WriteSuccess($"Found: {foundAssignment}");
		}
	}

	/// <summary>
	/// Collects the user input, and attempts to update the found assignment.
	/// </summary>
	private void UpdateAssignment()
	{
		string enteredOldTitle = CustomConsole.ReadInput("Enter the current title of the Assignment: ");
		string enteredNewTitle = CustomConsole.ReadInput("Enter the new title of the Assignment: ");
		string enteredNewDescription = CustomConsole.ReadInput("Enter the new description of the Assignment: ");
		string enteredNewPriority = CustomConsole.ReadInputOnlyAllowed("Available Options: 1. (VL)Very Low 2. (L)ow 3. (M)edium 4. (H)igh 5.(XH)Extra High, or leave empty to leave unchanged\nEnter new priority: ", ["", "1", "2", "3", "4", "5", "vl", "l", "m", "h", "xh", "extra low", "low", "medium", "high", "extra high"]);


		if (_assignmentService.UpdateAssigment(enteredOldTitle, enteredNewTitle, enteredNewDescription, _assignmentFormatter.AssignmentPriorityFromString(enteredNewPriority)))
		{
			CustomConsole.WriteSuccess($"Assignment Updated Successfully. {enteredOldTitle} -> {enteredNewTitle}");
		}
		else
		{
			CustomConsole.WriteFail("Update Failed: Title may conflict of assignment may not be found.");
		}
	}
	/// <summary>
	/// Collects the user input, and attempts to delete the found assignment.
	/// </summary>
	private void DeleteAssignment()
	{
		string enteredTitle = CustomConsole.ReadInput("Enter the title of the assignment to delete: ");
		if (_assignmentService.DeleteAssignment(enteredTitle))
		{
			CustomConsole.WriteSuccess("Assignment Delete Successfully.");
		}
		else
		{
			CustomConsole.WriteFail("Assignment Not Found.");
		}
	}
}

/// <summary>
/// Acts as a custom console inbetween.
/// </summary>
public static class CustomConsole
{
	/// <summary>
	/// The default color of the console text.
	/// </summary>
	public static readonly ConsoleColor DefaultConsoleColor;
	/// <summary>
	/// The color of messages marked as info messages.
	/// </summary>
	const ConsoleColor InfoColor = ConsoleColor.Blue;
	/// <summary>
	/// The color of messages that are marked as bad messages.
	/// </summary>
	const ConsoleColor BadColor = ConsoleColor.Red;
	/// <summary>
	/// The color of messages that are marked as good messages.
	/// </summary>
	const ConsoleColor GoodColor = ConsoleColor.Green;

	/// <summary>
	/// Static constructor.
	/// </summary>
	static CustomConsole()
	{
		DefaultConsoleColor = Console.ForegroundColor;
	}

	/// <summary>
	/// Acts as an inbetween for the console write and custom console.
	/// </summary>
	/// <param name="format">The string to be printed</param>
	/// <param name="arg0">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg1">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg2">Just an object passthrough for <see cref="Console.Write"/></param>
	public static void WriteInfo(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = InfoColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}
	/// <summary>
	/// Writes a message with the <see cref="GoodColor"/> text.
	/// </summary>
	/// <param name="format">The string to be printed</param>
	/// <param name="arg0">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg1">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg2">Just an object passthrough for <see cref="Console.Write"/></param>
	public static void WriteSuccess(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = GoodColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}
	/// <summary>
	/// Writes a message with the <see cref="BadColor"/> text.
	/// </summary>
	/// <param name="format">The string to be printed</param>
	/// <param name="arg0">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg1">Just an object passthrough for <see cref="Console.Write"/></param>
	/// <param name="arg2">Just an object passthrough for <see cref="Console.Write"/></param>
	public static void WriteFail(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = BadColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}

	/// <summary>
	/// Reads the input from the user and makes sure that it is valid.
	/// </summary>
	/// <param name="promptmessage">The message to prompt the user for input.</param>
	/// <returns>A valid string input</returns>
	public static string ReadInput(string promptmessage)
	{
		string input = "";

		while (string.IsNullOrEmpty(input))
		{
			WriteInfo(promptmessage);
			input = Console.ReadLine() ?? "";
			Console.WriteLine();
		}

		return input;
	}
	/// <summary>
	/// Reads the input from the user and makes sure that it is one that is contained in the list.
	/// </summary>
	/// <param name="promptmessage">The message to prompt the user for input.</param>
	/// <param name="availableinputs">The list of allowed inputs.</param>
	public static string ReadInputOnlyAllowed(string promptmessage, string[] availableinputs)
	{
		string input = "";

		do
		{
			WriteInfo(promptmessage);
			input = Console.ReadLine() ?? "";
			Console.WriteLine();
		} while (!availableinputs.Contains(input.ToLower()));
		return input;
	}
}