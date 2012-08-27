using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Model
{
	class SettingsOriginatorFactory
	{
		public ISettingsOriginator CreateOriginator()
		{
			return new SettingsOriginator();
		}

		public ISettingsOriginator CreateOriginatorFromMemento(ISettingsMemento memento)
		{
			return new SettingsOriginator(memento);
		}

		private static SettingsOriginatorFactory _instance;
		public static SettingsOriginatorFactory Instance
		{
			get
			{
				if(_instance == null)
					_instance = new SettingsOriginatorFactory();

				return _instance;
			}
			internal set
			{
				_instance = value;
			}
		}
	}
}
