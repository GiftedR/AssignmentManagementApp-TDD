using System;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Interfaces;

/// <summary>
/// Used to format an Assignment and its parts.
/// </summary>
public interface IAssignmentFormatter
{
	/// <summary>
	/// Formats an assignment into a string form.
	/// </summary>
	/// <param name="assignment">The assignment to be formatted.</param>
	/// <returns>String representative of the assignment.</returns>
	public string Format(Assignment assignment);
	/// <summary>
	/// Turns a string representative into its priority. Meant to be the counter of <see cref="StringFromAssignmentPriority"/>
	/// </summary>
	/// <param name="prioritymessage">Keyword meant to represent a priority.</param>
	/// <returns>Priority if the keyword matches a priority or null if it can't find one.</returns>
	public Priority? AssignmentPriorityFromString(string prioritymessage);
	/// <summary>
	/// Turns a priority into a keyword representation. Meant to be the counter of <see cref="AssignmentPriorityFromString"/>
	/// </summary>
	/// <param name="priority">The priority to be changed.</param>
	/// <returns>A string keyword for the priority.</returns>
	public string StringFromAssignmentPriority(Priority? priority);
}
