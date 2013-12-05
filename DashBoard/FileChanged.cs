using System;
using ApprovalUtilities.Utilities;

namespace DashBoard
{
	public class FileChanged
	{
		public DateTime Time { get; set; }
		public string Path { get; set; }

		public FileChanged(DateTime time, string path)
		{
			Time = time;
			Path = path;
		}
		public override string ToString()
		{
			return this.WritePropertiesToString();
		}
	}
}