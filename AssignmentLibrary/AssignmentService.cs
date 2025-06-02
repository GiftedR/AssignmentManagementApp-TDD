using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary;

/// <summary>
/// Handles creating, reading, updating, and deleting of assignments. 
/// </summary>
public class AssignmentService : IAssignmentService
{
	/// <summary>
	/// The list of assignments, holds all currently referenced assignments.
	/// </summary>
	private List<Assignment> Assignments = new List<Assignment>();
	/// <summary>
	/// The stored dependancy of the formatter.
	/// </summary>
	private IAssignmentFormatter _formatter;
	/// <summary>
	/// The stored dependancy of the logger.
	/// </summary>
	private IAppLogger _logger;
	/// <summary>
	/// Creates a new AssignmentFormatter.
	/// </summary>
	/// <param name="formatter">The injected formatter dependancy.</param>
	/// <param name="logger">The injected logger dependancy.</param>
	public AssignmentService(IAssignmentFormatter formatter, IAppLogger logger)
	{
		_formatter = formatter;
		_logger = logger;
	}
	/// <summary>
	/// Gets all of the assignments.
	/// </summary>
	/// <returns>A list of all registered assignments.</returns>
	public List<Assignment> ListAll()
	{
		return Assignments;
	}

	/// <summary>
	/// Adds an assignment to the service, checks for any duplicates, and throws exception if found.
	/// </summary>
	/// <param name="assignment">The assignment to attempted to be added.</param>
	/// <returns>True if adding was successful, otherwise throws exception.</returns>
	/// <exception cref="InvalidDataException">The exception when a duplicate was found.</exception>
	public bool AddAssignment(Assignment assignment)
	{
		if (Contains(assignment))
		{
			_logger.Log("Attempted to add duplicate assignment. Throwing Exception...");
			throw new InvalidDataException("Cannot have duplicate Assignments");
		}
		else
		{
			_logger.Log($"New Assignment Added: {_formatter.Format(assignment)}");
			Assignments.Add(assignment);
			return true;
		}
	}

	/// <summary>
	/// Gets a list of all non complete assignments.
	/// </summary>
	/// <returns>A List of incomplete assignments.</returns>
	public List<Assignment> ListIncomplete()
	{
		return Assignments.Where(a => a.IsCompleted == false).ToList();
	}

	/// <summary>
	/// Finds one assignment, or null if it cant find one.
	/// </summary>
	/// <param name="title">The title of the assignment to be found, wording needs to match, capitalization does not.</param>
	/// <returns>The found assignment, or null if not found.</returns>
	public Assignment? FindAssignmentByTitle(string title)
		=> Assignments.FirstOrDefault(assi => assi.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

	/// <summary>
	/// Marks a found assignment as completed.
	/// </summary>
	/// <param name="title">The title of the assignment to be found. Searches using <see cref="FindAssignmentByTitle"/>.</param>
	/// <returns>True or False depending on if it succeeded in marking it as completed.</returns>
	public bool MarkAssignmentComplete(string title)
	{
		Assignment? completeAssignment = FindAssignmentByTitle(title);

		if (completeAssignment == null)
		{
			_logger.Log($"Assignment not found by title: {title} \n Can't mark as complete.");
			return false;
		}
		_logger.Log($"Assignment Completed: {_formatter.Format(completeAssignment)}");

		completeAssignment.MarkComplete();
		return true;
	}
	/// <summary>
	/// Updates the assignment with the name of oldTitle.
	/// </summary>
	/// <param name="oldTitle">The current title of the assignment, uses <see cref="FindAssignmentByTitle"/></param>
	/// <param name="newTitle">The title to replace the current title.</param>
	/// <param name="newDescription">The description to replace the current description.</param>
	/// <param name="newPriority">The priority to replace the current priority, or leave unchanged if it's null.</param>
	/// <returns>True or False depending on if it succeeded in updating.</returns>
	public bool UpdateAssigment(string oldTitle, string newTitle, string newDescription, Priority? newPriority = null)
	{
		Assignment? updateAssignment = FindAssignmentByTitle(oldTitle);
		if (updateAssignment == null)
		{
			_logger.Log($"Assignment not found by title: {oldTitle} \n Can't Update.");
			return false;
		}
		if (!oldTitle.Equals(newTitle, StringComparison.OrdinalIgnoreCase)
			&& Assignments.Any(assi => assi.Title.Equals(newTitle, StringComparison.OrdinalIgnoreCase))
			) return false;

		_logger.Log($"Assignment Updated: {_formatter.Format(updateAssignment)}\n With New Details: {newTitle}, {newDescription}");
		updateAssignment.Update(newTitle, newDescription, newPriority);
		return true;
	}
	/// <summary>
	/// Deletes a specified assignment.
	/// </summary>
	/// <param name="title">The title of the assignment, uses <see cref="FindAssignmentByTitle"/></param>
	/// <returns>True or False depending on if it succeeded in deleting.</returns>
	public bool DeleteAssignment(string title)
	{
		Assignment? deleteAssignment = FindAssignmentByTitle(title);

		if (deleteAssignment == null)
		{
			_logger.Log($"Assignment not found by title: {title} \n Can't Delete.");
			return false;
		}
		_logger.Log($"Deleting Assignment: {_formatter.Format(deleteAssignment)}");
		Assignments.Remove(deleteAssignment);
		return true;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="assignment"></param>
	/// <returns></returns>
	private bool Contains(Assignment assignment)
	{
		foreach (Assignment a in Assignments)
		{
			if (a.Title == assignment.Title)
			{
				return true;
			}
		}
		return false;
	}
}