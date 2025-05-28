
namespace AssignmentManagement.Core
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime? DueDate { get; private set; }
        public AssignmentPriority Priority { get; private set; }
        public bool IsCompleted { get; private set; }
        public string Notes { get; private set; }

        public Assignment(string title, string description, DateTime? dueDate = null, AssignmentPriority priority = AssignmentPriority.Medium, string notes = "")
        {
            ValidateStringParameter(title, nameof(title));
            Title = title;
            ValidateStringParameter(description, nameof(description));
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Notes = notes;
            IsCompleted = false;
        }

        public void Update(string newTitle, string newDescription)
        {
            ValidateStringParameter(newTitle, nameof(newTitle));
            Title = newTitle;
            ValidateStringParameter(newDescription, nameof(newDescription));
            Description = newDescription;
        }

        public void MarkComplete()
        {
            IsCompleted = true;
        }

        public bool IsOverdue()
        {
            return DueDate.Value < DateTime.Now; // BUG: no null check, ignores IsCompleted
        }

        public override string ToString()
        {
            return $"- {Title} ({Priority}) due {DueDate?.ToShortDateString() ?? "N/A"}\n{Description}\n\tNotes: {Notes}";
            // BUG: Notes not included in output
        }

        private static void ValidateStringParameter(string? stringParameter, string parameterName)
        {
            if (string.IsNullOrEmpty(stringParameter))
                throw new ArgumentException($"{parameterName} cannot be null or empty!");
        }
    }

    public enum AssignmentPriority
    {
        Low,
        Medium,
        High
    }
}
