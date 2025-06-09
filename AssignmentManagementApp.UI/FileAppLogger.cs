using System.Text;
using AssignmentLibrary.Interfaces;

namespace AssignmentManagementApp.UI;

public class FileAppLogger : IAppLogger
{
	public void Log(string message)
	{
		Action<FileStream, string> addText = (FileStream fs, string writetext) =>
		{
			byte[] info = new UTF8Encoding(true).GetBytes(writetext);
			fs.Write(info, 0, info.Length);
		};

		if (!File.Exists("log.txt"))
		{
			FileStream createFS = File.Create("Log.txt");
			createFS.Close();
		}

		using (FileStream logFS = File.OpenWrite("Log.txt"))
		{
			addText(logFS, $"[LOG]: {message}");
			logFS.Close();
		}
	}
}