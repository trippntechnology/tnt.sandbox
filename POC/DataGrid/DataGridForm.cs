using System;
using System.Windows.Forms;

namespace DataGrid
{
	public partial class DataGridForm : Form
	{
		public DataGridForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Sets the SelectedIndexChanged event on the ComboBox
		/// </summary>
		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cb = e.Control as ComboBox;

			if (cb != null)
			{
				cb.SelectedIndexChanged += Column1_CB_SelectedIndexChanged;
			}
		}

		/// <summary>
		/// Sets the associated row with the value chosen in the ComboBox
		/// </summary>
		void Column1_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ComboBox cb = sender as ComboBox;
				int col = dataGridView1.CurrentCell.ColumnIndex;
				int row = dataGridView1.CurrentCell.RowIndex;

				Part part = (cb.DataSource as BindingSource).List[cb.SelectedIndex] as Part;

				if (col == 0)
				{
					dataGridView1.Rows[row].Cells[1].Value = part.Description;
				}
				else if (col == 1)
				{
					dataGridView1.Rows[row].Cells[0].Value = part.Code;
				}

				dataGridView1.Rows[row].Cells[2].Value = 0;

			}
			catch (Exception)
			{
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int col = dataGridView1.CurrentCell.ColumnIndex;
			int row = dataGridView1.CurrentCell.RowIndex;
			Parts parts = dataGridView1.DataSource as Parts;

			parts.RemoveAt(row);
		}
	}
}
