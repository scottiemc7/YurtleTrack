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
	public class OptionsPresenter_Test
	{
		public OptionsPresenter_Test() 
        { 
        }

		//[SetUp]
		//public void Init()
		//{ /* ... */ }

		//[TearDown]
		//public void Cleanup()
		//{ /* ... */ }

		[Fact]
		public void Initalize_Sets_URL_On_View()
		{
			MockRepository repo = new MockRepository();
			IOptionsView mockOptionsView = repo.StrictMock<IOptionsView>();
			ISettingsOriginator mockSettingsOriginator = repo.StrictMock<ISettingsOriginator>();
			ISettingsMemento mockSettingsMemento = repo.StrictMock<ISettingsMemento>();
			ISetting urlSetting = repo.StrictMock<ISetting>();
			string url = "http://www.google.com";

			urlSetting.Expect(set => set.Value).Return(url);

			mockSettingsOriginator.Expect(org => org.Get("SOMESETTINGNAME")).IgnoreArguments().Return(urlSetting);
			mockSettingsOriginator.Expect(org => org.GetMemento()).Return(mockSettingsMemento);

			mockOptionsView.Expect(view => view.URL).SetPropertyWithArgument(url);

			repo.ReplayAll();

			IOptionsPresenter pres = new OptionsPresenter(mockOptionsView, mockSettingsOriginator);
			pres.Initalize();

			mockOptionsView.VerifyAllExpectations();
			mockSettingsOriginator.VerifyAllExpectations();
			urlSetting.VerifyAllExpectations();
		}		
	}
}
