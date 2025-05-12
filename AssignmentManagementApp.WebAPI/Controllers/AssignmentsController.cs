using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManagementApp.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentsController : ControllerBase
	{
		public readonly IAssignmentService _assignmentService;

		public AssignmentsController(IAssignmentService assignmentservice)
		{
			_assignmentService = assignmentservice;
		}

		[HttpGet]
		public IActionResult GetAll() => Ok(_assignmentService.ListAll());
		[HttpGet("{title}")]
		public IActionResult GetOneByTitle(string title) => OkOrBad(_assignmentService.FindAssignmentByTitle(title));
		[HttpPut("{title}")]
		public IActionResult UpdateOneByTitle(string title, Assignment newassignment) => OkOrBad(_assignmentService.UpdateAssigment(title, newassignment.Title, newassignment.Description), false);
		[HttpPost]
		public IActionResult Create(Assignment assignment) => OkOrBad(_assignmentService.AddAssignment(assignment));
		[HttpDelete("{title}")]
		public IActionResult Delete(string title) => OkOrBad(_assignmentService.DeleteAssignment(title));

		// Simplifies checking for null and returning bad requests
		private IActionResult OkOrBad(dynamic? assignment, bool usenotfoundinstead = true)
		{
			if (assignment == null || (assignment is bool && !assignment))
			{
				if (usenotfoundinstead)
				{
					return NotFound("Assignment Not Found");
				}
				else
				{
					return BadRequest("Invalid Assignment");
				}
			}
			return Ok(assignment);
		}
	}
}
