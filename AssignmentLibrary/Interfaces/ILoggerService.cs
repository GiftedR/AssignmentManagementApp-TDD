using AssignmentLibrary.Interfaces;

namespace AssignmentLibrary;

public interface ILoggerService : IAppLogger
{
	public List<IAppLogger> GetAllLoggers();
	public bool AddLogger(IAppLogger newlogger);
	public IAppLogger? GetLogger(Type logger);
	public bool DeleteLogger();
}