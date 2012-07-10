using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YurtleTrack.Model
{
	interface ISettingsMemento
	{
		ReadOnlyCollection<ISetting> Settings { get; }
	}
}
