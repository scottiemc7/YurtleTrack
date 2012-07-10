using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.View;
using YurtleTrack.Service;
using YurtleTrack.Model;
using System.Diagnostics;

namespace YurtleTrack.Presenter
{
	internal class BugListViewPresenter : IBugListViewPresenter
	{
		private readonly IBugListView _view;
		private readonly IBugService _svc;
		public BugListViewPresenter(IBugListView view, IBugService svc)
		{
			_view = view;
			_svc = svc;
		}

		public void Initialize()
		{
			_view.Projects = _svc.GetProjects();
		}

		public void DisplayProjectDetails()
		{
			if (_view.SelectedProject != null)
			{
				_view.TotalBugs = _svc.GetBugCountForProject(_view.SelectedProject);
			}
		}

		public void DisplayBugDetails()
		{
			if (_view.SelectedProject != null)
			{
				_view.Bugs = _svc.GetBugsForProject(_view.SelectedProject, _view.Page-1, _view.PageSize);				
			}
		}
	}
}
