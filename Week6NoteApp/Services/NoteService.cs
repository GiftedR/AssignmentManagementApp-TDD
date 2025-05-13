using System;
using System.Collections.Generic;
using System.Linq;

public class NoteService
{
	private INoteRepository _repo;
	private ILogger _logger;

	public NoteService(INoteRepository repo, ILogger logger)
	{
		_repo = repo;
		_logger = logger;
	}

	public void AddNote(string text)
	{
		Note note = new(){ Text = text, CreatedAt = DateTime.Now };
		_repo.Add(note);
		_logger.Log($"[LOG] Note added: {text}");
	}

	public List<Note> GetAllNotes()
	{
		return _repo.GetAll();
	}

	public string FormatNotesForPrint()
	{
		return string.Join("\n", _repo.GetAll().Select(n => $"{n.CreatedAt}: {n.Text}"));
	}
}