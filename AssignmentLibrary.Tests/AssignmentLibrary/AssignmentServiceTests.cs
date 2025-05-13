using AssignmentLibrary.Interfaces;
using AssignmentManagementApp.UI;
using Castle.Core.Logging;

namespace AssignmentLibrary.Tests;

public class AssignmentServiceTests
{
	public IAppLogger testLogger = new ConsoleAppLogger();
	public IAssignmentFormatter testFormatter = new AssignmentFormatter();

	[Fact]
	public void ListAll_NoAssignments_ShouldListNoAssignments()
	{
		var service = new AssignmentService(testFormatter, testLogger);

		Assert.Empty(service.ListAll());
	}
	[Fact]
	public void ListAll_ThreeAssignments_ShouldListAllThreeAssignments()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment assignment1 = new("First Assignment", "Variables");
		Assignment assignment2 = new("Second Assignment", "Flow Controls");
		Assignment assignment3 = new("Third Assignment", "Classes");

		assignment2.MarkComplete();

		service.AddAssignment(assignment1);
		service.AddAssignment(assignment2);
		service.AddAssignment(assignment3);

		Assert.Contains(assignment1, service.ListAll());
		Assert.Contains(assignment2, service.ListAll());
		Assert.Contains(assignment3, service.ListAll());
	}
	[Fact]
	public void AddAssignment_ValidInput_ShouldAddAssignment()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment assignment = new("Lab 1", "TDD examples in the real world");

		service.AddAssignment(assignment);

		Assert.Contains(assignment, service.ListAll());
	}
	[Fact]
	public void AddAssignment_DuplicateAssignment_ShouldNotAdd()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment assignment = new("Lab 1", "TDD examples in the real world");
		Assignment duplicateassignment = new("Lab 1", "TDD examples in the real world");

		service.AddAssignment(assignment);

		Assert.Contains(assignment, service.ListAll());
		Assert.Throws<InvalidDataException>(() => service.AddAssignment(duplicateassignment));
	}

	[Fact]
	public void ListIncomplete_ThreeIncompleteAssignments_ShouldListThreeIncompleteAssignments()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment incompleteassignment1 = new("First Assignment", "Variables");
		Assignment incompleteassignment2 = new("Second Assignment", "Flow Controls");
		Assignment incompleteassignment3 = new("Third Assignment", "Classes");

		service.AddAssignment(incompleteassignment1);
		service.AddAssignment(incompleteassignment2);
		service.AddAssignment(incompleteassignment3);

		Assert.Contains(incompleteassignment1, service.ListIncomplete());
		Assert.Contains(incompleteassignment2, service.ListIncomplete());
		Assert.Contains(incompleteassignment3, service.ListIncomplete());
	}
	[Fact]
	public void ListIncomplete_Empty_ShouldListNoAssignments()
	{
		AssignmentService service = new(testFormatter, testLogger);

		Assert.Empty(service.ListIncomplete());
	}
	[Fact]
	public void ListIncomplete_TwoCompleteFourIncomplete_ShouldListFourIncompleteAssignments()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment completeassignment1 = new("Attendance", "Did you show up for the first week?");
		Assignment completeassignment2 = new("Syllabus Quiz", "Did you read the syllabus?");
		Assignment incompleteassignment1 = new("First Assignment", "Variables");
		Assignment incompleteassignment2 = new("Second Assignment", "Flow Controls");
		Assignment incompleteassignment3 = new("Third Assignment", "Classes");
		Assignment incompleteassignment4 = new("Fourth Assignment", "Lambdas");

		completeassignment1.MarkComplete();
		completeassignment2.MarkComplete();

		service.AddAssignment(incompleteassignment1);
		service.AddAssignment(incompleteassignment2);
		service.AddAssignment(incompleteassignment3);
		service.AddAssignment(incompleteassignment4);
		service.AddAssignment(completeassignment1);
		service.AddAssignment(completeassignment2);

		Assert.Contains(incompleteassignment1, service.ListIncomplete());
		Assert.Contains(incompleteassignment2, service.ListIncomplete());
		Assert.Contains(incompleteassignment3, service.ListIncomplete());
		Assert.Contains(incompleteassignment4, service.ListIncomplete());
		Assert.DoesNotContain(completeassignment1, service.ListIncomplete());
		Assert.DoesNotContain(completeassignment2, service.ListIncomplete());
	}
	[Fact]
	public void MarkAssignmentComplete_ShouldMarkAssignmentComplete()
	{
		AssignmentService service = new(testFormatter, testLogger);
		Assignment completeassignment1 = new("Attendance", "Did you show up for the first week?");
		Assignment completeassignment2 = new("Syllabus Quiz", "Did you read the syllabus?");
		Assignment incompleteassignment1 = new("First Assignment", "Variables");
		Assignment incompleteassignment2 = new("Second Assignment", "Flow Controls");
		Assignment incompleteassignment3 = new("Third Assignment", "Classes");
		Assignment incompleteassignment4 = new("Fourth Assignment", "Lambdas");

		service.AddAssignment(incompleteassignment1);
		service.AddAssignment(incompleteassignment2);
		service.AddAssignment(incompleteassignment3);
		service.AddAssignment(incompleteassignment4);
		service.AddAssignment(completeassignment1);
		service.AddAssignment(completeassignment2);
		
		service.MarkAssignmentComplete(completeassignment1.Title);
		service.MarkAssignmentComplete(completeassignment2.Title);
		
		Assert.Contains(incompleteassignment1, service.ListIncomplete());
		Assert.Contains(incompleteassignment2, service.ListIncomplete());
		Assert.Contains(incompleteassignment3, service.ListIncomplete());
		Assert.Contains(incompleteassignment4, service.ListIncomplete());
		Assert.DoesNotContain(completeassignment1, service.ListIncomplete());
		Assert.DoesNotContain(completeassignment2, service.ListIncomplete());
	}

	[Fact]
	public void MarkAssignmentComplete_NonExistantAssignment_ShouldNotMarkComplete()
	{
		AssignmentService service = new(testFormatter, testLogger);
		
		Assert.False(service.MarkAssignmentComplete("No Assignment (:"));
	}
}