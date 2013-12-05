using System.Collections.Generic;

namespace DashBoard
{
	public class FileChangedByDay : IEqualityComparer<FileChanged>
	{
		public bool Equals(FileChanged x, FileChanged y)
		{
			return x.Time.ToShortDateString() == y.Time.ToShortDateString() && x.Path == y.Path;
		}

		public int GetHashCode(FileChanged fileChanged)
		{
			return fileChanged.Time.ToShortDateString().GetHashCode();
		}
	}
}