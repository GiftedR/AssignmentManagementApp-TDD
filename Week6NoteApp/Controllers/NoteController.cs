public class NoteController
{
	private readonly NoteService _service;
	private readonly INoteFormatter _formatter;

	public NoteController(NoteService service, INoteFormatter formatter)
	{
		_service = service;
		_formatter = formatter;
	}

	public string ShowNotes()
	{
		return _formatter.Format(_service.GetAllNotes());
	}
}