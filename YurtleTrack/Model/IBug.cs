using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	interface IBug
	{
		string ID { get; set; }
		string Description { get; set; }
		string Summary { get; set; }
		bool IsResolved { get; set; }
	}
}
