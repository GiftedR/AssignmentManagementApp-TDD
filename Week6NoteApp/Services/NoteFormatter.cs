using System;
using System.Collections.Generic;
using System.Linq;

namespace Week6NoteApp.Services;

public class NoteFormatter : INoteFormatter
{
	public string Format(List<Note> notes)
	{
		return string.Join("\n", notes.Select(nte => $"{nte.CreatedAt}: {nte.Text}"));
	}
}
