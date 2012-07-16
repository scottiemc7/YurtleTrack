using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class YouTrackBug :IBug
	{
		public string ID
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Summary
		{
			get;
			set;
		}

		public bool IsResolved
		{
			get;
			set;
		}

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			YouTrackBug p = obj as YouTrackBug;
			if ((System.Object)p == null)
				return false;

			// Return true if the fields match:
			return (ID == p.ID);
		}
	}
}
