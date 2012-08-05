using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using YurtleTrack.Presenter;
using YurtleTrack.Service;
using YurtleTrack.View;
using Ninject;
using YurtleTrack.Model;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Ninject.Parameters;

namespace YurtleTrack
{
	[ComVisible(true), Guid("0044f6c0-b999-11e1-afa6-0800200c9a66"), ClassInterface(ClassInterfaceType.None)]
	public class YurtleTrackPlugin : IBugTraqProvider2, IBugTraqProvider
    {
		public static string URLOPTIONNAME = "TrackerURL";
		public static string USEROPTIONNAME = "UserName";
		public static string PASSWORDOPTIONNAME = "Password";

		private readonly IKernel _kernel;
        private readonly MemoryStream _ms;
		public YurtleTrackPlugin()
		{
            _ms = new MemoryStream();

			_kernel = new StandardKernel();

			_kernel.Bind<IHttpWebRequestFactory>().To<HttpWebRequestFactory>();
			_kernel.Bind<IBugService>().To<YouTrackBugService>();			
			_kernel.Bind<ISingleBugViewPresenter>().To<SingleBugViewPresenter>();
			_kernel.Bind<ISingleBugView>().To<YouTrackWebSingleBugView>();
			_kernel.Bind<ISettingsOriginator>().To<SettingsOriginator>();
			_kernel.Bind<ISettingsMemento>().To<XMLSettingsMemento>();
			_kernel.Bind<ISettingsService>().ToConstant<HKCURegistrySettingsService>(new HKCURegistrySettingsService("YurtleTrack\\UserSettings"));

			_kernel.Bind<FormBugListView>().To<FormBugListView>();
			_kernel.Bind<FormOptions>().To<FormOptions>();
		}

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            return true;
        }

        public string GetLinkText(IntPtr hParentWnd, string parameters)
        {
            return "Choose Issue(s)";
        }

        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList,
                                       string originalMessage)
        {
            string[] revPropNames = new string[0];
            string[] revPropValues = new string[0];
            string dummystring = "";
            return GetCommitMessage2( hParentWnd, parameters, "", commonRoot, pathList, originalMessage, "", out dummystring, out revPropNames, out revPropValues );
        }

        public string GetCommitMessage2( IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList,
                               string originalMessage, string bugID, out string bugIDOut, out string[] revPropNames, out string[] revPropValues )
        {

			try
			{
				Cursor.Current = Cursors.AppStarting;

				revPropNames = new string[0];
				revPropValues = new string[0];
				bugIDOut = string.Empty;

				ISettingsOriginator originator = RestoreFromParameters(parameters);
				_kernel.Rebind<IBugService>().ToConstructor<YouTrackBugService>(svc => new YouTrackBugService(svc.Inject<IHttpWebRequestFactory>(), originator.Get(YurtleTrackPlugin.URLOPTIONNAME).Value, originator.Get(YurtleTrackPlugin.USEROPTIONNAME).Value, originator.Get(YurtleTrackPlugin.PASSWORDOPTIONNAME).Value));
				_kernel.Rebind<ISettingsOriginator>().ToConstant(originator);

				ISettingsOriginator viewSettingsOriginator = RestoreFromCurrentUserSettings();
				FormBugListView vw = _kernel.Get<FormBugListView>(new ConstructorArgument("viewSettings", viewSettingsOriginator));
				//FormBugListView vw = _kernel.Get<FormBugListView>();
				if (vw.ShowDialog() != DialogResult.OK)
					return originalMessage;

				StringBuilder sb = new StringBuilder(originalMessage);
				if (!String.IsNullOrEmpty(originalMessage) && !originalMessage.EndsWith("\n"))
					sb.AppendLine();

				if (vw.SelectedBugs != null && vw.SelectedBugs.Count > 0)
				{
					foreach (IBug bug in vw.SelectedBugs)
					{
						sb.AppendFormat("Fixed #{0} : {1}", bug.ID, bug.Summary);
						sb.AppendLine();
					}//end foreach
				}//end if

				return sb.ToString();
			}
			catch (Exception ex)
			{
				//MessageBox.Show(ex.ToString());
				throw;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
        }

        public string CheckCommit( IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string commitMessage )
        {
            return null;
        }

        public string OnCommitFinished( IntPtr hParentWnd, string commonRoot, string[] pathList, string logMessage, int revision )
        {
            // we now could use the selectedTickets member to find out which tickets
            // were assigned to this commit.
			//CommitFinishedForm form = new CommitFinishedForm( selectedTickets );
			//if ( form.ShowDialog( ) != DialogResult.OK )
			//    return "";
            // just for testing, we return an error string
           // return "an error happened while closing the issue";

			return null;
        }

        public bool HasOptions()
        {
            return true;
        }

		private ISettingsOriginator RestoreFromParameters(string parameters)
		{
			//Restore our saved memento
			ISettingsMemento memento = _kernel.Get<ISettingsMemento>(new ConstructorArgument("settingsAsString", parameters));

			//Restore originator from memento
			ISettingsOriginator originator = _kernel.Get<ISettingsOriginator>(new ConstructorArgument("memento", memento));

			return originator;
		}

		private ISettingsOriginator RestoreFromCurrentUserSettings()
		{
			//Grab settings as string
			ISettingsService svc = _kernel.Get<ISettingsService>();			

			//Restore our saved memento
			ISettingsMemento memento = _kernel.Get<ISettingsMemento>(new ConstructorArgument("settingsAsString", svc.GetAllSettingsAsXML()));

			//Restore originator from memento
			ISettingsOriginator originator = _kernel.Get<ISettingsOriginator>(new ConstructorArgument("memento", memento));

			return originator;
		}

        public string ShowOptionsDialog(IntPtr hParentWnd, string parameters)
        {
			ISettingsOriginator originator = RestoreFromParameters(parameters);

			//Show the options dialog
			FormOptions frm = _kernel.Get<FormOptions>(new ConstructorArgument("originator", originator));
			NativeWindow nativeWindow = new NativeWindow();
			try
			{
				nativeWindow.AssignHandle(hParentWnd);
			}
			catch { nativeWindow = null; }

			if (nativeWindow != null)
				frm.ShowDialog(nativeWindow);
			else
				frm.ShowDialog();

			//Save our memento
			return originator.GetMemento().ToString();
        }
	}
}
