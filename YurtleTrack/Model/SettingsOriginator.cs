using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace YurtleTrack.Model
{
	class SettingsOriginator : ISettingsOriginator
	{
		private readonly List<ISetting> _settings = new List<ISetting>();
		public SettingsOriginator()
		{
		}
		public SettingsOriginator(ISettingsMemento memento)
			: this()
		{
			RestoreFromMemento(memento);
		}

		public ISetting Get(string name)
		{
			return _settings.FirstOrDefault(s => s.Name == name);
		}

		public void Set(ISetting setting)
		{
			ISetting found = _settings.FirstOrDefault(s => s.Name == setting.Name);
			if (found != null)
				_settings.Remove(found);
			_settings.Add(setting);
		}

		public ISettingsMemento GetMemento()
		{
			List<ISetting> encryptedSettings = new List<ISetting>(_settings.Count);
			foreach (ISetting setting in _settings)
			{
				ISetting newSetting = new Setting() { Name = setting.Name, Encrypt = setting.Encrypt };
				if (setting.Encrypt && !String.IsNullOrEmpty(setting.Value))
					newSetting.Value = EncryptAsBase64(setting.Value);
				else
					newSetting.Value = setting.Value;
				encryptedSettings.Add(newSetting);
			}//end foreach

			return new XMLSettingsMemento(encryptedSettings);
		}

		public void RestoreFromMemento(ISettingsMemento memento)
		{
			_settings.Clear();
			foreach (ISetting setting in memento.Settings)
			{
				ISetting newSetting = new Setting() { Name = setting.Name };
				if (setting.Encrypt & !String.IsNullOrEmpty(setting.Value))
					newSetting.Value = DecryptFromBase64(setting.Value);
				else
					newSetting.Value = setting.Value;
				_settings.Add(newSetting);
			}//end foreach
		}

		private byte[] _entropy = { 254, 12, 222, 25, 48, 112, 175, 3, 9 };
		private string EncryptAsBase64(string value)
		{
			if (String.IsNullOrEmpty(value))
				throw new ArgumentNullException("value");

			return Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(value), _entropy, DataProtectionScope.LocalMachine));
		}

		private string DecryptFromBase64(string encryptedValue)
		{
			if (String.IsNullOrEmpty(encryptedValue))
				throw new ArgumentNullException("value");

			return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encryptedValue), _entropy, DataProtectionScope.LocalMachine));
		}
	}
}
