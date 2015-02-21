using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickLaunch.Properties;
using System.Configuration;
using System.IO;

namespace QuickLaunch
{
	public partial class Form1 : Form
	{
		YurtleTrack.YurtleTrackPlugin plg;
		string parameters = "﻿<TrackerURL encrypted=\"False\">http://trackingURL.com</TrackerURL><UserName encrypted=\"False\">USERNAME</UserName><Password encrypted=\"False\">PWD</Password>";
		
		public Form1()
		{
			InitializeComponent();
			plg = new YurtleTrack.YurtleTrackPlugin();

            string path = String.Format("{0}\\{1}", Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Test.mysettings");
			if (File.Exists(path))
				parameters = File.ReadAllText(path);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string bugIDOut;
			string [] revPropNames, revPropValues;
			Console.WriteLine(plg.GetCommitMessage2(this.Handle, ﻿parameters, null, null, new string[] { }, null, null, out bugIDOut, out revPropNames, out revPropValues));
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (plg.HasOptions())
				parameters = plg.ShowOptionsDialog(this.Handle, parameters);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			plg.OnCommitFinished(this.Handle, null, null, null, 1);
		}
	}
}
