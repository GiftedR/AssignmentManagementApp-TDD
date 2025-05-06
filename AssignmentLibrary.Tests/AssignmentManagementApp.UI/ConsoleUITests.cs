using System;
using AssignmentLibrary.Interfaces;
using AssignmentManagementApp.UI;
using Moq;

namespace AssignmentLibrary.Tests;

public class ConsoleUITests
{
	[Fact]
	public void AddAssignment_ShouldCallAddAssignmentInAssignmentService()
	{
		Mock<IAssignmentService> moqAssignment = new();
		Assignment assignment = new("Lab 4", "Do thing with thing (:");
		ConsoleUI consoleUI = new(moqAssignment.Object);

		moqAssignment.Object.AddAssignment(assignment);

		moqAssignment.Verify(service => service.AddAssignment(assignment), Times.Once);
	}

	[Fact]
	public void DeleteAssignment_ShouldReturnTrueIfExists()
	{
		Mock<IAssignmentService> moqAssignment = new();
		string titleToDelete = "Cool Task (:";
		
		moqAssignment.Setup(service => service.DeleteAssignment(titleToDelete)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignment.Object);

		bool result = moqAssignment.Object.DeleteAssignment(titleToDelete);
		
		Assert.True(result);
		moqAssignment.Verify(service => service.DeleteAssignment(titleToDelete), Times.Once);
	}

	[Fact]
	public void FindByTitle_ShouldReturnFoundAssignmentWithSameTitle()
	{
		Mock<IAssignmentService> moqAssignment = new();
		string titleToSearch = "Cool Title (:";
		Assignment assignmentToFind = new(titleToSearch, "Cool Description");
		
		moqAssignment.Setup(service => service.FindAssignmentByTitle(titleToSearch)).Returns(assignmentToFind);
		
		ConsoleUI consoleUI = new(moqAssignment.Object);

		Assignment? foundAssignment = moqAssignment.Object.FindAssignmentByTitle(titleToSearch);

		Assert.NotNull(foundAssignment);
		Assert.Equal(titleToSearch, foundAssignment.Title);
		moqAssignment.Verify(service => service.FindAssignmentByTitle(titleToSearch), Times.Once);
	}

	[Fact]
	public void UpdateAssigment_ShouldReturnTrue_WhenAssignmentIsUpdated()
	{
		Mock<IAssignmentService> moqAssignment = new();
		string oldTitle = "Old Title";
		string newTitle = "New Title";
		Assignment assignment = new(oldTitle, "Description");
		moqAssignment.Setup(service => service.UpdateAssigment(oldTitle, newTitle, assignment.Description)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignment.Object);

		bool result = moqAssignment.Object.UpdateAssigment(oldTitle, newTitle, assignment.Description);

		Assert.True(result);
		moqAssignment.Verify(service => service.UpdateAssigment(oldTitle, newTitle, assignment.Description), Times.Once);
	}

	[Fact]
	public void DeleteAssignment_ShouldReturnTrue_WhenAssignmentIsDeleted()
	{
		Mock<IAssignmentService> moqAssignment = new();
		string assignmentTitle = "Delete Me";
		Assignment assignment = new(assignmentTitle, "Description");
		moqAssignment.Setup(service => service.DeleteAssignment(assignmentTitle)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignment.Object);

		bool result = moqAssignment.Object.DeleteAssignment(assignmentTitle);

		Assert.True(result);
		moqAssignment.Verify(service => service.DeleteAssignment(assignmentTitle), Times.Once);
	
	}
}
