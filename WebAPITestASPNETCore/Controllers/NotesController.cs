using Microsoft.AspNetCore.Mvc;
using WebAPITestASPNETCore.Interfaces;
using WebAPITestASPNETCore.Models;
using WebAPITestASPNETCore.Services;

namespace WebAPITestASPNETCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly INoteService _noteService;

		public NotesController(INoteService noteservice)
		{
			_noteService = noteservice;
		}

		[HttpGet]
		public IActionResult GetAll() => Ok(_noteService.GetAll());

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			Note? note = _noteService.GetById(id);
			return note == null ? NotFound() : Ok(note);
		}

		[HttpPost]
		public IActionResult Create(Note note)
		{
			Note createNote = _noteService.Add(note);
			return CreatedAtAction(nameof(Get), new {id = createNote.Id}, createNote);
		}
		
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_noteService.Delete(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Note updateNote)
		{
			if (id != updateNote.Id) return BadRequest("Invalid Note To Update.");
			
			if (!_noteService.Update(updateNote))
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
