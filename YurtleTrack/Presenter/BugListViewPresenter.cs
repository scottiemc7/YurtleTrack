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
		private bool _isSuspended = false;
		public BugListViewPresenter(IBugListView view, IBugService svc)
		{
			_view = view;
			_svc = svc;
		}

		public void Initialize()
		{
			if (_isSuspended)
				return;

			try
			{
				_view.IsBusy = true;
				_view.Projects = _svc.GetProjects();
			}
			finally
			{
				_view.IsBusy = false;
			}//end try
		}

		public void DisplayProjectDetails()
		{
			if (_view.SelectedProject != null)
			{
				try
				{
					_view.IsBusy = true;
					_view.TotalBugs = _svc.GetBugCountForProject(_view.SelectedProject);
				}
				finally
				{
					_view.IsBusy = false;
				}//end try		
			}//end if
		}

		public void DisplayBugDetails()
		{
			if (_isSuspended)
				return;

			if (_view.SelectedProject != null)
			{
				try
				{
					_view.IsBusy = true;
					if (String.IsNullOrEmpty(_view.FilterValue))
						_view.Bugs = _svc.GetBugsForProject(_view.SelectedProject, _view.Page - 1, _view.PageSize);
					else
						_view.Bugs = _svc.GetFilteredBugsForProject(_view.SelectedProject, _view.Page - 1, _view.PageSize, _view.FilterBy, _view.FilterValue);
				}
				finally
				{
					_view.IsBusy = false;
				}//end try				
			}//end if
		}


		public void SuspendBindings()
		{
			_isSuspended = true;
		}

		public void ResumeBindings()
		{
			_isSuspended = false;
		}
	}
}
