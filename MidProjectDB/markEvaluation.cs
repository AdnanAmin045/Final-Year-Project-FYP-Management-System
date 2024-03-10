using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidProjectDB
{
    public partial class markEvaluation : UserControl
    {
        public markEvaluation()
        {
            InitializeComponent();
        }

        private void markEvaluation_Load(object sender, EventArgs e)
        {
            showDataIntoGrid();
        }
        void showDataIntoGrid()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from [Group]", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int groupId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
                giveMarks panel1 = new giveMarks(groupId);
                panel1.Show();
            }
        }
    }
}
