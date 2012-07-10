using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class Bug :IBug
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
	}
}
