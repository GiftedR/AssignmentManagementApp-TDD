using System.Drawing;
using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentManagementApp.UI;

public class ConsoleUI
{
	private IAssignmentService _assignmentService;
	private IAssignmentFormatter _assignmentFormatter;
	public bool isRunning = true;

	public ConsoleUI(IAssignmentService assignmentservice, IAssignmentFormatter assignmentFormatter)
	{
		_assignmentService = assignmentservice;
		_assignmentFormatter = assignmentFormatter;
	}

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

	private void AddAssignment()
	{
		string title = CustomConsole.ReadInput("Enter Assignment Title: ");
		string description = CustomConsole.ReadInput("Enter Assignment Description: ");
		string enteredNewPriority = CustomConsole.ReadInputOnlyAllowed("Available Options: 1. (VL)Very Low 2. (L)ow 3. (M)edium 4. (H)igh 5.(XH)Extra High, or leave empty to default\nEnter priority: ", ["", "1", "2", "3", "4", "5", "vl", "l", "m", "h", "xh", "extra low", "low", "medium", "high", "extra high"]);
		
		try
		{
			Assignment assi = new(title, description, _assignmentFormatter.AssignmentPriorityFromString(enteredNewPriority) ?? Priority.Medium);
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

public static class CustomConsole
{
	public static readonly ConsoleColor DefaultConsoleColor;
	const ConsoleColor InfoColor = ConsoleColor.Blue;
	const ConsoleColor BadColor = ConsoleColor.Red;
	const ConsoleColor GoodColor = ConsoleColor.Green;

	static CustomConsole()
	{
		DefaultConsoleColor = Console.ForegroundColor;
	}

	public static void WriteInfo(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = InfoColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}

	public static void WriteSuccess(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = GoodColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}

	public static void WriteFail(string format, object? arg0 = null, object? arg1 = null, object? arg2 = null)
	{
		Console.ForegroundColor = BadColor;
		Console.Write(format, arg0, arg1, arg2);
		Console.ForegroundColor = DefaultConsoleColor;
	}

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