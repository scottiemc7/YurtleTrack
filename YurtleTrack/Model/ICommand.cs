using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	interface ICommand
	{
		string Command { get; set; }
		bool DisableNotifications { get; set; }
	}
}
