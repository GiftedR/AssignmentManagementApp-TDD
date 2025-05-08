using System;
using WebAPITestASPNETCore.Interfaces;
using WebAPITestASPNETCore.Models;

namespace WebAPITestASPNETCore.Services;

public class NoteService : INoteService
{
	private readonly List<Note> _notes = new();

	public NoteService()
	{
		if (_notes.Count() < 1)
		{
			_notes = new List<Note>{
				new Note
				{
					Id = 1,
					Content = "Add Seed Data (:"
				},
				new Note
				{
					Id = 2,
					Content = "According to all known laws (:"
				},
				new Note
				{
					Id = 3,
					Content = "Of Avation, there is no way (:"
				},
				new Note
				{
					Id = 4,
					Content = "A Bee Should Be Able To Fly (:"
				},
				new Note
				{
					Id = 5,
					Content = "Its Wings Are Too Small To (:"
				},
				new Note
				{
					Id = 6,
					Content = "Get Its Fat Little Body Off The Ground (:"
				}
			};
		}
	}

	public Note Add(Note newnote)
	{
		newnote.Id = _notes.Count() + 1; // Adding 1 so list starts at 1 Id
		_notes.Add(newnote);
		return newnote;
	}

	public void Delete(int id)
	{
		Note? deleteNote = GetById(id);
		if (deleteNote != null) _notes.Remove(deleteNote);
	}

	public IEnumerable<Note> GetAll() => _notes;

	public Note? GetById(int id) => _notes.FirstOrDefault(note => note.Id == id);

	public bool Update(Note updatenote)
	{
		Note? updatingNote = GetById(updatenote.Id);
		if (updatingNote != null)
		{
			updatingNote.Content = updatenote.Content;
			return true;
		}
		return false;
	}
}
