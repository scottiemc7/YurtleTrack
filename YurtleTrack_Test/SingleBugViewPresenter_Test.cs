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
	public class SingleBugViewPresenter_Test
	{
		public SingleBugViewPresenter_Test() 
        { 
        }

		//[SetUp]
		//public void Init()
		//{ /* ... */ }

		//[TearDown]
		//public void Cleanup()
		//{ /* ... */ }

		[Fact]
		public void ShowBug_Sets_BugProperty_On_View()
		{
			MockRepository repo = new MockRepository();
			ISingleBugView mockBugView = repo.StrictMock<ISingleBugView>();
			IBug mockBug = repo.StrictMock<IBug>();

			mockBugView.Expect(view => view.Bug).SetPropertyWithArgument(mockBug);

			repo.ReplayAll();

			ISingleBugViewPresenter pres = new SingleBugViewPresenter(mockBugView);
			pres.ShowBug(mockBug);

			mockBugView.VerifyAllExpectations();
		}		
	}
}
