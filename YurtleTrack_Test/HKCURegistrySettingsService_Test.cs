using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Rhino.Mocks;
using YurtleTrack.Model;
using YurtleTrack.Service;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Xml;

namespace YurtleTrack_Test
{
	public class HKCURegistrySettingsService_Test
	{
		[Fact]
		public void RegistryService_Can_Set()
		{
			MockRepository repo = new MockRepository();
			ISetting mockSetting1 = repo.StrictMock<ISetting>();

			mockSetting1.Expect(setting => setting.Name).Return("Setting1").Repeat.Any();
			mockSetting1.Expect(setting => setting.Value).Return("Setting1Value").Repeat.Any();
			mockSetting1.Expect(setting => setting.Encrypt).Return(false).Repeat.Any();

			repo.ReplayAll();

			string root = "Someplace\\SomeplaceElse";
			HKCURegistrySettingsService svc = new HKCURegistrySettingsService(root);
			svc.Set(mockSetting1);

			//Check registry
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(root, true))
			{
				Assert.True(key != null);
				Assert.Equal<string>(key.GetValue(mockSetting1.Name).ToString(), mockSetting1.Value);

				Registry.CurrentUser.DeleteSubKey(root);
			}//end using
		}

		[Fact]
		public void RegistryService_Can_Get_All_As_XML()
		{
			MockRepository repo = new MockRepository();
			ISetting mockSetting1 = repo.StrictMock<ISetting>();
			ISetting mockSetting2 = repo.StrictMock<ISetting>();
						
			mockSetting1.Expect(setting => setting.Name).Return("Setting1").Repeat.Any();
			mockSetting1.Expect(setting => setting.Value).Return("Setting1Value").Repeat.Any();
			mockSetting1.Expect(setting => setting.Encrypt).Return(false).Repeat.Any();

			mockSetting2.Expect(setting => setting.Name).Return("Setting2").Repeat.Any();
			mockSetting2.Expect(setting => setting.Value).Return("Setting2Value").Repeat.Any();
			mockSetting2.Expect(setting => setting.Encrypt).Return(false).Repeat.Any();

			repo.ReplayAll();

			string root = "Someplace\\SomeplaceElse";
			HKCURegistrySettingsService svc = new HKCURegistrySettingsService(root);
			svc.Set(mockSetting1);
			svc.Set(mockSetting2);

			string xml = svc.GetAllSettingsAsXML();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Settings>{0}</Settings>", xml));


			XmlNode setting1Node = doc.SelectSingleNode("/Settings/Setting1");
			XmlNode setting2Node = doc.SelectSingleNode("/Settings/Setting2");

			Assert.True(setting1Node != null);
			Assert.True(setting2Node != null);

			Assert.True(setting1Node.Attributes["encrypted"] != null);
			Assert.True(setting2Node.Attributes["encrypted"] != null);

			Assert.Equal<string>(setting1Node.Attributes["encrypted"].Value, false.ToString());
			Assert.Equal<string>(setting2Node.Attributes["encrypted"].Value, false.ToString());

			Assert.Equal<string>(setting1Node.InnerXml, "Setting1Value");
			Assert.Equal<string>(setting2Node.InnerXml, "Setting2Value");

			//Clean registry
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(root, true))
				Registry.CurrentUser.DeleteSubKey(root);
		}

		
	}
}
