using WeifenLuo.WinFormsUI.Docking;

namespace Docking;

public partial class DockableForm : DockContent
{
  public DockableForm(string caption)
  {
    InitializeComponent();
    this.Text = caption;
  }

  private void DockableForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    e.Cancel = true;
  }
}
