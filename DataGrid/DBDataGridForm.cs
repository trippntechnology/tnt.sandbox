using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGrid
{
	public partial class DBDataGridForm : Form
	{
		public DBDataGridForm()
		{
			InitializeComponent();
		}

		private void DBDataGridForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'hISP_LogDataSet.EMailTransactions' table. You can move, or remove it, as needed.
			this.eMailTransactionsTableAdapter.Fill(this.hISP_LogDataSet.EMailTransactions);

		}

		private void fillByToolStripButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.eMailTransactionsTableAdapter.FillBy(this.hISP_LogDataSet.EMailTransactions);
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

		}

		private void eMailTransactionsBindingSource_CurrentChanged(object sender, EventArgs e)
		{

		}
	}
}
