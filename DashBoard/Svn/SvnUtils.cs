using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ApprovalUtilities.Utilities;
using SharpSvn;
using SharpSvn.Implementation;
using SharpSvn.Security;

namespace DashBoard.Svn
{
	public static class SvnUtils
	{
		public static FileChanged[] GetChangeSetFromSvn(DateTime startDateTime, DateTime endDateTime, SvnConfig server)
		{
			SvnRevisionRange range = new SvnRevisionRange(new SvnRevision(startDateTime), new SvnRevision(endDateTime));
			Collection<SvnLogEventArgs> logitems;

			var uri = new Uri(server.Url);
			FileChanged[] changed;
			using (SvnClient client = new SvnClient())
			{
				client.Authentication.Clear(); // Disable all use of the authentication area
				client.Authentication.UserNamePasswordHandlers +=
					delegate(object sender, SvnUserNamePasswordEventArgs e)
						{
							e.UserName = server.UserName;
							e.Password = server.Password;
							e.Save = true;
						};
				client.Authentication.SslServerTrustHandlers += delegate(object sender, SvnSslServerTrustEventArgs e)
					{
						e.AcceptedFailures = e.Failures;
						e.Save = true; // Save acceptance to authentication store
					};
				client.GetLog(uri, new SvnLogArgs(range), out logitems);
				foreach (var svnLogEventArgse in logitems)
				{
					Console.WriteLine((string) WriteString(svnLogEventArgse));
				}
				changed = logitems.SelectMany<SvnLogEventArgs, FileChanged>(l => GetChangedFiles(l)).ToArray();
			}
			return changed;
		}
		public static string WriteString(SvnLogEventArgs log)
		{
			return "Date={0}\nPaths={1}".FormatWith(log.Time, WriteString(log.ChangedPaths));
		}

		public static string WriteString(SvnChangeItemCollection paths)
		{
			return StringUtils.Write(paths, p => p.Path);
		}
		public static IEnumerable<FileChanged> GetChangedFiles(SvnLogEventArgs logEvent)
		{
			foreach (var p in logEvent.ChangedPaths)
			{
				yield return new FileChanged(logEvent.Time, p.Path);
			}
		}

		public static IEnumerable<FileChanged> GetChangeSetFromSvn(string startDateTime, string endDateTime, SvnConfig server)
		{
			return GetChangeSetFromSvn(DateTime.Parse(startDateTime), DateTime.Parse(endDateTime), server);
		}
	}
}