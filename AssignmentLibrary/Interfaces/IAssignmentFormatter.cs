using System;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Interfaces;

public interface IAssignmentFormatter
{
	public string Format(Assignment assignment);
	public Priority? AssignmentPriorityFromString(string prioritymessage);
	public string StringFromAssignmentPriority(Priority? priority);
}
