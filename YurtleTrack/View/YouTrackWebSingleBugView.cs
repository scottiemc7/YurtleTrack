using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;
using System.Diagnostics;
using System.Windows.Forms;
using Ninject;

namespace YurtleTrack.View
{
	class YouTrackWebSingleBugView : ISingleBugView
	{
		private readonly ISettingsOriginator _settings;
		public YouTrackWebSingleBugView([Named("Parameters")]ISettingsOriginator settings)
		{
			_settings = settings;
		}

		private IBug _bug;
		public IBug Bug
		{
			get
			{
				return _bug;
			}
			set
			{
				if (value == null)
					return;

				_bug = value;

				//Show bug in browser
				ISetting url = _settings.Get(YurtleTrackPlugin.URLOPTIONNAME);
				if (url == null || String.IsNullOrEmpty(url.Value))
					MessageBox.Show("Unable to display. No value has been set for YouTrackURL.");
				else
					Process.Start(String.Format("{0}/issue/{1}", url.Value, Bug.ID));
			}
		}
	}
}
