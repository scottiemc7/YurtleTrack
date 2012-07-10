using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class Setting : ISetting
	{
		public string Name
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public bool Encrypt
		{
			get;
			set;
		}
	}
}
