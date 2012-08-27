using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YurtleTrack.Model;
using YurtleTrack.Presenter;
using Ninject;

namespace YurtleTrack.View
{
	partial class FormOptions : Form, IOptionsView
	{
		private readonly IOptionsPresenter _presenter;
		private FormOptions()
		{
			InitializeComponent();
		}
		public FormOptions([Named("Parameters")]ISettingsOriginator originator) : this()
		{
			_presenter = new OptionsPresenter(this, originator);
		}

		protected override void OnLoad(EventArgs e)
		{
			_presenter.Initalize();

			base.OnLoad(e);
		}

		private string _url;
		public string URL
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
				textBox1.Text = _url;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			URL = textBox1.Text;
			UserName = textBoxUN.Text;
			Password = textBoxPwd.Text;

			//Save
			_presenter.Save();

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			//_presenter.Rollback();

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private string _userName;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
				textBoxUN.Text = _userName;
			}
		}

		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				textBoxPwd.Text = _password;
			}
		}
	}
}
