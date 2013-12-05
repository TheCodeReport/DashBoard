using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApprovalUtilities.Utilities;

namespace DashBoard
{
	public class Churn
	{
		public static void WriteToFile(IEnumerable<FileChurn> churn, string file)
		{
			var text = churn.JoinStringsWith(c => c.Path + "," + c.ChurnCount, "\r\n");
			File.WriteAllText(file, text);
		}

		public static IEnumerable<FileChurn> GetChurnFor(IEnumerable<FileChanged> changed)
		{
			var distinctForDay = changed.Distinct(new FileChangedByDay());
			Console.WriteLine(
				distinctForDay.Where(c => c.Path == "/trunk/SmallBasicFun/SmallBasicFun/Tortoise.cs")
				              .Select(c => c.Time.ToShortDateString())
				              .ToReadableString());
			var churn = distinctForDay.GroupBy(c => c.Path).Select(g => new FileChurn(g.Key, g.Count()));
			return churn.OrderByDescending(c => c.ChurnCount).ToArray();
		}
	}
}