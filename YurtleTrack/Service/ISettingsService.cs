using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;

namespace YurtleTrack.Service
{
	interface ISettingsService
	{
		string GetAllSettingsAsXML();
		void Set(ISetting setting);
	}
}
