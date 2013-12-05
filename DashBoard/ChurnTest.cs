using System;
using ApprovalUtilities.Utilities;
using DashBoard.Svn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DashBoard
{
	[TestClass]
	public class ChurnTest
	{
		[TestMethod]
		public void TestSvnChurn()
		{
			var changed = SvnUtils.GetChangeSetFromSvn("2012-01-01", "2014-01-01", new SvnConfig());
			var churn = Churn.GetChurnFor(changed);
			Console.WriteLine(churn.ToReadableString());
			Churn.WriteToFile(churn, @"c:\temp\churn.csv");
		}
	}
}