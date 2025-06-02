using System;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Interfaces;
/// <summary>
/// Provides a templete for creating, reading, updating, and deleting a list of assignments.
/// </summary>
public interface IAssignmentService
{
	/// <summary>
	/// Gets all the assignment the service has.
	/// </summary>
	/// <returns>A list of all registered assignments.</returns>
	public List<Assignment> ListAll();
	/// <summary>
	/// Adds and assignment to the service.
	/// </summary>
	/// <param name="assignment">The assignment to attempt to add.</param>
	/// <returns>True or false depending on if it succeeded or not.</returns>
	public bool AddAssignment(Assignment assignment);
	/// <summary>
	/// Shows a list of assigments that are marked as incomplete.
	/// </summary>
	/// <returns>A list of assignments that are not complete.</returns>
	public List<Assignment> ListIncomplete();
	/// <summary>
	/// Looks within the service and finds the one that matched the title.
	/// </summary>
	/// <param name="title">The title to be searched.</param>
	/// <returns>The assignment with a matching title, or null if it can't find one.</returns>
	public Assignment? FindAssignmentByTitle(string title);
	/// <summary>
	/// Marks a specific assignment complete.
	/// </summary>
	/// <param name="title">The title of the assignment to be found.</param>
	/// <returns>True or False depending on if it succeded or not.</returns>
	public bool MarkAssignmentComplete(string title);
	/// <summary>
	/// Updates an assignment with new details.
	/// </summary>
	/// <param name="oldTitle">The title of the current assignment.</param>
	/// <param name="newTitle">The title to replace the current title of the selected assignment.</param>
	/// <param name="newDescription">The description to replace the current description of the selected assignment.</param>
	/// <param name="newPriority">The new priority of the selected assignment, leaving null will leave it the same.</param>
	/// <returns>True or False depending on if it succeded or not.</returns>
	public bool UpdateAssigment(string oldTitle, string newTitle, string newDescription, Priority? newPriority = null);
	/// <summary>
	/// Deletes the assignment by title.
	/// </summary>
	/// <param name="title">The title of the assignment to be deleted.</param>
	/// <returns>True or False depending on if it succeded or not.</returns>
	public bool DeleteAssignment(string title);
}
