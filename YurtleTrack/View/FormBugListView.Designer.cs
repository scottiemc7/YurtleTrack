namespace YurtleTrack.View
{
	partial class FormBugListView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.comboBoxProject = new System.Windows.Forms.ComboBox();
			this.comboBoxPage = new System.Windows.Forms.ComboBox();
			this.dataGridViewBugs = new System.Windows.Forms.DataGridView();
			this.bindingSourceBugs = new System.Windows.Forms.BindingSource(this.components);
			this.bindingSourceProjects = new System.Windows.Forms.BindingSource(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.labelSelected = new System.Windows.Forms.Label();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxFilter = new System.Windows.Forms.TextBox();
			this.buttonClearFilter = new System.Windows.Forms.Button();
			this.textBoxCommand = new System.Windows.Forms.TextBox();
			this.checkBoxApplyCommand = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewBugs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceBugs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceProjects)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBoxProject
			// 
			this.comboBoxProject.DisplayMember = "Name";
			this.comboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProject.FormattingEnabled = true;
			this.comboBoxProject.Location = new System.Drawing.Point(13, 13);
			this.comboBoxProject.Name = "comboBoxProject";
			this.comboBoxProject.Size = new System.Drawing.Size(185, 21);
			this.comboBoxProject.TabIndex = 0;
			this.comboBoxProject.ValueMember = "ID";
			// 
			// comboBoxPage
			// 
			this.comboBoxPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboBoxPage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxPage.Location = new System.Drawing.Point(269, 301);
			this.comboBoxPage.Name = "comboBoxPage";
			this.comboBoxPage.Size = new System.Drawing.Size(77, 21);
			this.comboBoxPage.TabIndex = 1;
			// 
			// dataGridViewBugs
			// 
			this.dataGridViewBugs.AllowUserToAddRows = false;
			this.dataGridViewBugs.AllowUserToDeleteRows = false;
			this.dataGridViewBugs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewBugs.AutoGenerateColumns = false;
			this.dataGridViewBugs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewBugs.ColumnHeadersVisible = false;
			this.dataGridViewBugs.DataSource = this.bindingSourceBugs;
			this.dataGridViewBugs.Location = new System.Drawing.Point(13, 41);
			this.dataGridViewBugs.MultiSelect = false;
			this.dataGridViewBugs.Name = "dataGridViewBugs";
			this.dataGridViewBugs.RowHeadersVisible = false;
			this.dataGridViewBugs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewBugs.ShowEditingIcon = false;
			this.dataGridViewBugs.Size = new System.Drawing.Size(517, 226);
			this.dataGridViewBugs.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(204, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Bugs Found: 0";
			// 
			// labelSelected
			// 
			this.labelSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelSelected.AutoSize = true;
			this.labelSelected.Location = new System.Drawing.Point(12, 306);
			this.labelSelected.Name = "labelSelected";
			this.labelSelected.Size = new System.Drawing.Size(61, 13);
			this.labelSelected.TabIndex = 5;
			this.labelSelected.Text = "Selected: 0";
			// 
			// buttonClear
			// 
			this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClear.Location = new System.Drawing.Point(84, 301);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(39, 23);
			this.buttonClear.TabIndex = 4;
			this.buttonClear.Text = "Clear";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(455, 299);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(374, 299);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(228, 304);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Page:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(352, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Bug ID:";
			// 
			// textBoxFilter
			// 
			this.textBoxFilter.AcceptsReturn = true;
			this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFilter.Location = new System.Drawing.Point(401, 13);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new System.Drawing.Size(84, 20);
			this.textBoxFilter.TabIndex = 11;
			// 
			// buttonClearFilter
			// 
			this.buttonClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearFilter.Location = new System.Drawing.Point(491, 11);
			this.buttonClearFilter.Name = "buttonClearFilter";
			this.buttonClearFilter.Size = new System.Drawing.Size(39, 23);
			this.buttonClearFilter.TabIndex = 12;
			this.buttonClearFilter.Text = "Clear";
			this.buttonClearFilter.UseVisualStyleBackColor = true;
			this.buttonClearFilter.Click += new System.EventHandler(this.buttonClearFilter_Click);
			// 
			// textBoxCommand
			// 
			this.textBoxCommand.AcceptsReturn = true;
			this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxCommand.Location = new System.Drawing.Point(124, 275);
			this.textBoxCommand.Name = "textBoxCommand";
			this.textBoxCommand.Size = new System.Drawing.Size(222, 20);
			this.textBoxCommand.TabIndex = 13;
			// 
			// checkBoxApplyCommand
			// 
			this.checkBoxApplyCommand.AutoSize = true;
			this.checkBoxApplyCommand.Location = new System.Drawing.Point(13, 277);
			this.checkBoxApplyCommand.Name = "checkBoxApplyCommand";
			this.checkBoxApplyCommand.Size = new System.Drawing.Size(105, 17);
			this.checkBoxApplyCommand.TabIndex = 14;
			this.checkBoxApplyCommand.Text = "Apply Command:";
			this.checkBoxApplyCommand.UseVisualStyleBackColor = true;
			// 
			// FormBugListView
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(542, 334);
			this.ControlBox = false;
			this.Controls.Add(this.checkBoxApplyCommand);
			this.Controls.Add(this.textBoxCommand);
			this.Controls.Add(this.buttonClearFilter);
			this.Controls.Add(this.textBoxFilter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonClear);
			this.Controls.Add(this.labelSelected);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridViewBugs);
			this.Controls.Add(this.comboBoxPage);
			this.Controls.Add(this.comboBoxProject);
			this.Name = "FormBugListView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "YouTrack Bug List";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewBugs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceBugs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceProjects)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxProject;
		private System.Windows.Forms.ComboBox comboBoxPage;
		private System.Windows.Forms.DataGridView dataGridViewBugs;
		private System.Windows.Forms.BindingSource bindingSourceProjects;
		private System.Windows.Forms.BindingSource bindingSourceBugs;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelSelected;
		private System.Windows.Forms.Button buttonClear;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxFilter;
		private System.Windows.Forms.Button buttonClearFilter;
		private System.Windows.Forms.TextBox textBoxCommand;
		private System.Windows.Forms.CheckBox checkBoxApplyCommand;
	}
}