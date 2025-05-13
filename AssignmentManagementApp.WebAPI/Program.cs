using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using AssignmentManagementApp.UI;

public class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		// Add services to the container.
		// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
		builder.Services.AddOpenApi();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
		builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
		builder.Services.AddSingleton<IAssignmentService, AssignmentService>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}