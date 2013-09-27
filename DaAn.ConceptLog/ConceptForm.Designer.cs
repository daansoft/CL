namespace DaAn.ConceptLog
{
    partial class ConceptForm
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
            this.descriptionTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.relatedConceptsBS = new System.Windows.Forms.BindingSource(this.components);
            this.addRelatedConceptBT = new System.Windows.Forms.Button();
            this.removeRelatedConceptBT = new System.Windows.Forms.Button();
            this.saveBT = new System.Windows.Forms.Button();
            this.cancelBT = new System.Windows.Forms.Button();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relatedConceptsBS)).BeginInit();
            this.SuspendLayout();
            // 
            // descriptionTB
            // 
            this.descriptionTB.Location = new System.Drawing.Point(12, 25);
            this.descriptionTB.Multiline = true;
            this.descriptionTB.Name = "descriptionTB";
            this.descriptionTB.Size = new System.Drawing.Size(692, 105);
            this.descriptionTB.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Related concepts";
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
            this.dataGridView1.DataSource = this.relatedConceptsBS;
            this.dataGridView1.Location = new System.Drawing.Point(12, 169);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(692, 268);
            this.dataGridView1.TabIndex = 3;
            // 
            // relatedConceptsBS
            // 
            this.relatedConceptsBS.DataSource = typeof(DaAn.ConceptLog.Model.Entities.Concept);
            // 
            // addRelatedConceptBT
            // 
            this.addRelatedConceptBT.Location = new System.Drawing.Point(12, 443);
            this.addRelatedConceptBT.Name = "addRelatedConceptBT";
            this.addRelatedConceptBT.Size = new System.Drawing.Size(121, 35);
            this.addRelatedConceptBT.TabIndex = 4;
            this.addRelatedConceptBT.Text = "Add";
            this.addRelatedConceptBT.UseVisualStyleBackColor = true;
            this.addRelatedConceptBT.Click += new System.EventHandler(this.addRelatedConceptBT_Click);
            // 
            // removeRelatedConceptBT
            // 
            this.removeRelatedConceptBT.Location = new System.Drawing.Point(139, 443);
            this.removeRelatedConceptBT.Name = "removeRelatedConceptBT";
            this.removeRelatedConceptBT.Size = new System.Drawing.Size(121, 35);
            this.removeRelatedConceptBT.TabIndex = 5;
            this.removeRelatedConceptBT.Text = "Remove";
            this.removeRelatedConceptBT.UseVisualStyleBackColor = true;
            this.removeRelatedConceptBT.Click += new System.EventHandler(this.removeRelatedConceptBT_Click);
            // 
            // saveBT
            // 
            this.saveBT.Location = new System.Drawing.Point(456, 443);
            this.saveBT.Name = "saveBT";
            this.saveBT.Size = new System.Drawing.Size(121, 35);
            this.saveBT.TabIndex = 5;
            this.saveBT.Text = "Save";
            this.saveBT.UseVisualStyleBackColor = true;
            this.saveBT.Click += new System.EventHandler(this.saveBT_Click);
            // 
            // cancelBT
            // 
            this.cancelBT.Location = new System.Drawing.Point(583, 443);
            this.cancelBT.Name = "cancelBT";
            this.cancelBT.Size = new System.Drawing.Size(121, 35);
            this.cancelBT.TabIndex = 5;
            this.cancelBT.Text = "Cancel";
            this.cancelBT.UseVisualStyleBackColor = true;
            this.cancelBT.Click += new System.EventHandler(this.cancelBT_Click);
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // ConceptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 490);
            this.Controls.Add(this.cancelBT);
            this.Controls.Add(this.saveBT);
            this.Controls.Add(this.removeRelatedConceptBT);
            this.Controls.Add(this.addRelatedConceptBT);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionTB);
            this.Name = "ConceptForm";
            this.Text = "ConceptForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relatedConceptsBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox descriptionTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource relatedConceptsBS;
        private System.Windows.Forms.Button addRelatedConceptBT;
        private System.Windows.Forms.Button removeRelatedConceptBT;
        private System.Windows.Forms.Button saveBT;
        private System.Windows.Forms.Button cancelBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}