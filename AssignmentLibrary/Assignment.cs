namespace AssignmentLibrary;

public class Assignment
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }

    public Assignment(string title, string description)
    {
        Update(title, description);
    }

    public void Update(string newTitle, string newDescription)
    {
        Validate(newTitle, nameof(newTitle));
        Validate(newDescription, nameof(newDescription));
        // BUG: Missing validation here
        Title = newTitle;
        Description = newDescription;
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
}
