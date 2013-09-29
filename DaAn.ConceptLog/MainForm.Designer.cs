namespace DaAn.ConceptLog
{
    partial class MainForm
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
            this.mainTools = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMI = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swichToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commitMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.showLogMI = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangesMI = new System.Windows.Forms.ToolStripMenuItem();
            this.conceptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewConceptMI = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conceptsBS = new System.Windows.Forms.BindingSource(this.components);
            this.editConceptMI = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTools.SuspendLayout();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conceptsBS)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTools
            // 
            this.mainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.mainTools.Location = new System.Drawing.Point(0, 24);
            this.mainTools.Name = "mainTools";
            this.mainTools.Size = new System.Drawing.Size(910, 25);
            this.mainTools.TabIndex = 0;
            this.mainTools.Text = "mainTools";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.branchToolStripMenuItem,
            this.commitToolStripMenuItem,
            this.conceptToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(910, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectMI,
            this.openProjectMI,
            this.saveProjectMI,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // newProjectMI
            // 
            this.newProjectMI.Name = "newProjectMI";
            this.newProjectMI.Size = new System.Drawing.Size(103, 22);
            this.newProjectMI.Text = "&New";
            this.newProjectMI.Click += new System.EventHandler(this.newProjectMI_Click);
            // 
            // openProjectMI
            // 
            this.openProjectMI.Name = "openProjectMI";
            this.openProjectMI.Size = new System.Drawing.Size(103, 22);
            this.openProjectMI.Text = "&Open";
            this.openProjectMI.Click += new System.EventHandler(this.openProjectMI_Click);
            // 
            // saveProjectMI
            // 
            this.saveProjectMI.Name = "saveProjectMI";
            this.saveProjectMI.Size = new System.Drawing.Size(103, 22);
            this.saveProjectMI.Text = "&Save";
            this.saveProjectMI.Click += new System.EventHandler(this.saveProjectMI_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // branchToolStripMenuItem
            // 
            this.branchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.swichToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addToolStripMenuItem});
            this.branchToolStripMenuItem.Name = "branchToolStripMenuItem";
            this.branchToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.branchToolStripMenuItem.Text = "Branch";
            // 
            // swichToolStripMenuItem
            // 
            this.swichToolStripMenuItem.Name = "swichToolStripMenuItem";
            this.swichToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.swichToolStripMenuItem.Text = "&Swich";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(102, 6);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.addToolStripMenuItem.Text = "&Add";
            // 
            // commitToolStripMenuItem
            // 
            this.commitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commitMI,
            this.toolStripMenuItem3,
            this.showLogMI,
            this.showChangesMI});
            this.commitToolStripMenuItem.Name = "commitToolStripMenuItem";
            this.commitToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.commitToolStripMenuItem.Text = "Commit";
            // 
            // commitMI
            // 
            this.commitMI.Name = "commitMI";
            this.commitMI.Size = new System.Drawing.Size(150, 22);
            this.commitMI.Text = "&Commit";
            this.commitMI.Click += new System.EventHandler(this.commitMI_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(147, 6);
            // 
            // showLogMI
            // 
            this.showLogMI.Name = "showLogMI";
            this.showLogMI.Size = new System.Drawing.Size(150, 22);
            this.showLogMI.Text = "Show &log";
            // 
            // showChangesMI
            // 
            this.showChangesMI.Name = "showChangesMI";
            this.showChangesMI.Size = new System.Drawing.Size(150, 22);
            this.showChangesMI.Text = "Show changes";
            // 
            // conceptToolStripMenuItem
            // 
            this.conceptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewConceptMI,
            this.detailsMI,
            this.editConceptMI});
            this.conceptToolStripMenuItem.Name = "conceptToolStripMenuItem";
            this.conceptToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.conceptToolStripMenuItem.Text = "Concept";
            // 
            // addNewConceptMI
            // 
            this.addNewConceptMI.Name = "addNewConceptMI";
            this.addNewConceptMI.Size = new System.Drawing.Size(152, 22);
            this.addNewConceptMI.Text = "Add new";
            this.addNewConceptMI.Click += new System.EventHandler(this.addNewConceptMI_Click);
            // 
            // detailsMI
            // 
            this.detailsMI.Name = "detailsMI";
            this.detailsMI.Size = new System.Drawing.Size(152, 22);
            this.detailsMI.Text = "Details";
            this.detailsMI.Click += new System.EventHandler(this.detailsMI_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descriptionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.conceptsBS;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 49);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(910, 505);
            this.dataGridView1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(910, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // conceptsBS
            // 
            this.conceptsBS.DataSource = typeof(DaAn.ConceptLog.Model.Entities.Concept);
            // 
            // editConceptMI
            // 
            this.editConceptMI.Name = "editConceptMI";
            this.editConceptMI.Size = new System.Drawing.Size(152, 22);
            this.editConceptMI.Text = "Edit";
            this.editConceptMI.Click += new System.EventHandler(this.editConceptMI_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 554);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.mainTools);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "ConceptLog";
            this.mainTools.ResumeLayout(false);
            this.mainTools.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conceptsBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainTools;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectMI;
        private System.Windows.Forms.ToolStripMenuItem saveProjectMI;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem commitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commitMI;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem showLogMI;
        private System.Windows.Forms.ToolStripMenuItem showChangesMI;
        private System.Windows.Forms.ToolStripMenuItem branchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swichToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem newProjectMI;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource conceptsBS;
        private System.Windows.Forms.ToolStripMenuItem conceptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewConceptMI;
        private System.Windows.Forms.ToolStripMenuItem detailsMI;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem editConceptMI;
    }
}

