using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;

namespace YurtleTrack.Model
{
	sealed class XMLSettingsMemento : ISettingsMemento
	{
		public XMLSettingsMemento(string settingsAsString)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Settings>{0}</Settings>", settingsAsString));

			List<ISetting> lst = new List<ISetting>();
			foreach (XmlNode node in doc.SelectNodes("/Settings/*"))
				lst.Add(new Setting() { Name = node.Name, Value = node.InnerText, Encrypt = node.Attributes["encrypted"] != null? Boolean.Parse(node.Attributes["encrypted"].Value) : false });

			Settings = new ReadOnlyCollection<ISetting>(lst);
		}

		public XMLSettingsMemento(List<ISetting> settings)
		{
			Settings = new ReadOnlyCollection<ISetting>(settings);
		}

		public ReadOnlyCollection<ISetting> Settings { get; private set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Settings != null)
			{
				foreach (ISetting setting in Settings)
					sb.AppendFormat("<{0} encrypted=\"{2}\">{1}</{0}>", setting.Name, setting.Value, setting.Encrypt);
			}//end if

			return sb.ToString();
		}
    }
}
