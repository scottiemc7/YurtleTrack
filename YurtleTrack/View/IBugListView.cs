using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YurtleTrack.View
{
	interface IBugListView
	{
		IProject SelectedProject { get; set; }
		ObservableCollection<IBug> SelectedBugs { get; }
		List<IProject> Projects { get; set; }
		List<IBug> Bugs { get; set; }
		int Page { get; set; }
		int TotalBugs { get; set; }
		int PageSize { get; set; }
		string FilterBy { get; set; }
		string FilterValue { get; set; }
		bool IsBusy { get; set; }
		bool ApplyCommand { get; set; }
		ICommand CommandToApply { get; set; }

		//List<ICommand> AvailableCommands { get; set; }
		//List<ICommand> SelectedCommands { get; set; }
	}
}
