using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YurtleTrack.View;
using YurtleTrack.Model;
using System.Security.Cryptography;
using Ninject;

namespace YurtleTrack.Presenter
{
	class OptionsPresenter : IOptionsPresenter
	{
		private readonly IOptionsView _view;
		private readonly ISettingsOriginator _settings;
		private ISettingsMemento _memento;
		public OptionsPresenter(IOptionsView view, [Named("Parameters")]ISettingsOriginator settings)
		{
			_view = view;
			_settings = settings;
		}

		public void Initalize()
		{
			//Reset our settings memento before we start
			_memento = _settings.GetMemento();

			//Set up the view
			ISetting url = _settings.Get(YurtleTrackPlugin.URLOPTIONNAME);
			if (url != null)
				_view.URL = url.Value;
			else
				_view.URL = string.Empty;

			ISetting user = _settings.Get(YurtleTrackPlugin.USEROPTIONNAME);
			if (url != null)
				_view.UserName = user.Value;
			else
				_view.UserName = string.Empty;

			ISetting pass = _settings.Get(YurtleTrackPlugin.PASSWORDOPTIONNAME);
			if (url != null)
				_view.Password = pass.Value;
			else
				_view.Password = string.Empty;
		}

		public void Save()
		{
			_settings.Set(new Setting() { Name = YurtleTrackPlugin.URLOPTIONNAME, Value = _view.URL });
			_settings.Set(new Setting() { Name = YurtleTrackPlugin.USEROPTIONNAME, Value = _view.UserName });
			_settings.Set(new Setting() { Name = YurtleTrackPlugin.PASSWORDOPTIONNAME, Value = _view.Password, Encrypt = true });
		}

		public void Rollback()
		{
			if (_memento != null)
				_settings.RestoreFromMemento(_memento);
			else
				throw new InvalidOperationException("The presenter must be initalized before you can call Rollback");
		}		
	}
}
