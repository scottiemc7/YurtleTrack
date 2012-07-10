using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.View
{
	interface IOptionsView
	{
		string URL { get; set; }
		string UserName { get; set; }
		string Password { get; set; }
	}
}
