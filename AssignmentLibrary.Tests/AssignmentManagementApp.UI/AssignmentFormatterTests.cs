using System;
using AssignmentManagementApp.UI;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Tests.AssignmentManagementApp.UI;

public class AssignmentFormatterTests
{
	[Theory]
	[InlineData("Title", "Description")]
	[InlineData("Different Title", "Different Description")]
	[InlineData("Make thing", "Create a world", "Big ol project")]
	[InlineData("Create program", "Build application")]
	[InlineData("Build Software", "Archetecture go brrrr", "Better than crashing")]
	public void Format_ShouldProperlyFormatFilledAssignment(string formattitle, string formatdescription, string? formatnote = null)
	{
		Assignment formattingAssignment = new(
			formattitle,
			formatdescription,
			note: formatnote
		);
		AssignmentFormatter formatter = new();
		string expectedFormat;

		expectedFormat = $"{formatter.StringFromAssignmentPriority(formattingAssignment.Priority)}: [{formattingAssignment.Id}] {formattingAssignment.Title} - {(formattingAssignment.IsCompleted ? "Completed" : "Incomplete")}\nNote:{(formattingAssignment.Note ?? "None" )}";

		Assert.Equal(expectedFormat, formatter.Format(formattingAssignment));
	}

	[Theory]
	[InlineData("1", Priority.VeryLow)]
	[InlineData("vl", Priority.VeryLow)]
	[InlineData("very low", Priority.VeryLow)]
	[InlineData("2", Priority.Low)]
	[InlineData("l", Priority.Low)]
	[InlineData("low", Priority.Low)]
	[InlineData("3", Priority.Medium)]
	[InlineData("m", Priority.Medium)]
	[InlineData("medium", Priority.Medium)]
	[InlineData("4", Priority.High)]
	[InlineData("h", Priority.High)]
	[InlineData("high", Priority.High)]
	[InlineData("5", Priority.ExtraHigh)]
	[InlineData("xh", Priority.ExtraHigh)]
	[InlineData("extra high", Priority.ExtraHigh)]
	[InlineData("this should return null", null)]
	public void AssignmentPriorityFromString_ShouldReturnExpectedOutput(string formatinput, Priority? expectedoutput)
	{
		AssignmentFormatter formatter = new();

		Assert.Equal(expectedoutput, formatter.AssignmentPriorityFromString(formatinput));
	}

	[Theory]
	[InlineData(Priority.VeryLow, "Very Low")]
	[InlineData(Priority.Low, "Low")]
	[InlineData(Priority.Medium, "Medium")]
	[InlineData(Priority.High, "High")]
	[InlineData(Priority.ExtraHigh, "Extra High")]
	[InlineData((Priority)300, "Unknown")]
	public void StringFromAssignmentPriority_ShouldReturnExpectedOutput(Priority inputpriority, string expectedoutput)
	{
		AssignmentFormatter formatter = new();

		Assert.Equal(expectedoutput, formatter.StringFromAssignmentPriority(inputpriority));
	}
}
