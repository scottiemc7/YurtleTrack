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
using YurtleTrack.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Ninject;

namespace YurtleTrack.View
{
	partial class FormBugListView : Form, IBugListView
	{
		private readonly IBugListViewPresenter _presenter;
		private readonly ISingleBugViewPresenter _singleBugPresenter;
		private readonly ISettingsOriginator _viewSettings;
		private readonly DataGridViewLinkColumn _linkCol;
		public FormBugListView(IBugService svc, ISingleBugViewPresenter sbPres, [Named("ViewSettings")]ISettingsOriginator viewSettings)
		{
			InitializeComponent();

			_presenter = new BugListViewPresenter(this, svc);
			_singleBugPresenter = sbPres;
			_viewSettings = viewSettings;
			SelectedBugs = new ObservableCollection<IBug>();
			Projects = new List<IProject>();
			CommandToApply = new YouTrackCommand() { DisableNotifications = false };

			comboBoxProject.DataSource = bindingSourceProjects;			
			
			//Set up grid
			dataGridViewBugs.Columns.Add(new DataGridViewCheckBoxColumn() { ReadOnly = false, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Resizable = DataGridViewTriState.False, Width = 20 });
			dataGridViewBugs.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ID", ReadOnly = true, Resizable = DataGridViewTriState.True });
			dataGridViewBugs.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Summary", ReadOnly = true, Resizable = DataGridViewTriState.True });
			dataGridViewBugs.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "Description", ReadOnly = true, Resizable = DataGridViewTriState.True, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
			_linkCol = new DataGridViewLinkColumn() { LinkBehavior = LinkBehavior.NeverUnderline, UseColumnTextForLinkValue = true, Text = "view", ReadOnly = true, Resizable = DataGridViewTriState.False, Width = 50, DefaultCellStyle = new DataGridViewCellStyle(dataGridViewBugs.DefaultCellStyle) { Alignment = DataGridViewContentAlignment.MiddleCenter } };
			dataGridViewBugs.Columns.Add(_linkCol);

			dataGridViewBugs.CellContentClick += new DataGridViewCellEventHandler(dataGridViewBugs_CellContentClick);
			bindingSourceProjects.CurrentChanged += (s, ea) => { if (!bindingSourceProjects.IsBindingSuspended) SelectedProject = bindingSourceProjects.Current as IProject; };
			comboBoxPage.SelectedIndexChanged += (s, ea) => UpdatePage();
			comboBoxPage.KeyUp += (s, ea) => { if (ea.KeyCode == Keys.Enter)  UpdatePage(); };
			bindingSourceBugs.DataSourceChanged += new EventHandler(bindingSourceBugs_DataSourceChanged);
			SelectedBugs.CollectionChanged += (s, ea) => labelSelected.Text = String.Format("Selected: {0}", SelectedBugs.Count);
			checkBoxApplyCommand.CheckStateChanged += (s, ea) => { _applyCommand = checkBoxApplyCommand.Checked; };
			textBoxCommand.TextChanged += (s, ea) => { _commandToApply.Command = textBoxCommand.Text; };
		}

		private void UpdatePage()
		{
			int pg = 1;
			Page = Int32.TryParse(comboBoxPage.Text, out pg) ? pg : 1;
		}

		void bindingSourceBugs_DataSourceChanged(object sender, EventArgs e)
		{			
			foreach (DataGridViewRow row in dataGridViewBugs.Rows)
			{
				IBug bug = row.DataBoundItem as IBug;
				if (bug == null)
					continue;

				//Set the check state to true if bug has been selected
				if (SelectedBugs.Contains((IBug)row.DataBoundItem))
					row.Cells[0].Value = true;

				if (!bug.IsResolved)
					continue;

				//Set the font style for resolved bugs to be FontStyle.Strikeout
				foreach (DataGridViewCell cell in row.Cells)
				{
					if (cell.OwningColumn.GetType() != typeof(DataGridViewTextBoxColumn))
						continue;

					if(cell.Style.Font == null)
						cell.Style.Font = new Font(dataGridViewBugs.DefaultCellStyle.Font, FontStyle.Strikeout);
					else
						cell.Style.Font = new Font(row.Cells[1].Style.Font, FontStyle.Strikeout);
				}//end foreach
			}//end foreach
		}

