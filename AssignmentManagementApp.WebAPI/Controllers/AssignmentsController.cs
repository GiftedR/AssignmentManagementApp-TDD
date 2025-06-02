using AssignmentLibrary;
using AssignmentLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentManagementApp.WebAPI.Controllers
{
	/// <summary>
	/// The controller for the <see cref="AssignmentService"/> to expose its methods to the internet.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentsController : ControllerBase
	{
		/// <summary>
		/// Injected property storage for <see cref="AssignmentService"/>
		/// </summary>
		public readonly IAssignmentService _assignmentService;

		/// <summary>
		/// Creates a new controller.
		/// </summary>
		/// <param name="assignmentservice">The <see cref="AssignmentService"/> injection.</param>
		public AssignmentsController(IAssignmentService assignmentservice)
		{
			_assignmentService = assignmentservice;
		}

		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.ListAll"/>
		/// </summary>
		/// <returns>Http response for getting all assignments.</returns>
		[HttpGet]
		public IActionResult GetAll() => Ok(_assignmentService.ListAll());

		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.ListIncomplete"/>
		/// </summary>
		/// <returns>Http response for getting only incompleted assignments.</returns>
		[HttpGet("incomplete")]
		public IActionResult GetIncomplete() => Ok(_assignmentService.ListIncomplete());
		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.FindAssignmentByTitle"/>
		/// </summary>
		/// <returns>Http response for getting a found assignment or none.</returns>
		[HttpGet("one/{title}")]
		public IActionResult GetOneByTitle(string title) => OkOrBad(_assignmentService.FindAssignmentByTitle(title));
		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.UpdateAssigment"/>
		/// </summary>
		/// <returns>Http response for wheither it succeeded or not.</returns>
		[HttpPut("{title}")]
		public IActionResult UpdateOneByTitle(string title, Assignment newassignment) => OkOrBad(_assignmentService.UpdateAssigment(title, newassignment.Title, newassignment.Description), false);
		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.AddAssignment"/>
		/// </summary>
		/// <returns>Http response for wheither it succeeded or not.</returns>
		[HttpPost]
		public IActionResult Create(Assignment assignment) => OkOrBad(_assignmentService.AddAssignment(assignment));
		/// <summary>
		/// Exposed Get method for <see cref="IAssignmentService.DeleteAssignment"/>
		/// </summary>
		/// <returns>Http response for wheither it succeeded or not.</returns>
		[HttpDelete("{title}")]
		public IActionResult Delete(string title) => OkOrBad(_assignmentService.DeleteAssignment(title));

		/// <summary>
		/// Shortcut for converting the results into their http counterparts.
		/// </summary>
		/// <param name="assignment">The object to be passed in an HttpRequest</param>
		/// <param name="usenotfoundinstead">Uses NotFound over BadRequest when failing.</param>
		/// <returns></returns>
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
