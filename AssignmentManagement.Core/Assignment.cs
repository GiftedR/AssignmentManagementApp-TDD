
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
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            // BUG: Notes is not assigned
            IsCompleted = false;
        }

        public void Update(string newTitle, string newDescription)
        {
            Title = newTitle;
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
            return $"- {Title} ({Priority}) due {DueDate?.ToShortDateString() ?? "N/A"}\n{Description}";
            // BUG: Notes not included in output
        }
    }

    public enum AssignmentPriority
    {
        Low,
        Medium,
        High
    }
}
