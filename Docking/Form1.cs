namespace Docking;

public partial class Form1 : Form
{
  private List<DockableForm> dockables = new List<DockableForm>();

  public Form1()
  {
    InitializeComponent();
    dockables.Add(new DockableForm("Dock 1"));
    dockables.Add(new DockableForm("Dock 2"));
    dockables.Add(new DockableForm("Dock 3"));
    dockables.Add(new DockableForm("Dock 4"));
    dockPanel1.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
    dockables.ForEach(dockable => dockable.Show(dockPanel1));
  }
}
