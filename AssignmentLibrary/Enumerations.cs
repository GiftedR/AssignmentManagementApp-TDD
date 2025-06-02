namespace AssignmentLibrary;
/// <summary>
/// Static class for the Emumerations.
/// </summary>
public static class Enumerations
{
	/// <summary>
	/// Handles the priority for assignments.
	/// </summary>
	public enum Priority
	{
		/// <summary>
		/// This item should be one of the last ones to be done.
		/// </summary>
		VeryLow,
		/// <summary>
		/// This item can wait.
		/// </summary>
		Low,
		/// <summary>
		/// This item can be done at any time. This is the normal priority for assignments.
		/// </summary>
		Medium,
		/// <summary>
		/// This item should be done sooner rather than later.
		/// </summary>
		High,
		/// <summary>
		/// This item should be done now.
		/// </summary>
		ExtraHigh
	}
}