using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentManagementApp.UI;

public class AssignmentFormatter : IAssignmentFormatter
{
	public Priority? AssignmentPriorityFromString(string prioritymessage) => prioritymessage.ToLower() switch
	{
		"1" or "vl" or "very low" => Priority.VeryLow,
		"2" or "l" or "low" => Priority.Low,
		"3" or "m" or "medium" => Priority.Medium,
		"4" or "h" or "high" => Priority.High,
		"5" or "xh" or "extra high" => Priority.ExtraHigh,
		_ => null
	};

	public string Format(Assignment assignment)
	{
		return $"{StringFromAssignmentPriority(assignment.Priority)}: [{assignment.Id}] {assignment.Title} - {(assignment.IsCompleted ? "Completed" : "Incomplete")}";
	}

	public string StringFromAssignmentPriority(Priority priority) => priority switch
	{
		Priority.VeryLow => "Very Low",
		Priority.Low => "Low",
		Priority.Medium => "Medium",
		Priority.High => "High",
		Priority.ExtraHigh => "Extra High",
		_ => "Unknown"
	};
}