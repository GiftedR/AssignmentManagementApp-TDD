using System;
using WebAPITestASPNETCore.Models;

namespace WebAPITestASPNETCore.Interfaces;

public interface INoteService
{
	IEnumerable<Note> GetAll();
	Note? GetById(int id);
	Note Add(Note newnote);
	bool Update(Note updatenote);
	void Delete(int id);
}
