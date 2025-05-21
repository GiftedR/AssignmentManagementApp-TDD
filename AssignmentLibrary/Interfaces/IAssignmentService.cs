using System;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Interfaces;

public interface IAssignmentService
{
	public List<Assignment> ListAll();
	public bool AddAssignment(Assignment assignment);
	public List<Assignment> ListIncomplete();
	public Assignment? FindAssignmentByTitle(string title);
	public bool MarkAssignmentComplete(string title);
	public bool UpdateAssigment(string oldTitle, string newTitle, string newDescription, Priority? newPriority = null);
	public bool DeleteAssignment(string title);
}