		void dataGridViewBugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == _linkCol.Index)
				_singleBugPresenter.ShowBug((IBug)bindingSourceBugs.Current);
			else if (e.ColumnIndex == 0)//Our check box
			{
				if (dataGridViewBugs.Rows[e.RowIndex].Cells[0].EditedFormattedValue != null && (bool)dataGridViewBugs.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
					SelectedBugs.Add((IBug)bindingSourceBugs.Current);
				else
					SelectedBugs.Remove((IBug)bindingSourceBugs.Current);
			}//end if
		}

		private bool _settingsLoaded = false;
		protected override void OnLoad(EventArgs e)
		{
			_settingsLoaded = false;

			try
			{
				_presenter.Initialize();

				//Load some view settings			
				ISetting locationX = _viewSettings.Get("LocationX");
				ISetting locationY = _viewSettings.Get("LocationY");
				ISetting sizeWidth = _viewSettings.Get("SizeWidth");
				ISetting sizeHeight = _viewSettings.Get("SizeHeight");
				ISetting projectID = _viewSettings.Get("LastProjectID");
				ISetting command = _viewSettings.Get("LastCommandText");
				ISetting useCommand = _viewSettings.Get("LastUseCommand");

				if (locationX != null && locationY != null)
					this.DesktopLocation = new Point(Convert.ToInt32(locationX.Value), Convert.ToInt32(locationY.Value));
				else
					this.StartPosition = FormStartPosition.CenterParent;

				if (sizeWidth != null && sizeHeight != null)
					this.Size = new Size(Convert.ToInt32(sizeWidth.Value), Convert.ToInt32(sizeHeight.Value));

				if (projectID != null)
				{
					IProject proj = Projects.FirstOrDefault(p => p.ID == projectID.Value);
					if (proj != null)
						bindingSourceProjects.Position = bindingSourceProjects.IndexOf(proj);
				}//end if

				if (command != null)
					CommandToApply = new YouTrackCommand() { Command = command.Value, DisableNotifications = false };
				if (useCommand != null)
				{
					try
					{
						ApplyCommand = bool.Parse(useCommand.Value);
					}
					catch { ApplyCommand = false; }
				}//end if

				//Successfully loaded settings
				_settingsLoaded = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}//end try
			
			base.OnLoad(e);			
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			//Only save our settings if nothing funny happened on settings load
			if (_settingsLoaded)
			{
				_viewSettings.Set(new Setting() { Name = "LocationX", Value = this.Location.X.ToString() });
				_viewSettings.Set(new Setting() { Name = "LocationY", Value = this.Location.Y.ToString() });
				_viewSettings.Set(new Setting() { Name = "SizeWidth", Value = this.Size.Width.ToString() });
				_viewSettings.Set(new Setting() { Name = "SizeHeight", Value = this.Size.Height.ToString() });
				_viewSettings.Set(new Setting() { Name = "LastCommandText", Value = textBoxCommand.Text });
				_viewSettings.Set(new Setting() { Name = "LastUseCommand", Value = checkBoxApplyCommand.Checked.ToString() });

				if (bindingSourceProjects.Current != null)
					_viewSettings.Set(new Setting() { Name = "LastProjectID", Value = ((IProject)bindingSourceProjects.Current).ID });
			}//end if

			base.OnClosing(e);
		}

		private IProject _selectedProject;
		public IProject SelectedProject
		{
			get
			{
				return _selectedProject;
			}
			set
			{
				_selectedProject = value;
				Debug.WriteLine(String.Format("Selected Project: {0}", _selectedProject.ID));

				//Refresh our current bug view
			    _presenter.DisplayProjectDetails();
				_presenter.DisplayBugDetails();
			}
		}

