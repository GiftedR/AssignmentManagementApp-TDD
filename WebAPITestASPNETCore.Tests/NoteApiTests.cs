using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebAPITestASPNETCore.Models;

namespace WebAPITestASPNETCore.Tests;

public class NoteApiTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public NoteApiTests(WebApplicationFactory<Program> factory)
	{
		_client = factory.CreateClient();
	}

	[Fact]
	public async Task Can_Create_And_Get_Note()
	{
		string noteContent = "Testing Note (:";

		StringContent noteJson = new(
			JsonSerializer.Serialize(new {Content = noteContent}),
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Notes", noteJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage getResponse = await _client.GetAsync("/api/Notes");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Note>? notes = JsonSerializer.Deserialize<List<Note>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(notes);
		Assert.Contains(notes, n => n.Content == noteContent);
	}
}
