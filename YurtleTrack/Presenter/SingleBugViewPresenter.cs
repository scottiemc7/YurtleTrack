using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;
using YurtleTrack.View;

namespace YurtleTrack.Presenter
{
	class SingleBugViewPresenter : ISingleBugViewPresenter
	{
		private readonly ISingleBugView _singleBugView;
		public SingleBugViewPresenter(ISingleBugView view)
		{
			_singleBugView = view;
		}

		public void ShowBug(IBug bug)
		{
			_singleBugView.Bug = bug;
		}
	}
}
