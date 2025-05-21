namespace AssignmentLibrary;

public class Enumerations
{
	public enum Priority
	{
		VeryLow,
		Low,
		Medium,
		High,
		ExtraHigh
	}

	public static Priority? PriorityFromString(string prioritymessage) => prioritymessage.ToLower() switch
	{
		"1" or "vl" or "very low" => Priority.VeryLow,
		"2" or "l" or "low" => Priority.Low,
		"3" or "m" or "medium" => Priority.Medium,
		"4" or "h" or "high" => Priority.High,
		"5" or "xh" or "extra high" => Priority.ExtraHigh,
		_ => null
	};
}