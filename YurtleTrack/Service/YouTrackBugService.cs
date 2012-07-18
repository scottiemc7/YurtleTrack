using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace YurtleTrack.Service
{
	class YouTrackBugService : IBugService
	{
		private readonly string YOUTRACKURL;
		private readonly string LOGINURL;
		private readonly string PROJECTLISTURL;
		private readonly string BUGLISTURL;
		private readonly string COUNTURL;
		private readonly string COUNTBYPROJECTURL;
		private readonly string BUGLISTWITHFILTERURL;

		private readonly IHttpWebRequestFactory _httpFactory;
		private readonly string _userName;
		private readonly string _password;
		public YouTrackBugService(IHttpWebRequestFactory httpFactory, string youtrackURL, string userName, string password)
		{
			_httpFactory = httpFactory;

			YOUTRACKURL = youtrackURL;
			LOGINURL = YOUTRACKURL + "/rest/user/login";
			PROJECTLISTURL = YOUTRACKURL + "/rest/project/all";
			BUGLISTURL = YOUTRACKURL + "/rest/issue/byproject/{0}?after={1}&max={2}";
			COUNTURL = YOUTRACKURL + "/rest/issue/count";
			COUNTBYPROJECTURL = YOUTRACKURL + "/rest/issue/count?filter=project:{{{0}}}";
			BUGLISTWITHFILTERURL = YOUTRACKURL + "/rest/issue/byproject/{0}?after={1}&max={2}&filter={3}:{4}";

			_userName = userName;
			_password = password;
		}

		private string _authCookie;
		private string AuthCookie
		{
			get
			{
				if (string.IsNullOrEmpty(_authCookie))
					_authCookie = GetAuthCookie();

				return _authCookie;
			}
		}

		private string GetAuthCookie()
		{
			//Set up request, including username and password in form data
			IHttpWebRequestProxy req = _httpFactory.Create(LOGINURL);
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";
			using (Stream reqStream = req.GetRequestStream())
			using (StreamWriter strWriter = new StreamWriter(reqStream))
			{
				strWriter.Write(String.Format("login={0}&password={1}", _userName, _password));
			}//end using

			//Grab response
			using (IHttpWebResponseProxy resp = req.GetResponse())
			{
				return resp.Headers["Set-Cookie"];
			}//end using
		}

		private string GETResponseFrom(string url)
		{
			IHttpWebRequestProxy req = _httpFactory.Create(url);
			req.Method = "GET";
			req.Headers["Cookie"] = AuthCookie;

			using (IHttpWebResponseProxy resp = req.GetResponse())
			{
				using (Stream s = resp.GetResponseStream())
				using (StreamReader sr = new StreamReader(s))
				{
					return sr.ReadToEnd();
				}//end using
			}//end using
		}

		public List<IProject> GetProjects()
		{
			List<IProject> projects = new List<IProject>();
			string projectsAsXML = GETResponseFrom(PROJECTLISTURL);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(projectsAsXML);

			XmlNodeList projectNodes = doc.SelectNodes("/projects/project");
			foreach (XmlNode projectNode in projectNodes)
			{
				IProject proj = new YouTrackProject() { ID = projectNode.Attributes["shortName"].Value, Name = projectNode.Attributes["name"].Value };
				if (projectNode.Attributes["description"] != null)
					proj.Description = projectNode.Attributes["description"].Value;
				projects.Add(proj);
			}//end foreach

			return projects;
		}

		public List<IBug> GetBugsForProject(IProject project, int page, int pageSize)
		{
			return GetFilteredBugsForProject(project, page, pageSize, null, null);
		}

		//Wildcard char doesn't seem to work in version 3
		//http://youtrack.jetbrains.com/issue/JT-13494#
		public List<IBug> GetFilteredBugsForProject(IProject project, int page, int pageSize, string filterBy, string filterValue)
		{
			string url;
			if(String.IsNullOrEmpty(filterBy) || String.IsNullOrEmpty(filterValue))
				url = String.Format(BUGLISTURL, project.ID, pageSize * page, pageSize);
			else
				url = String.Format(BUGLISTWITHFILTERURL, project.ID, pageSize * page, pageSize, filterBy, filterValue);

			List<IBug> bugs = new List<IBug>();
			string bugsAsXML = GETResponseFrom(url);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(bugsAsXML);

			XmlNodeList bugNodes = doc.SelectNodes("/issues/issue");
			foreach (XmlNode bugNode in bugNodes)
			{
				IBug bug = new YouTrackBug() { ID = bugNode.Attributes["id"].Value, IsResolved = false };

				//Get description node
				XmlNode desNode = bugNode.SelectSingleNode("field[@name='description']/value");
				if (desNode != null)
					bug.Description = desNode.InnerText;

				//Get summary node
				XmlNode sumNode = bugNode.SelectSingleNode("field[@name='summary']/value");
				if (sumNode != null)
					bug.Summary = sumNode.InnerText;

				//Find out if resolved
				XmlNode resolvedNode = bugNode.SelectSingleNode("field[@name='resolved']/value");
				if (resolvedNode != null)
					bug.IsResolved = true;
				else
					bug.IsResolved = false;

				bugs.Add(bug);
			}//end foreach

			return bugs;
		}

		//http://youtrack.jetbrains.com/issue/JT-11650
		public int GetTotalBugCount()
		{
			string countAsXML = GETResponseFrom(COUNTURL);
			return ParseCountFromResponse(countAsXML);
		}

		public int GetBugCountForProject(IProject project)
		{
			string countAsXML = GETResponseFrom(String.Format(COUNTBYPROJECTURL, project.ID));
			return ParseCountFromResponse(countAsXML);
		}

		private static int ParseCountFromResponse(string response)
		{
			string matched = "-1";
			try
			{
				matched = Regex.Match(response, "^callback\\(\"(?<num>-*\\d+)\"\\)$").Groups["num"].Value;
			}
			catch { }

			return Int32.Parse(matched);
		}


		
	}
}
