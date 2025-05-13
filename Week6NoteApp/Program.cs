using System;
using Week6NoteApp.Services;

namespace Week6NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        NoteController controller = new(
            new NoteService(
                new InMemoryNoteRepository(),
                new ConsoleLogger()
            ),
            new NoteFormatter()
        );
    }
}
