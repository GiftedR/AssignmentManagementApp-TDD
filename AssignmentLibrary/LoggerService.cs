using AssignmentLibrary.Interfaces;

namespace AssignmentLibrary;

public class LoggerService : ILoggerService
{
    protected List<IAppLogger> _loggers = [];

    public LoggerService(params IAppLogger[] loggers)
    {
        foreach (IAppLogger ial in loggers)
        {
            AddLogger(ial);
        }
    }

    public bool AddLogger(IAppLogger newlogger)
    {
        throw new NotImplementedException();
    }

    public bool DeleteLogger()
    {
        throw new NotImplementedException();
    }

    public List<IAppLogger> GetAllLoggers()
    {
        throw new NotImplementedException();
    }

    public IAppLogger? GetLogger(Type logger)
    {
        throw new NotImplementedException();
    }

    public void Log(string message)
    {
        throw new NotImplementedException();
    }
}