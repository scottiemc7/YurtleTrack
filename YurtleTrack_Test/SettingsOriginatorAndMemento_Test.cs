using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using YurtleTrack.Model;
using Xunit;
using System.Windows.Forms;

namespace YurtleTrack_Test
{
	//[TestFixture]
	public class SettingsOriginatorAndMemento_Test
	{
		//[SetUp]
		//public void Init()
		//{ /* ... */ }

		//[TearDown]
		//public void Cleanup()
		//{ /* ... */ }

		//[Test]
		[Fact]
		public void Originator_Can_Set_Get()
		{
			MockRepository repo = new MockRepository();
			ISetting mockSetting1 = repo.StrictMock<ISetting>();

			mockSetting1.Expect(setting => setting.Name).Return("Setting1").Repeat.Any();
			mockSetting1.Expect(setting => setting.Value).Return("Setting1Value").Repeat.Any();

			repo.ReplayAll();

			SettingsOriginator org = new SettingsOriginator();
			org.Set(mockSetting1);
			ISetting ret = org.Get("Setting1");

			Assert.Equal<string>(mockSetting1.Name, ret.Name);
			Assert.Equal<string>(mockSetting1.Value, ret.Value);
		}

		//[Test]
		[Fact]
		public void Originator_Can_Restore_From_Memento()
		{
			MockRepository repo = new MockRepository();
			ISetting mockSetting1 = repo.StrictMock<ISetting>();
			ISetting mockSettingEncrypted = repo.StrictMock<ISetting>();

			mockSetting1.Expect(setting => setting.Name).Return("Setting1").Repeat.Any();
			mockSetting1.Expect(setting => setting.Value).Return("Setting1Value").Repeat.Any();
			mockSetting1.Expect(setting => setting.Encrypt).Return(false).Repeat.Any();

			mockSettingEncrypted.Expect(setting => setting.Name).Return("SettingEncrypted").Repeat.Any();
			mockSettingEncrypted.Expect(setting => setting.Value).Return("SettingEncryptedValue").Repeat.Any();
			mockSettingEncrypted.Expect(setting => setting.Encrypt).Return(true).Repeat.Any();

			repo.ReplayAll();

			SettingsOriginator org = new SettingsOriginator();
			org.Set(mockSetting1);
			org.Set(mockSettingEncrypted);

			ISettingsMemento memento = org.GetMemento();

			SettingsOriginator orgRestored = new SettingsOriginator();
			orgRestored.RestoreFromMemento(memento);

			ISetting setting1 = orgRestored.Get("Setting1");
			Assert.Equal("Setting1", setting1.Name);
			Assert.Equal("Setting1Value", setting1.Value);

			ISetting settingEncrypted = orgRestored.Get("SettingEncrypted");
			Assert.Equal("SettingEncrypted", settingEncrypted.Name);
			Assert.Equal("SettingEncryptedValue", settingEncrypted.Value);
		}
	}
}
