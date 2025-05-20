using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary;

public class Assignment
{
	public Guid Id { get; } = Guid.NewGuid();
	public string Title { get; private set; } = default!;
	public string Description { get; private set; } = default!;
	public bool IsCompleted { get; private set; }
	public Priority Priority { get; private set; }

	public Assignment(string title, string description, Priority priority = Priority.Medium)
	{
		Validate(title, nameof(title));
		Validate(description, nameof(description));
		Title = title;
		Description = description;
		Priority = priority;
	}

	public void Update(Assignment newassignment)
	{
		Validate(newassignment.Title, nameof(newassignment.Title));
		Validate(newassignment.Description, nameof(newassignment.Description));
		// BUG: Missing validation here
		Title = newassignment.Title;
		Description = newassignment.Description;
		Priority = newassignment.Priority;
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
