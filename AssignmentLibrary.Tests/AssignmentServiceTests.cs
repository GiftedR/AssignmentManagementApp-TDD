namespace AssignmentLibrary.Tests;

public class AssignmentServiceTests
{
	[Fact]
	public void ListAll_NoAssignments_ShouldListNoAssignments()
	{
		var service = new AssignmentService();

		Assert.Empty(service.ListAll());
	}
	[Fact]
	public void ListAll_ThreeAssignments_ShouldListAllThreeAssignments()
	{
		var service = new AssignmentService();
		var assignment1 = new Assignment("First Assignment", "Variables");
		var assignment2 = new Assignment("Second Assignment", "Flow Controls");
		var assignment3 = new Assignment("Third Assignment", "Classes");

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
		var service = new AssignmentService();
		var assignment = new Assignment("Lab 1", "TDD examples in the real world");

		service.AddAssignment(assignment);

		Assert.Contains(assignment, service.ListAll());
	}
	[Fact]
	public void AddAssignment_DuplicateAssignment_ShouldNotAdd()
	{
		var service = new AssignmentService();
		var assignment = new Assignment("Lab 1", "TDD examples in the real world");
		var duplicateassignment = new Assignment("Lab 1", "TDD examples in the real world");

		service.AddAssignment(assignment);
		service.AddAssignment(duplicateassignment);

		Assert.Contains(assignment, service.ListAll());
		Assert.DoesNotContain(duplicateassignment, service.ListAll());
	}

	[Fact]
	public void ListIncomplete_ThreeIncompleteAssignments_ShouldListThreeIncompleteAssignments()
	{
		var service = new AssignmentService();
		var incompleteassignment1 = new Assignment("First Assignment", "Variables");
		var incompleteassignment2 = new Assignment("Second Assignment", "Flow Controls");
		var incompleteassignment3 = new Assignment("Third Assignment", "Classes");

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
		var service = new AssignmentService();

		Assert.Empty(service.ListIncomplete());
	}
	[Fact]
	public void ListIncomplete_TwoCompleteFourIncomplete_ShouldListFourIncompleteAssignments()
	{
		var service = new AssignmentService();
		var completeassignment1 = new Assignment("Attendance", "Did you show up for the first week?");
		var completeassignment2 = new Assignment("Syllabus Quiz", "Did you read the syllabus?");
		var incompleteassignment1 = new Assignment("First Assignment", "Variables");
		var incompleteassignment2 = new Assignment("Second Assignment", "Flow Controls");
		var incompleteassignment3 = new Assignment("Third Assignment", "Classes");
		var incompleteassignment4 = new Assignment("Fourth Assignment", "Lambdas");

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

}