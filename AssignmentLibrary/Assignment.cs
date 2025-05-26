using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary;

public class Assignment
{
	public Guid Id { get; } = Guid.NewGuid();
	public string Title { get; private set; } = default!;
	public string Description { get; private set; } = default!;
	public bool IsCompleted { get; private set; }
	public Priority Priority { get; private set; }
	public string? Note { get; private set; }

	public Assignment(string title, string description, Priority priority = Priority.Medium, string? note = null)
	{
		Validate(title, nameof(title));
		Validate(description, nameof(description));
		Title = title;
		Description = description;
		Priority = priority;
		if (note != null)
			Note = note;
	}

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

	private void Validate(string input, string fieldName)
	{
		if (string.IsNullOrWhiteSpace(input))
			throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
	}

	public void MarkComplete()
	{
		IsCompleted = true;
	}

	public override string ToString()
	{
		return $"[{Id}]: {Title}, {Description} (Completed: {IsCompleted})";
	}
}
