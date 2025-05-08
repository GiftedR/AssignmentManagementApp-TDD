using System;

namespace WebAPITestASPNETCore.Models;

public class Note
{
	public int Id { get; set; }
	public string Content { get; set; } = default!;
}
