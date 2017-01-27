namespace DataGrid
{
	partial class DBDataGridForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.applicationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.directionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.responseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.messageIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.senderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.recipientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rawDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.eMailTransactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.hISP_LogDataSet = new DataGrid.HISP_LogDataSet();
			this.eMailTransactionsTableAdapter = new DataGrid.HISP_LogDataSetTableAdapters.EMailTransactionsTableAdapter();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.eMailTransactionsBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hISP_LogDataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.applicationDataGridViewTextBoxColumn,
            this.dateTimeDataGridViewTextBoxColumn,
            this.directionDataGridViewTextBoxColumn,
            this.responseDataGridViewTextBoxColumn,
            this.messageIDDataGridViewTextBoxColumn,
            this.senderDataGridViewTextBoxColumn,
            this.recipientDataGridViewTextBoxColumn,
            this.rawDataDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.eMailTransactionsBindingSource;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dataGridView1.Location = new System.Drawing.Point(0, 62);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.Size = new System.Drawing.Size(519, 451);
			this.dataGridView1.TabIndex = 0;
			// 
			// applicationDataGridViewTextBoxColumn
			// 
			this.applicationDataGridViewTextBoxColumn.DataPropertyName = "Application";
			this.applicationDataGridViewTextBoxColumn.HeaderText = "Application";
			this.applicationDataGridViewTextBoxColumn.Name = "applicationDataGridViewTextBoxColumn";
			// 
			// dateTimeDataGridViewTextBoxColumn
			// 
			this.dateTimeDataGridViewTextBoxColumn.DataPropertyName = "DateTime";
			this.dateTimeDataGridViewTextBoxColumn.HeaderText = "DateTime";
			this.dateTimeDataGridViewTextBoxColumn.Name = "dateTimeDataGridViewTextBoxColumn";
			// 
			// directionDataGridViewTextBoxColumn
			// 
			this.directionDataGridViewTextBoxColumn.DataPropertyName = "Direction";
			this.directionDataGridViewTextBoxColumn.HeaderText = "Direction";
			this.directionDataGridViewTextBoxColumn.Name = "directionDataGridViewTextBoxColumn";
			// 
			// responseDataGridViewTextBoxColumn
			// 
			this.responseDataGridViewTextBoxColumn.DataPropertyName = "Response";
			this.responseDataGridViewTextBoxColumn.HeaderText = "Response";
			this.responseDataGridViewTextBoxColumn.Name = "responseDataGridViewTextBoxColumn";
			// 
			// messageIDDataGridViewTextBoxColumn
			// 
			this.messageIDDataGridViewTextBoxColumn.DataPropertyName = "MessageID";
			this.messageIDDataGridViewTextBoxColumn.HeaderText = "MessageID";
			this.messageIDDataGridViewTextBoxColumn.Name = "messageIDDataGridViewTextBoxColumn";
			// 
			// senderDataGridViewTextBoxColumn
			// 
			this.senderDataGridViewTextBoxColumn.DataPropertyName = "Sender";
			this.senderDataGridViewTextBoxColumn.HeaderText = "Sender";
			this.senderDataGridViewTextBoxColumn.Name = "senderDataGridViewTextBoxColumn";
			// 
			// recipientDataGridViewTextBoxColumn
			// 
			this.recipientDataGridViewTextBoxColumn.DataPropertyName = "Recipient";
			this.recipientDataGridViewTextBoxColumn.HeaderText = "Recipient";
			this.recipientDataGridViewTextBoxColumn.Name = "recipientDataGridViewTextBoxColumn";
			// 
			// rawDataDataGridViewTextBoxColumn
			// 
			this.rawDataDataGridViewTextBoxColumn.DataPropertyName = "RawData";
			this.rawDataDataGridViewTextBoxColumn.HeaderText = "RawData";
			this.rawDataDataGridViewTextBoxColumn.Name = "rawDataDataGridViewTextBoxColumn";
			// 
			// eMailTransactionsBindingSource
			// 
			this.eMailTransactionsBindingSource.DataMember = "EMailTransactions";
			this.eMailTransactionsBindingSource.DataSource = this.hISP_LogDataSet;
			this.eMailTransactionsBindingSource.CurrentChanged += new System.EventHandler(this.eMailTransactionsBindingSource_CurrentChanged);
			// 
			// hISP_LogDataSet
			// 
			this.hISP_LogDataSet.DataSetName = "HISP_LogDataSet";
			this.hISP_LogDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// eMailTransactionsTableAdapter
			// 
			this.eMailTransactionsTableAdapter.ClearBeforeFill = true;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker1.TabIndex = 1;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(229, 12);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker2.TabIndex = 2;
			// 
			// DBDataGridForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 513);
			this.Controls.Add(this.dateTimePicker2);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.dataGridView1);
			this.Name = "DBDataGridForm";
			this.Text = "DBDataGridForm";
			this.Load += new System.EventHandler(this.DBDataGridForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.eMailTransactionsBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hISP_LogDataSet)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private HISP_LogDataSet hISP_LogDataSet;
		private System.Windows.Forms.BindingSource eMailTransactionsBindingSource;
		private HISP_LogDataSetTableAdapters.EMailTransactionsTableAdapter eMailTransactionsTableAdapter;
		private System.Windows.Forms.DataGridViewTextBoxColumn applicationDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn directionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn responseDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn messageIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn senderDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn recipientDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn rawDataDataGridViewTextBoxColumn;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
	}
}