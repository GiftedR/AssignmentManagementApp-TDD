using System.Collections.Generic;

public interface INoteRepository
{
    void Add(Note note);
    List<Note> GetAll();
}