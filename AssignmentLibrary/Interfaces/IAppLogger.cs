namespace AssignmentLibrary.Interfaces;

/// <summary>
/// Used as the basis for logging and debugging messages.
/// </summary>
public interface IAppLogger
{
    /// <summary>
    /// Logs to a place.
    /// </summary>
    /// <param name="message">The message that gets logged.</param>
    void Log(string message);
}