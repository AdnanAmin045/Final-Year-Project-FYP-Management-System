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
    public partial class project : UserControl
    {
        string title;
        string description;
        public project()
        {
            InitializeComponent();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            clearBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (titletxt.Text == "" || descriptiontxt.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if(!checkDuplication())
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Project values (@Description,@Title)", con);
                cmd.Parameters.AddWithValue("@Title", (titletxt.Text));
                cmd.Parameters.AddWithValue("@Description", descriptiontxt.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been added");
                loadProjectData();
                clearBoxes();
            }
            else
            {
                MessageBox.Show("Data already exists");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int personID = getProjectId();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Update Project SET  Title = @Title, Description = @Description WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", personID);
            cmd.Parameters.AddWithValue("@Title", (titletxt.Text));
            cmd.Parameters.AddWithValue("@Description", descriptiontxt.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Updated");
            loadProjectData();
            clearBoxes();
        }

        private void project_Load(object sender, EventArgs e)
        {
            loadProjectData();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                titletxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                descriptiontxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                title = titletxt.Text;
                description = descriptiontxt.Text;
            }
        }
        void loadProjectData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title,Description from Project", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        int getProjectId()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Project WHERE Title = @Title AND Description = @Description", con);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Description", description);
            id = Convert.ToInt32(cmd.ExecuteScalar());
            return id;
        }
        bool checkDuplication()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Count(*) from Project Where Title = @Title AND Description = @Description", con);
            cmd1.Parameters.AddWithValue("@Title", (titletxt.Text));
            cmd1.Parameters.AddWithValue("@Description", descriptiontxt.Text);
            cmd1.ExecuteNonQuery();
            if(Convert.ToInt32(cmd1.ExecuteScalar()) > 0)
            {
                flag = true;
            }

            return flag;
        }
        void clearBoxes()
        {
            titletxt.Text = "";
            descriptiontxt.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
