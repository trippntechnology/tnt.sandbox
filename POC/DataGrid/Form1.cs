using System;
using System.Windows.Forms;

namespace DataGrid
{
	public partial class Form1 : Form
	{
		Inventory _Inventory;
		Parts _Parts = new Parts();

		public Form1()
		{
			InitializeComponent();

			_Inventory = new Inventory();

			for (int i = 0; i < 100; i++)
			{
				//_Inventory.Add(new Part("Code" + i, "Description" + i, i));
				_Inventory.Add(new Part()
					{
						Code = "Code"+i,
						Description = "Description"+i
					});
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Create new form
			DataGridForm dgf = new DataGridForm();

			// Bind the inventory to the partsBindingSource that is associated with the ComboBox
			dgf.inventoryBindingSource.DataSource = _Inventory;

			// Bind the parts with the grid's datasource
			dgf.dataGridView1.DataSource = _Parts;

			dgf.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DBDataGridForm dbGrid = new DBDataGridForm();

			dbGrid.Show();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Form2 form = new Form2();

			form.partsBindingSource.DataSource = _Inventory;

			form.dataGridView1.DataSource = _Parts;

			form.ShowDialog();
		}
	}
}
