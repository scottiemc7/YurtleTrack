using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class YouTrackCommand : ICommand
	{
		public YouTrackCommand()
		{
			DisableNotifications = false;
			Command = null;
		}

		public string Command
		{
			get;
			set;
		}

		public bool DisableNotifications
		{
			get;
			set;
		}
	}
}
