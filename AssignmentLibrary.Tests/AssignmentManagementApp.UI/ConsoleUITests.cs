using System;
using AssignmentLibrary.Interfaces;
using AssignmentManagementApp.UI;
using Moq;
using static AssignmentLibrary.Enumerations;

namespace AssignmentLibrary.Tests;

public class ConsoleUITests
{
	[Fact]
	public void AddAssignment_ShouldCallAddAssignmentInAssignmentService()
	{
		Mock<IAssignmentService> moqAssignmentService = new();
		Assignment assignment = new("Lab 4", "Do thing with thing (:");
		ConsoleUI consoleUI = new(moqAssignmentService.Object);

		moqAssignmentService.Object.AddAssignment(assignment);

		moqAssignmentService.Verify(service => service.AddAssignment(assignment), Times.Once);
	}

	[Fact]
	public void DeleteAssignment_ShouldReturnTrueIfExists()
	{
		Mock<IAssignmentService> moqAssignmentService = new();
		string titleToDelete = "Cool Task (:";
		
		moqAssignmentService.Setup(service => service.DeleteAssignment(titleToDelete)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignmentService.Object);

		bool result = moqAssignmentService.Object.DeleteAssignment(titleToDelete);
		
		Assert.True(result);
		moqAssignmentService.Verify(service => service.DeleteAssignment(titleToDelete), Times.Once);
	}

	[Fact]
	public void FindByTitle_ShouldReturnFoundAssignmentWithSameTitle()
	{
		Mock<IAssignmentService> moqAssignmentService = new();
		string titleToSearch = "Cool Title (:";
		Assignment assignmentToFind = new(titleToSearch, "Cool Description");
		
		moqAssignmentService.Setup(service => service.FindAssignmentByTitle(titleToSearch)).Returns(assignmentToFind);
		
		ConsoleUI consoleUI = new(moqAssignmentService.Object);

		Assignment? foundAssignment = moqAssignmentService.Object.FindAssignmentByTitle(titleToSearch);

		Assert.NotNull(foundAssignment);
		Assert.Equal(titleToSearch, foundAssignment.Title);
		moqAssignmentService.Verify(service => service.FindAssignmentByTitle(titleToSearch), Times.Once);
	}

	[Fact]
	public void UpdateAssigment_ShouldReturnTrue_WhenAssignmentIsUpdated()
	{
		Mock<IAssignmentService> moqAssignmentService = new();
		string oldTitle = "Old Title";
		string newTitle = "New Title";
		Assignment assignment = new(oldTitle, "Description");
		moqAssignmentService.Setup(service => service.UpdateAssigment(oldTitle, newTitle, assignment.Description, null)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignmentService.Object);

		bool result = moqAssignmentService.Object.UpdateAssigment(oldTitle, newTitle, assignment.Description);

		Assert.True(result);
		moqAssignmentService.Verify(service => service.UpdateAssigment(oldTitle, newTitle, assignment.Description, null), Times.Once);
	}

