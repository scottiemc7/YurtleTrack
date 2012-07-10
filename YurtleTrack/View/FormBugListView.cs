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

namespace YurtleTrack.View
{
	partial class FormBugListView : Form, IBugListView
	{
		private readonly IBugListViewPresenter _presenter;
		private readonly ISingleBugViewPresenter _singleBugPresenter;
		public FormBugListView(IBugService svc, ISingleBugViewPresenter sbPres)
		{
			InitializeComponent();

			_presenter = new BugListViewPresenter(this, svc);
			_singleBugPresenter = sbPres;
			SelectedBugs = new ObservableCollection<IBug>();
			Projects = new List<IProject>();

			comboBox1.DataSource = bindingSourceProjects;			
			
			//Set up grid
			dataGridViewBugs.Columns.Add(new DataGridViewCheckBoxColumn() { ReadOnly = false, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, Resizable = DataGridViewTriState.False, Width = 20 });
			dataGridViewBugs.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = "ID", ReadOnly = true, Resizable = DataGridViewTriState.True });
			dataGridViewBugs.Columns.Add(new DataGridViewLinkColumn() { LinkBehavior = LinkBehavior.NeverUnderline, UseColumnTextForLinkValue = true, Text = "view", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, Resizable = DataGridViewTriState.True });

			dataGridViewBugs.CellContentClick += new DataGridViewCellEventHandler(dataGridViewBugs_CellContentClick);
			bindingSourceProjects.CurrentChanged += (s, ea) => { if (!bindingSourceProjects.IsBindingSuspended) SelectedProject = bindingSourceProjects.Current as IProject; };
			comboBoxPage.SelectedIndexChanged += (s, ea) => UpdatePage();
			comboBoxPage.KeyUp += (s, ea) => { if (ea.KeyCode == Keys.Enter)  UpdatePage(); };
			bindingSourceBugs.DataSourceChanged += new EventHandler(bindingSourceBugs_DataSourceChanged);
			SelectedBugs.CollectionChanged += (s, ea) => labelSelected.Text = String.Format("Selected: {0}", SelectedBugs.Count);
		}

		private void UpdatePage()
		{
			int pg = 1;
			Page = Int32.TryParse(comboBoxPage.Text, out pg) ? pg : 1;
		}

		void bindingSourceBugs_DataSourceChanged(object sender, EventArgs e)
		{
			if (SelectedBugs.Count == 0)
				return;

			foreach (DataGridViewRow row in dataGridViewBugs.Rows)
			{
				if (SelectedBugs.Contains((IBug)row.DataBoundItem))
					row.Cells[0].Value = true;
			}//end foreach
		}

		void dataGridViewBugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 2)
				_singleBugPresenter.ShowBug((IBug)bindingSourceBugs.Current);
			else if (e.ColumnIndex == 0)//Our check box
			{
				if (dataGridViewBugs.Rows[e.RowIndex].Cells[0].EditedFormattedValue != null && (bool)dataGridViewBugs.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
					SelectedBugs.Add((IBug)bindingSourceBugs.Current);
				else
					SelectedBugs.Remove((IBug)bindingSourceBugs.Current);
			}//end if
		}

		protected override void OnLoad(EventArgs e)
		{
			_presenter.Initialize();

			base.OnLoad(e);			
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

				bindingSourceProjects.SuspendBinding();
				bindingSourceProjects.DataSource = _projects;
				bindingSourceProjects.ResumeBinding();				
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
	}
}
