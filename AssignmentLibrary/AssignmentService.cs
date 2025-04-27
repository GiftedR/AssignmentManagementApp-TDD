namespace AssignmentLibrary;

public class AssignmentService
{
	private List<Assignment> Assignments = new List<Assignment>();

	public List<Assignment> ListAll()
	{
		return Assignments;
	}

	public void AddAssignment(Assignment assignment)
	{
		if (Contains(assignment))
		{
			throw new InvalidDataException("Cannot have duplicate Assignments");
		} else {
			Assignments.Add(assignment);
		}
	}

	public List<Assignment> ListIncomplete()
	{
		return Assignments.Where(a => a.IsCompleted == false).ToList();
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