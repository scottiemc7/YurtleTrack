using System;
using System.Collections.Generic;
using System.Text;

namespace YurtleTrack.Model
{
	interface IProject
	{
		string ID { get; set; }
		string Name { get; set; }
		string Description { get; set; }
	}
}