	[Fact]
	public void DeleteAssignment_ShouldReturnTrue_WhenAssignmentIsDeleted()
	{
		Mock<IAssignmentService> moqAssignmentService = new();
		string assignmentTitle = "Delete Me";
		Assignment assignment = new(assignmentTitle, "Description");
		moqAssignmentService.Setup(service => service.DeleteAssignment(assignmentTitle)).Returns(true);

		ConsoleUI consoleUI = new(moqAssignmentService.Object);

		bool result = moqAssignmentService.Object.DeleteAssignment(assignmentTitle);

		Assert.True(result);
		moqAssignmentService.Verify(service => service.DeleteAssignment(assignmentTitle), Times.Once);
	}
#region Run Tests
	[Fact]
	public void Run_Input0_ShouldExit()
	{
		using (StringReader reader = new("0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
		}
	}
	[Fact]
	public void Run_Input1_ShouldCallAddAssignment()
	{
		const string title = "Called Title Here";
		const string description = "Called Description Here";
		Assignment assignment = new(title, description);
		using (StringReader reader = new($"1\n{title}\n{description}\n\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.AddAssignment(It.IsAny<Assignment>()))
				.Returns(true);
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.AddAssignment(It.IsAny<Assignment>()), Times.Once);
		}
	}
	[Fact]
	public void Run_Input1_WithInvalidAssignment_ShouldShowMessage()
	{
		const string title = "Called Title Here";
		const string description = "Called Description Here";
		Assignment assignment = new(title, description);
		using (StringReader reader = new($"1\n{title}\n{description}\n\n0"))
		using (StringWriter writer = new())
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.AddAssignment(It.IsAny<Assignment>()))
				.Returns(false);
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Assignment is Duplicate.";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
			
		}
		ResetOutput();
	}
	[Fact]
	public void Run_Input2_ShouldCallListAllAssignments()
	{
		using (StringReader reader = new($"2\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.ListAll())
				.Returns(new List<Assignment>());
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.ListAll(), Times.Once);
		}
	}
	[Fact]
	public void Run_Input2_WithData_ShouldListAllAssignments()
	{
		using (StringReader reader = new($"2\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.ListAll())
				.Returns(new List<Assignment>(){
					new Assignment("Cool", "Beans")
				});
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.ListAll(), Times.Once);
		}
	}
	[Fact]
	public void Run_Input3_ShouldCallListIncompleteAssignments()
	{
		using (StringReader reader = new($"3\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.ListIncomplete())
				.Returns(new List<Assignment>());
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.ListIncomplete(), Times.Once);
		}
	}
	[Fact]
	public void Run_Input3_WithData_ShouldListAllIncompleteAssignments()
	{
		using (StringReader reader = new($"3\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.ListIncomplete())
				.Returns(new List<Assignment>(){
					new Assignment("Cool", "Beans")
				});
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.ListIncomplete(), Times.Once);
		}
	}
	[Fact]
	public void Run_Input4_ShouldCallMarkAssignmentComplete()
	{
		using (StringReader reader = new($"4\nTest\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.MarkAssignmentComplete(It.IsAny<string>()))
				.Returns(true);
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.MarkAssignmentComplete(It.IsAny<string>()), Times.Once);
		}
	}
	[Fact]
	public void Run_Input4_WithBadData_ShouldSayAssignmentNotFound()
	{
		using (StringWriter writer = new())
		using (StringReader reader = new($"4\nBad data\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.MarkAssignmentComplete(It.IsAny<string>())
				).Returns(false);
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Not Found";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}
	[Fact]
	public void Run_Input5_ShouldCallSearchAssignmentByTitle()
	{
		using (StringReader reader = new($"5\nTest\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.FindAssignmentByTitle(It.IsAny<string>()))
				.Returns(new Assignment("True", "False"));
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.FindAssignmentByTitle(It.IsAny<string>()), Times.Once);
		}
	}
	[Fact]
	public void Run_Input5_WithBadData_ShouldListAssignmentNotFound()
	{
		using (StringWriter writer = new())
		using (StringReader reader = new($"5\nBad data\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.FindAssignmentByTitle(It.IsAny<string>())
				).Verifiable();
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Not Found";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}
	[Fact]
	public void Run_Input6_ShouldCallUpdateAssignment()
	{
		using (StringReader reader = new($"6\nGood\nBad\nUgly\n\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.UpdateAssigment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Priority?>()))
				.Returns(true);
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.UpdateAssigment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Priority?>()), Times.Once);
		}
	}
	[Fact]
	public void Run_Input6_WithBadData_ShouldSayUpdateFailed()
	{
		using (StringWriter writer = new())
		using (StringReader reader = new($"6\nBad data\nBad data\nBad data\n\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.UpdateAssigment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Priority?>()))
				.Returns(false);
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Update Failed";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}
	[Fact]
	public void Run_Input7_ShouldCallDeleteAssignment()
	{
		using (StringReader reader = new($"7\nWeird\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.DeleteAssignment(It.IsAny<string>()))
				.Returns(true);
			
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);

			console.Run();

			Assert.False(console.isRunning);
			moqAssignmentService.Verify(s => s
				.DeleteAssignment(It.IsAny<string>()), Times.Once);
		}
	}
	[Fact]
	public void Run_Input7_WithBadData_ShouldSayAssignmentNotFound()
	{
		using (StringWriter writer = new())
		using (StringReader reader = new($"7\nBad data\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.DeleteAssignment(It.IsAny<string>()))
				.Returns(false);
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Not Found";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}
	[Fact]
	public void Run_BadInput_ShouldWriteUnknownOption()
	{
		using (StringReader reader = new($"13\n0"))
		using (StringWriter writer = new())
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Unknown Option. Try Again.";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}

#region Exception Tests
	[Fact]
	public void Run_Input1_WithBadData_ShouldWriteException()
	{
		using (StringWriter writer = new())
		using (StringReader reader = new($"1\nData\nData\n1\nData\nData\n0"))
		{
			Mock<IAssignmentService> moqAssignmentService = new();
			moqAssignmentService.Setup(s => s
				.AddAssignment(It.IsAny<Assignment>())
				).Throws(new Exception("Test Exception"));
			ConsoleUI console = new(moqAssignmentService.Object);

			Console.SetIn(reader);
			Console.SetOut(writer);

			console.Run();

			string outputmessage = writer.ToString();
			string expectedMessage = "Exception";

			Assert.False(console.isRunning);
			Assert.Contains(expectedMessage, outputmessage);
		}
		ResetOutput();
	}
#endregion

	private void ResetOutput(){Console.SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = true});}
#endregion
}
