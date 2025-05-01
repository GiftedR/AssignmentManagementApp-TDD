namespace AssignmentLibrary;

public class AssignmentService
{
	private List<Assignment> Assignments = new List<Assignment>();

	public List<Assignment> ListAll()
	{
		return Assignments;
	}

	// Create Assignment
	public void AddAssignment(Assignment assignment)
	{
		if (Contains(assignment))
		{
			throw new InvalidDataException("Cannot have duplicate Assignments");
		} else {
			Assignments.Add(assignment);
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

		if (completeAssignment == null) return false;

		completeAssignment.MarkComplete();
		return true;
	}
	public bool UpdateAssigment(string oldTitle, string newTitle, string newDescription)
	{
		Assignment? updateAssignment = FindAssignmentByTitle(oldTitle);
		if (updateAssignment == null) return false;
		if (!oldTitle.Equals(newTitle, StringComparison.OrdinalIgnoreCase)
			&& Assignments.Any(assi => assi.Title.Equals(newTitle, StringComparison.OrdinalIgnoreCase))
			) return false;
		
		updateAssignment.Update(newTitle, newDescription);
		return true;
	}
	// Delete Assignment
	public bool DeleteAssignment(string title)
	{
		Assignment? deleteAssignment = FindAssignmentByTitle(title);

		if (deleteAssignment == null) return false;

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