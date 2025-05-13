using Xunit;
using System.Collections.Generic;
using Week6NoteApp.Services;

public class NoteServiceTests
{
    [Fact]
    public void AddNote_ShouldIncreaseNoteCount()
    {
        var repo = new InMemoryNoteRepository();
        var logger = new TestLogger();
        var service = new NoteService(repo, logger);

        service.AddNote("Test note");

        var notes = service.GetAllNotes();
        Assert.Single(notes);
        Assert.Equal("Test note", notes[0].Text);
    }

    private class TestLogger : ILogger
    {
        public void Log(string message) { }
    }
}