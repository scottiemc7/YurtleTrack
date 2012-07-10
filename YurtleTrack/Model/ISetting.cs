using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	interface ISetting
	{
		string Name { get; set; }
		string Value { get; set; }
		bool Encrypt { get; set; }
	}
}
