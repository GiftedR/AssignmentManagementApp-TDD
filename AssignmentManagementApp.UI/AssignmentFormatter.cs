using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using static AssignmentLibrary.Enumerations;

namespace AssignmentManagementApp.UI;

/// <summary>
/// Formats an assignment and its parts.
/// </summary>
public class AssignmentFormatter : IAssignmentFormatter
{
	/// <summary>
	/// Turns a string representative into its priority. Meant to be the counter of <see cref="StringFromAssignmentPriority"/>
	/// </summary>
	/// <param name="prioritymessage">Keyword meant to represent a priority.</param>
	/// <returns>Priority if the keyword matches a priority or null if it can't find one.</returns>
	public Priority? AssignmentPriorityFromString(string prioritymessage) => prioritymessage.ToLower() switch
	{
		"1" or "vl" or "very low" => Priority.VeryLow,
		"2" or "l" or "low" => Priority.Low,
		"3" or "m" or "medium" => Priority.Medium,
		"4" or "h" or "high" => Priority.High,
		"5" or "xh" or "extra high" => Priority.ExtraHigh,
		_ => null
	};
	/// <summary>
	/// Formats an assignment into a string form.
	/// </summary>
	/// <param name="assignment">The assignment to be formatted.</param>
	/// <returns>String representative of the assignment.</returns>
	public string Format(Assignment assignment)
	{
		return $"{StringFromAssignmentPriority(assignment.Priority)}: [{assignment.Id}] {assignment.Title} - {(assignment.IsCompleted ? "Completed" : "Incomplete")}\nNote:{(assignment.Note ?? "None")}";
	}

	/// <summary>
	/// Turns a priority into a keyword representation. Meant to be the counter of <see cref="AssignmentPriorityFromString"/>
	/// </summary>
	/// <param name="priority">The priority to be changed.</param>
	/// <returns>A string keyword for the priority.</returns>
	public string StringFromAssignmentPriority(Priority? priority) => priority switch
	{
		Priority.VeryLow => "Very Low",
		Priority.Low => "Low",
		Priority.Medium => "Medium",
		Priority.High => "High",
		Priority.ExtraHigh => "Extra High",
		_ => "Unknown"
	};
}