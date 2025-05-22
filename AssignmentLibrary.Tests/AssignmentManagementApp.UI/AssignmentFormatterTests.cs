using System;
using AssignmentManagementApp.UI;

namespace AssignmentLibrary.Tests.AssignmentManagementApp.UI;

public class AssignmentFormatterTests
{
    [Theory]
    [InlineData("Title", "Description")]
    [InlineData("Different Title", "Different Description")]
    [InlineData("Make thing", "Create a world")]
    [InlineData("Create program", "Build application")]
    [InlineData("Build Software", "Archetecture go brrrr")]
    public void Format_ShouldProperlyFormatFilledAssignment(string formattitle, string formatdescription)
    {
        Assignment formattingAssignment = new(
            formattitle,
            formatdescription
        );
        AssignmentFormatter formatter = new();
        string expectedFormat;

        expectedFormat = $"{formatter.StringFromAssignmentPriority(formattingAssignment.Priority)}: [{formattingAssignment.Id}] {formattingAssignment.Title} - {(formattingAssignment.IsCompleted ? "Completed" : "Incomplete")}";
    
        Assert.Equal(expectedFormat, formatter.Format(formattingAssignment));
    }
}