		private List<IProject> _projects;
		public List<IProject> Projects
		{
			get
			{
				return _projects;
			}
			set
			{
				_projects = value;

				Debug.WriteLine(String.Format("Showing {0} Projects", _projects.Count));

				//Suspend any bindings momentarily so we don't get any unnecessary updates while rebinding the list of projects
				bindingSourceProjects.SuspendBinding();
				bindingSourceProjects.DataSource = _projects;
				bindingSourceProjects.ResumeBinding();				
			}
		}

		private bool _applyCommand;
		public bool ApplyCommand
		{
			get
			{
				return _applyCommand;
			}
			set
			{
				_applyCommand = value;
				checkBoxApplyCommand.Checked = _applyCommand;
			}
		}

		private ICommand _commandToApply;
		public ICommand CommandToApply
		{
			get
			{
				return _commandToApply;
			}
			set
			{
				_commandToApply = value;
				textBoxCommand.Text = _commandToApply.Command;
			}
		}

		public ObservableCollection<IBug> SelectedBugs
		{
			get;
			private set;
		}

		private List<IBug> _bugs;
		public List<IBug> Bugs
		{
			get
			{
				return _bugs;
			}
			set
			{
				_bugs = value;

				Debug.WriteLine(String.Format("Showing {0} Bugs", _bugs != null ? _bugs.Count : 0));

				bindingSourceBugs.SuspendBinding();
				bindingSourceBugs.DataSource = _bugs;
				bindingSourceBugs.ResumeBinding();
			}
		}

		private int _page = 1;
		public int Page
		{
			get
			{
				return _page;
			}
			set
			{
				if (value <= 0 || value == _page)
					return;

				_page = value;
				comboBoxPage.Text = _page.ToString();
				comboBoxPage.Select(0, 0);
				_presenter.DisplayBugDetails();
			}
		}

		private int _pageSize = 10;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				if (value <= 0 || value == _pageSize)
					return;

				_pageSize = value;				
				_presenter.DisplayBugDetails();
			}
		}

		private int _totalBugs = 1;
		public int TotalBugs
		{
			get
			{
				return _totalBugs;
			}
			set
			{
				comboBoxPage.Items.Clear();
				comboBoxPage.Text = "1";
				if (value < 0)
				{
					label1.Text = "Total Bugs Found: 0";
					
					return;
				}//end if

				_totalBugs = value;
				label1.Text = String.Format("Total Bugs Found: {0}", _totalBugs); 

				//Set up our paging numbers				
				int totalPages = (int)Math.Ceiling((double)_totalBugs / (double)PageSize);
				for (int i = 1; i <= totalPages; i++)
					comboBoxPage.Items.Add(i);
			}
		}

		public string FilterBy
		{
			get
			{
				return "Issue ID";
			}
			set { throw new NotImplementedException(); }
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get 
			{
				return _isBusy; 
			}
			set
			{
				_isBusy = value;
				if (_isBusy)
					Cursor.Current = Cursors.WaitCursor;
				else
					Cursor.Current = Cursors.Default;
			}
		}

		private string _filterValue;
		public string FilterValue
		{
			get
			{
				return _filterValue;
			}
			set
			{
				_presenter.SuspendBindings();
				_filterValue = value;
				Page = 1;
				_presenter.ResumeBindings();
				_presenter.DisplayBugDetails();
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			SelectedBugs.Clear();
			foreach (DataGridViewRow row in dataGridViewBugs.Rows)
				row.Cells[0].Value = false;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			//Override the AcceptButton if we are in the page combo or filter tb
			if (keyData == Keys.Enter)
			{
				if (comboBoxPage.Focused)
				{
					//Switch pages
					int newPage;
					if (!Int32.TryParse(comboBoxPage.Text, out newPage))
						newPage = 1;

					Page = newPage;
					return true;
				}
				else if (textBoxFilter.Focused)
				{
					//Apply filter
					FilterValue = textBoxFilter.Text;
					return true;
				}
				else
					return base.ProcessCmdKey(ref msg, keyData);
			}
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}

		private void buttonClearFilter_Click(object sender, EventArgs e)
		{
			textBoxFilter.Text = string.Empty;
			FilterValue = string.Empty;
		}
	}
}
