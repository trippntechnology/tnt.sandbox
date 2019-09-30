namespace Async
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
			this.button1 = new System.Windows.Forms.Button();
			this.tb = new System.Windows.Forms.TextBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
			// 
			// tb
			// 
			this.tb.Location = new System.Drawing.Point(12, 41);
			this.tb.Multiline = true;
			this.tb.Name = "tb";
			this.tb.Size = new System.Drawing.Size(248, 252);
			this.tb.TabIndex = 1;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(276, 41);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(235, 251);
			this.listBox1.TabIndex = 2;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(13, 313);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(247, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUpAsync);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(523, 447);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.tb);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tb;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.TextBox textBox1;
	}
}

