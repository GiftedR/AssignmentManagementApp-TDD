using Xunit;
using AssignmentLibrary;

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
    public void Update_BlankDescription_ShouldThrowException()
    {
        var assignment = new Assignment("Read Chapter 2", "Summarize key points");
        Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", ""));
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
}
