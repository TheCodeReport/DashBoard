using ApprovalUtilities.Utilities;

namespace DashBoard
{
	public class FileChurn
	{
		public int ChurnCount { get; set; }
		public string Path { get; set; }

		public FileChurn(string path, int count)
		{
			Path = path;
			ChurnCount = count;
		}
		public override string ToString()
		{
			return this.WritePropertiesToString();
		}
	}
}