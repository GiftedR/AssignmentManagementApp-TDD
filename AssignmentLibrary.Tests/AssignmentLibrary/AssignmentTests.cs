using Xunit;
using AssignmentLibrary;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Tests;

public class AssignmentTests
{
    [Fact]
    public void Constructor_ValidInput_ShouldCreateAssignment()
    {
        var assignment = new Assignment("Read Chapter 2", "Summarize key points");
        Assert.Equal("Read Chapter 2", assignment.Title);
        Assert.Equal("Summarize key points", assignment.Description);
    }

    [Fact]
    public void Constructor_BlankTitle_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Assignment("", "Valid description"));
    }

    [Fact]
    public void Constructor_ShouldHaveDefaultPriority()
    {
        Assignment assignment = new("Medium Priority", "Medium Priority Assignment");
        Assert.Equal(Priority.Medium, assignment.Priority);
    }

    [Fact]
    public void Constructor_HighPriority_ShouldSetPriorityToHigh()
    {
        Assignment assignment = new("High Priority", "High Priority Assignment", Priority.High);
        Assert.Equal(Priority.High, assignment.Priority);
    }

    [Fact]
    public void Constructor_ShouldAllowNoteInput()
    {
        Assignment assignment = new(
            "Testing of the Notes",
            "Cool note bro",
            note: "Really Cool Notes Bro"
        );

        Assert.NotNull(assignment.Note);
    }

    [Fact]
    public void Update_BlankDescription_ShouldThrowException()
    {
        var assignment = new Assignment("Read Chapter 2", "Summarize key points");
        Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", ""));
    }

    [Fact]
    public void Update_NewPriority_ShouldUpdatePriority()
    {
        var assignment = new Assignment("Read Chapter 8", "Summarize key points");
        assignment.Update("Read Chapter 8 & 9", "Summarize Both Chapters", Priority.High);

        Assert.Equal(Priority.High, assignment.Priority);
    }
    [Fact]
    public void MarkComplete_ShouldMarkCompleted()
    {
        var assignment = new Assignment("Week 3 TDD", "Introducing AssignmentService and Filtering with TDD");

        assignment.MarkComplete();

        Assert.True(assignment.IsCompleted);
    }

    [Fact]
    public void ToString_ShouldFormatAsAString()
    {
        Assignment assignment = new(
            "This is a Title",
            "This is a description"
        );
        string expectString;

        expectString = $"[{assignment.Id}]: {assignment.Title}, {assignment.Description} (Completed: {assignment.IsCompleted})";

        Assert.Equal(expectString, assignment.ToString());
    }

    [Fact]
    public void NotesProperty_ShouldBeOptionalString()
    {
        Assignment assignment = new(
            "Testing of the Notes",
            "Cool note bro"
        );

        Assert.Null(assignment.Note);
    }
}
