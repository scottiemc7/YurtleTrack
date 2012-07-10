using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Presenter
{
	interface IBugListViewPresenter
	{
		void Initialize();
		void DisplayBugDetails();
		void DisplayProjectDetails();
	}
}
