namespace TraceTest
{
	partial class Form1
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
			this.label1 = new System.Windows.Forms.Label();
			this.ErrorText = new System.Windows.Forms.TextBox();
			this.VerboseText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.InformationText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.WarningText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Error:";
			// 
			// ErrorText
			// 
			this.ErrorText.Location = new System.Drawing.Point(73, 6);
			this.ErrorText.Name = "ErrorText";
			this.ErrorText.Size = new System.Drawing.Size(231, 20);
			this.ErrorText.TabIndex = 0;
			this.ErrorText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Text_KeyUp);
			// 
			// VerboseText
			// 
			this.VerboseText.Location = new System.Drawing.Point(73, 84);
			this.VerboseText.Name = "VerboseText";
			this.VerboseText.Size = new System.Drawing.Size(231, 20);
			this.VerboseText.TabIndex = 4;
			this.VerboseText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Text_KeyUp);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Verbose:";
			// 
			// InformationText
			// 
			this.InformationText.Location = new System.Drawing.Point(73, 58);
			this.InformationText.Name = "InformationText";
			this.InformationText.Size = new System.Drawing.Size(231, 20);
			this.InformationText.TabIndex = 3;
			this.InformationText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Text_KeyUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Information:";
			// 
			// WarningText
			// 
			this.WarningText.Location = new System.Drawing.Point(73, 32);
			this.WarningText.Name = "WarningText";
			this.WarningText.Size = new System.Drawing.Size(231, 20);
			this.WarningText.TabIndex = 1;
			this.WarningText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Text_KeyUp);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 35);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Warning:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(316, 112);
			this.Controls.Add(this.WarningText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.InformationText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.VerboseText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ErrorText);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ErrorText;
		private System.Windows.Forms.TextBox VerboseText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox InformationText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox WarningText;
		private System.Windows.Forms.Label label4;
	}
}

