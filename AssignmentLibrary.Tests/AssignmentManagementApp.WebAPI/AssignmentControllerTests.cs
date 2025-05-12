using System.Text;
using System.Text.Json;
using AssignmentLibrary;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AssignmentManagementApp.WebAPI.Tests;

public class AssignmentControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public AssignmentControllerTests(WebApplicationFactory<Program> factory)
	{
		_client = factory.CreateClient();
	}

	[Theory]
	[InlineData("Title", "Set an Object Title")]
	[InlineData("A Cool Title", "String Interpolation in a title")]
	[InlineData("A Title with some data (:", "Make a fancy looking title")]
	[InlineData("Variable Learning", "Advanced variables with actions")]
	[InlineData("Pointers", "C# Pointers with unsafe blocks")]
	[InlineData("Classes", "C# classes and class manipulation")]
	[InlineData("Reference", "Passing objects by reference vs instance")]
	[InlineData("Linq", "Linq Framework in C#")]
	[InlineData("Asp.Net", "Asp.Net Framework")]
	[InlineData("Final Assignment", "The final assignment")]
	public async Task GivenNewAssignment_ShouldReturnFoundAssignment(string title, string description)
	{
		StringContent assignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				title,
				description
			)),
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", assignmentJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		Assert.Contains(assignments, assi => assi.Title == title);
		Assert.Contains(assignments, assi => assi.Description == description);
	}
	[Fact]
	public async Task GivenBadAssignment_ShouldReturnBadRequest()
	{
		StringContent assignmentJson = new(
			"Bad Data!",
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", assignmentJson);
		
		Assert.Equal(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest).StatusCode, createResponse.StatusCode);
	}

	[Fact]
	public async Task GivenMultipleNewAssignments_ShouldReturnAllAddedAssignments()
	{
		List<Assignment> postAssignments = [
			new Assignment("Title 2", "Set an Object Title"),
			new Assignment("A Cool Title 2", "String Interpolation in a title"),
			new Assignment("A Title with some data (: 2", "Make a fancy looking title"),
			new Assignment("Variable Learning 2", "Advanced variables with actions"),
			new Assignment("Pointers 2", "C# Pointers with unsafe blocks"),
			new Assignment("Classes 2", "C# classes and class manipulation"),
			new Assignment("Reference 2", "Passing objects by reference vs instance"),
			new Assignment("Linq 2", "Linq Framework in C#"),
			new Assignment("Asp.Net 2", "Asp.Net Framework"),
			new Assignment("Final Assignment 2", "The final assignment")
		];

		List<StringContent> assignmentJsons = new();
		postAssignments.ForEach(assi => assignmentJsons.Add(
			new(
				JsonSerializer.Serialize(assi), 
				Encoding.UTF8, "application/json"
			)
		));
		assignmentJsons.ForEach(async asj =>
		{
			HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", asj);
			createResponse.EnsureSuccessStatusCode();
		});

		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		List<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		assignments.ForEach(retrievedAssi => {
			Assert.Contains(assignments, assi => assi.Title == retrievedAssi.Title);
			Assert.Contains(assignments, assi => assi.Description == retrievedAssi.Description);
		});
	}

	[Fact]
	public async Task GivenMultipleNewAssignmentsSomeOfWhichAreCompleted_ShouldReturnIncompletedAssignments()
	{
		List<Assignment> postAssignments = [
			new Assignment("Title 3", "Set an Object Title"),
			new Assignment("A Cool Title 3", "String Interpolation in a title"),
			new Assignment("A Title with some data (: 3", "Make a fancy looking title"),
			new Assignment("Variable Learning 3", "Advanced variables with actions"),
			new Assignment("Pointers 3", "C# Pointers with unsafe blocks"),
			new Assignment("Classes 3", "C# classes and class manipulation"),
			new Assignment("Reference 3", "Passing objects by reference vs instance"),
			new Assignment("Linq 3", "Linq Framework in C#"),
			new Assignment("Asp.Net 3", "Asp.Net Framework"),
			new Assignment("Final Assignment 3", "The final assignment")
		];

		postAssignments[0].MarkComplete();
		postAssignments[4].MarkComplete();
		postAssignments[2].MarkComplete();
		postAssignments[6].MarkComplete();

		List<StringContent> assignmentJsons = new();
		postAssignments.ForEach(assi => assignmentJsons.Add(
			new(
				JsonSerializer.Serialize(assi), 
				Encoding.UTF8, "application/json"
			)
		));
		assignmentJsons.ForEach(async asj =>
		{
			HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", asj);
			createResponse.EnsureSuccessStatusCode();
		});

		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments/incomplete");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		List<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		assignments.ForEach(retrievedAssi => {
			Assert.False(retrievedAssi.IsCompleted);
		});
	}

	[Fact]
	public async Task GivenOldAssignmentAndNewAssignment_ShouldUpdateAssignment()
	{
		string oldTitle = "Old Title";
		string oldDescription = "Old Description";
		string newTitle = "New Title";
		string newDescription = "New Description";

		StringContent originalAssignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				oldTitle,
				oldDescription
			)),
			Encoding.UTF8, "application/json"
		);
		
		StringContent updateAssignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				newTitle,
				newDescription
			)),
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", originalAssignmentJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage updateResponse = await _client.PutAsync($"/api/Assignments/{oldTitle}", updateAssignmentJson);
		updateResponse.EnsureSuccessStatusCode();
		
		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		Assert.DoesNotContain(assignments, assi => assi.Title == oldTitle);
		Assert.Contains(assignments, assi => assi.Title == newTitle);
		Assert.DoesNotContain(assignments, assi => assi.Description == oldDescription);
		Assert.Contains(assignments, assi => assi.Description == newDescription);
	}

	[Fact]
	public async Task GivenOldAssignmentAndBadNewAssignment_ShouldNotUpdateAndReturnBadRequest()
	{
		string originalTitle = "A Very Cool Title";
		string originalDescription = "A Very Cool Description";

		StringContent originalAssignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				originalTitle,
				originalDescription
			)),
			Encoding.UTF8, "application/json"
		);
		
		StringContent updateAssignmentJson = new(
			"Bad Update Data!",
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", originalAssignmentJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage updateResponse = await _client.PutAsync($"/api/Assignments/{originalTitle}", updateAssignmentJson);
		// updateResponse.EnsureSuccessStatusCode();
		
		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		Assert.Contains(assignments, assi => assi.Title == originalTitle);
		Assert.Contains(assignments, assi => assi.Description == originalDescription);
		Assert.Equal(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest).StatusCode, updateResponse.StatusCode);
	}

	[Fact]
	public async Task GivenAnDeleteAssignment_ShouldDeleteAssignment()
	{
		string deleteTitle = "Delete Me!";
		string deleteDescription = "Delete Me!";

		StringContent assignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				deleteTitle,
				deleteDescription
			)),
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", assignmentJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage deleteResponse = await _client.DeleteAsync($"/api/Assignments/{deleteTitle}");
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		Assert.DoesNotContain(assignments, assi => assi.Title == deleteTitle);
		Assert.DoesNotContain(assignments, assi => assi.Description == deleteDescription);
	}

	[Fact]
	public async Task GivenAnAssignmentAndBadDeleteAssignment_ShouldNotDeleteAssignmentAndReturnNotFound()
	{
		string deleteTitle = "Should not be Deleted (:";
		string deleteDescription = "HIII, I still exist!";

		StringContent originalAssignmentJson = new(
			JsonSerializer.Serialize(new Assignment(
				deleteTitle,
				deleteDescription
			)),
			Encoding.UTF8, "application/json"
		);

		HttpResponseMessage createResponse = await _client.PostAsync("/api/Assignments", originalAssignmentJson);
		createResponse.EnsureSuccessStatusCode();

		HttpResponseMessage deleteResponse = await _client.DeleteAsync("/api/Assignments/BadDataGoesHereThisDoesNotExist");
		// deleteResponse.EnsureSuccessStatusCode();
		
		HttpResponseMessage getResponse = await _client.GetAsync("/api/Assignments");
		getResponse.EnsureSuccessStatusCode();

		string json = await getResponse.Content.ReadAsStringAsync();
		IEnumerable<Assignment>? assignments = JsonSerializer.Deserialize<List<Assignment>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		Assert.NotNull(assignments);
		Assert.Contains(assignments, assi => assi.Title == deleteTitle);
		Assert.Contains(assignments, assi => assi.Description == deleteDescription);
		Assert.Equal(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound).StatusCode, deleteResponse.StatusCode);
	}
}
