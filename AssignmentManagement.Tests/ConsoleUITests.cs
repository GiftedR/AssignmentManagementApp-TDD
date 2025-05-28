
using Xunit;
using Moq;
using AssignmentManagement.Core;
using AssignmentManagement.Console;
using System.Collections.Generic;
using System.IO;
using AssignmentManagement.UI;

namespace AssignmentManagement.Tests
{
    public class ConsoleUITests
    {
        [Fact]
        public void AddAssignment_Should_Call_Service_Add()
        {
            var mock = new Mock<IAssignmentService>();
            var ui = new ConsoleUI(mock.Object);

            // Correct input: choose menu option 1, enter title, enter description, then exit
            using (var addAssignmentInput = new StringReader("1\nSample Title\nSample Description\n0\n"))
            {
                System.Console.SetIn(addAssignmentInput);

                ui.Run();
            }

            mock.Verify(s => s.AddAssignment(It.Is<Assignment>(a =>
                a.Title == "Sample Title" &&
                a.Description == "Sample Description"
            )), Times.Once);
        }


        [Fact]
        public void SearchAssignmentByTitle_Should_Display_Assignment()
        {
            var mock = new Mock<IAssignmentService>();
            mock.Setup(s => s.FindAssignmentByTitle(It.IsAny<string>()))
                .Returns(new Assignment("Sample", "Details"));

            var ui = new ConsoleUI(mock.Object);

            using (var searchAssignmentInput = new StringReader("5\nSample\n0"))
            {
                System.Console.SetIn(searchAssignmentInput);

                ui.Run();
            }

            mock.Verify(s => s.FindAssignmentByTitle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteAssignment_Should_Call_Service_Delete()
        {
            var mock = new Mock<IAssignmentService>();
            mock.Setup(s => s.DeleteAssignment(It.IsAny<string>())).Returns(true);

            var ui = new ConsoleUI(mock.Object);

            using (var deleteAssignmentInput = new StringReader("7\nToDelete\n0"))
            {
                System.Console.SetIn(deleteAssignmentInput);

                ui.Run();
            }

            mock.Verify(s => s.DeleteAssignment(It.IsAny<string>()), Times.Once);
        }
    }
}
