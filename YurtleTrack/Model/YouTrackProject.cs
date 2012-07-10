using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class YouTrackProject : IProject
	{
		public string ID
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}


		public string Description
		{
			get;
			set;
		}
	}
}
