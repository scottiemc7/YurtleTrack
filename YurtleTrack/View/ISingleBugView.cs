using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;

namespace YurtleTrack.View
{
	interface ISingleBugView
	{
		IBug Bug { get; set; }
	}
}
