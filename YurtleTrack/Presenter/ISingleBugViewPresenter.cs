using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;

namespace YurtleTrack.Presenter
{
	interface ISingleBugViewPresenter
	{
		void ShowBug(IBug bug);
	}
}
