using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary;

public class AssignmentService : IAssignmentService
{
	private List<Assignment> Assignments = new List<Assignment>();

	private IAssignmentFormatter _formatter; 
	private IAppLogger _logger;	
	public AssignmentService(IAssignmentFormatter formatter, IAppLogger logger)
	{
		_formatter = formatter;
		_logger = logger;
	}

	public List<Assignment> ListAll()
	{
		return Assignments;
	}

	// Create Assignment
	public bool AddAssignment(Assignment assignment)
	{
		if (Contains(assignment))
		{
			_logger.Log("Attempted to add duplicate assignment. Throwing Exception...");
			throw new InvalidDataException("Cannot have duplicate Assignments");
		} else {
			_logger.Log($"New Assignment Added: {_formatter.Format(assignment)}");
			Assignments.Add(assignment);
			return true;
		}
	}

	// Read All Assignment
	public List<Assignment> ListIncomplete()
	{
		return Assignments.Where(a => a.IsCompleted == false).ToList();
	}

	// Read One Assignment
	public Assignment? FindAssignmentByTitle(string title) 
		=> Assignments.FirstOrDefault(assi => assi.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

	// Update Assignment IsComplete
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
	// Delete Assignment
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