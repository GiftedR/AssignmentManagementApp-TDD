using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary;

/// <summary>
/// Model of an assignment, holds a title, description, priority, and note.
/// </summary>
public class Assignment
{
	/// <summary>
	/// The unique ID of the assignment.
	/// </summary>
	public Guid Id { get; } = Guid.NewGuid();
	/// <summary>
	/// The name of the assignment.
	/// </summary>
	public string Title { get; private set; } = default!;
	/// <summary>
	/// A description of the assignment.
	/// </summary>
	public string Description { get; private set; } = default!;
	/// <summary>
	/// Determines whether the assignment is completed or not.
	/// </summary>
	public bool IsCompleted { get; private set; }
	/// <summary>
	/// The priority of the assignment.
	/// </summary>
	public Priority Priority { get; private set; }
	/// <summary>
	/// Holds additional details about this assignment.
	/// </summary>
	public string? Note { get; private set; }

	/// <summary>
	/// Creates a new Assignment
	/// </summary>
	/// <param name="title">The title of this assignment. Gets stored in <see cref="Title"/></param>
	/// <param name="description">The description of this assignment. Gets stored in <see cref="Description"/></param>
	/// <param name="priority">The priority of this assignment. Gets stored in <see cref="Priority"/>. Defaults to medium.</param>
	/// <param name="note">Any additional notes for the assignment, or none if null. Gets stored in <see cref="Note" /></param>
	public Assignment(string title, string description, Priority priority = Priority.Medium, string? note = null)
	{
		Validate(title, nameof(title));
		Validate(description, nameof(description));
		Title = title;
		Description = description;
		Priority = priority;
		if (!string.IsNullOrWhiteSpace(note))
			Note = note;
	}

	/// <summary>
	/// Updates the current assignment with new data.
	/// </summary>
	/// <param name="newtitle">Replaces the current title with this new title</param>
	/// <param name="newdescription">Replaces the current description with this new descriptipn.</param>
	/// <param name="newpriority">Replaces the current priority with this new priority, or leaves it unchanged if it's null</param>
	/// <param name="newnote">Replaces the current note with this new note, or leaves it unchanged if it's null.</param>
	public void Update(string newtitle, string newdescription, Priority? newpriority = null, string? newnote = null)
	{
		Validate(newtitle, nameof(newtitle));
		Validate(newdescription, nameof(newdescription));
		Title = newtitle;
		Description = newdescription;
		if (newpriority != null)
			Priority = (Priority)newpriority;
		if (newnote != null)
			Note = newnote;
	}

	/// <summary>
	/// Validates the passed in string, throws exception if it fails validation.
	/// </summary>
	/// <param name="input">The value to be validated.</param>
	/// <param name="fieldName">The name of the value, used when throwing the exception.</param>
	/// <exception cref="ArgumentException">Exception for when the value fails validation.</exception>
	private void Validate(string input, string fieldName)
	{
		if (string.IsNullOrWhiteSpace(input))
			throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
	}

	/// <summary>
	/// Markes the assignment as completed.
	/// </summary>
	public void MarkComplete()
	{
		IsCompleted = true;
	}

	/// <summary>
	/// Converts the assignment into a string for debugging purposes.
	/// </summary>
	/// <returns>String representation of the assignment</returns>
	public override string ToString()
	{
		return $"[{Id}]: {Title}, {Description} (Completed: {IsCompleted})";
	}
}
