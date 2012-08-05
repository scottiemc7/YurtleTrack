using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using YurtleTrack.Model;
using System.Xml;
using System.IO;

namespace YurtleTrack.Service
{
	class HKCURegistrySettingsService: ISettingsService
	{
		private readonly string _root;
		public HKCURegistrySettingsService(string root)
		{
			_root = root;
			RegistryKey rootKey = Registry.CurrentUser.OpenSubKey(_root);
			if (rootKey == null)
				Registry.CurrentUser.CreateSubKey(_root).Close();
		}

		public string GetAllSettingsAsXML()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Settings></Settings>");
			XmlNode rootNode = doc.SelectSingleNode("/Settings");			

			RegistryKey root = Registry.CurrentUser.OpenSubKey(_root);
			try
			{
				string[] valNames = root.GetValueNames();
				if (valNames != null && valNames.Length > 0)
				{
					foreach (string valName in valNames)
					{
						XmlNode valNode = doc.CreateElement(valName);
						valNode.InnerXml = root.GetValue(valName).ToString();
						XmlAttribute attr = doc.CreateAttribute("encrypted");
						attr.Value = false.ToString();
						valNode.Attributes.Append(attr);
						
						rootNode.AppendChild(valNode);
					}//end foreach				
				}//end if
			}
			finally
			{
				root.Close();
			}//end try

			return rootNode.InnerXml;
		}

		public void Set(ISetting setting)
		{
			RegistryKey root = Registry.CurrentUser.OpenSubKey(_root, true);
			try
			{
				if (setting.Encrypt)
					throw new NotImplementedException();//Later
				else
					root.SetValue(setting.Name, setting.Value);
			}
			finally
			{
				root.Close();
			}
		}
	}
}
