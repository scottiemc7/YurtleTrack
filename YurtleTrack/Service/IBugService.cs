using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;

namespace YurtleTrack.Service
{
	interface IBugService
	{
		int GetTotalBugCount();
		int GetBugCountForProject(IProject project);
		List<IProject> GetProjects();
		List<IBug> GetBugsForProject(IProject project, int page, int pageSize);
		List<IBug> GetFilteredBugsForProject(IProject project, int page, int pageSize, string filterBy, string filterValue);
		void ApplyCommandsToBugs(List<ICommand> commands, List<IBug> bugs);
	}
}
