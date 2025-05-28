namespace AssignmentManagement.Tests
{
    using AssignmentManagement.Core;
    public class AssignmentTests
    {
        [Fact]
        public void Constructor_ValidInput_ShouldCreateAssignment()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Equal("Read Chapter 2", assignment.Title);
            Assert.Equal("Summarize key points", assignment.Description);
            Assert.False(assignment.IsCompleted);
        }

        [Fact]
        public void Constructor_BlankTitle_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Assignment("", "Valid description"));
        }

        [Fact]
        public void Constructor_WithNotes_ShouldStoreNotes()
        {
            var notesmessage = "See, arent these cool notes (:";
            var assignment = new Assignment("Notes Assignment", "This is an assignment with notes", notes: notesmessage);

            Assert.Equal(notesmessage, assignment.Notes);
        }
        [Fact]
        public void Update_BlankDescription_ShouldThrowException()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", ""));
        }

        [Fact]
        public void MarkComplete_SetsIsCompletedToTrue()
        {
            var assignment = new Assignment("Task", "Complete the lab");
            assignment.MarkComplete();
            Assert.True(assignment.IsCompleted);
        }

        [Fact]
        public void ToString_ShouldShowNotesAsPartOfOutput()
        {
            var assignment = new Assignment("Notes name", "Notes description", notes: "Cool Notes (:");

            Assert.Contains(assignment.Notes, assignment.ToString());
        }

        [Fact]
        public void IsOverdue_WithNoDueDate_ShouldReturnFalse()
        {
            var assignment = new Assignment("Extra Credit", "Due at the end of the quarter");

            Assert.False(assignment.IsOverdue());
        }

        [Fact]
        public void IsOverdue_WithPastDueDate_ShouldReturnTrue()
        {
            var assignment = new Assignment("First Assignment", "Assignment to keep you enrolled in the class", new DateTime(2025, 4, 19, 23, 59, 59));

            Assert.True(assignment.IsOverdue());
        }

        [Fact]
        public void IsOverdue_WithIsComplete_ShouldReturnFalse()
        {
            var assignment = new Assignment("First Assignment", "Assignment to keep you enrolled in the class", new DateTime(2025, 6, 16, 23, 59, 59));
            assignment.MarkComplete();

            Assert.False(assignment.IsOverdue());
        }
    }
}
