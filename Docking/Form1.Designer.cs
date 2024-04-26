namespace Docking
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
      SuspendLayout();
      // 
      // dockPanel1
      // 
      dockPanel1.Dock = DockStyle.Fill;
      dockPanel1.Location = new Point(0, 0);
      dockPanel1.Name = "dockPanel1";
      dockPanel1.Size = new Size(800, 450);
      dockPanel1.TabIndex = 0;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(dockPanel1);
      Name = "Form1";
      Text = "Form1";
      ResumeLayout(false);
    }

    #endregion

    private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
  }
}
