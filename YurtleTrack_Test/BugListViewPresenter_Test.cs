using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.View;
using YurtleTrack.Service;
using Rhino.Mocks;
using YurtleTrack.Presenter;
using YurtleTrack.Model;
using System.ComponentModel;
using Xunit;

namespace YurtleTrack_Test
{
	public class BugListViewPresenter_Test
	{
		public BugListViewPresenter_Test() 
        { 
        }

		//[SetUp]
		//public void Init()
		//{ /* ... */ }

		//[TearDown]
		//public void Cleanup()
		//{ /* ... */ }

		[Fact]
		public void Initalize_Should_Set_ProjectList_From_BugService()
		{
			MockRepository repo = new MockRepository();
			IBugListView mockBugView = repo.StrictMock<IBugListView>();
			IBugService mockBugService = repo.StrictMock<IBugService>();
			IProject[] mockProjectsArray = new IProject[] { repo.StrictMock<IProject>(), repo.StrictMock<IProject>() };
			List<IProject> projects = new List<IProject>(mockProjectsArray);

			mockBugService.Expect(svc => svc.GetProjects()).Return(projects);
			mockBugView.Expect(view => view.Projects).SetPropertyWithArgument(projects);

			repo.ReplayAll();

			IBugListViewPresenter pres = new BugListViewPresenter(mockBugView, mockBugService);
			pres.Initialize();

			mockBugService.VerifyAllExpectations();
			mockBugView.VerifyAllExpectations();
		}

		[Fact]
		public void DisplayBugDetails_Should_Set_Bugs_From_BugService()
		{
			MockRepository repo = new MockRepository();
			IBugListView mockBugView = repo.StrictMock<IBugListView>();
			IBugService mockBugService = repo.StrictMock<IBugService>();
			IProject mockProject = repo.StrictMock<IProject>();
			IBug[] mockBugsArray = new IBug[] { repo.StrictMock<IBug>(), repo.StrictMock<IBug>() };
			List<IBug> mockBugs = new List<IBug>(mockBugsArray);

			mockBugService.Expect(svc => svc.GetBugsForProject(mockProject, 0, 10)).Return(mockBugs);

			mockBugView.Expect(view => view.SelectedProject).Return(mockProject).Repeat.Twice();
			mockBugView.Expect(view => view.Page).Return(1);
			mockBugView.Expect(view => view.PageSize).Return(10);
			mockBugView.Expect(view => view.Bugs).SetPropertyWithArgument(mockBugs);		

			repo.ReplayAll();

			IBugListViewPresenter pres = new BugListViewPresenter(mockBugView, mockBugService);
			pres.DisplayBugDetails();

			mockBugService.VerifyAllExpectations();
			mockBugView.VerifyAllExpectations();
		}

		[Fact]
		public void DisplayProjectDetails_Should_Set_Bug_Count_From_BugService()
		{
			MockRepository repo = new MockRepository();
			IBugListView mockBugView = repo.StrictMock<IBugListView>();
			IBugService mockBugService = repo.StrictMock<IBugService>();
			IProject mockProject = repo.StrictMock<IProject>();
			IBug[] mockBugsArray = new IBug[] { repo.StrictMock<IBug>(), repo.StrictMock<IBug>() };
			List<IBug> mockBugs = new List<IBug>(mockBugsArray);

			mockBugView.Expect(view => view.SelectedProject).Return(mockProject).Repeat.Twice();
			mockBugService.Expect(svc => svc.GetBugCountForProject(mockProject)).Return(200);
			mockBugView.Expect(view => view.TotalBugs).SetPropertyWithArgument(200);

			repo.ReplayAll();

			IBugListViewPresenter pres = new BugListViewPresenter(mockBugView, mockBugService);
			pres.DisplayProjectDetails();

			mockBugService.VerifyAllExpectations();
			mockBugView.VerifyAllExpectations();
		}
	}
}
