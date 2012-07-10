using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	interface ISettingsOriginator
	{
		ISetting Get(string name);
		void Set(ISetting setting);
		ISettingsMemento GetMemento();
		void RestoreFromMemento(ISettingsMemento memento);
	}
}
