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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MidProjectDB
{
    public partial class addGroup : UserControl
    { 
        public addGroup()
        {
            InitializeComponent();
        }

        private void addGroup_Load(object sender, EventArgs e)
        {
            ShowData();
            loadDataIntoCombo();
        }
        bool ifGroupExists()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from GroupProject JOIN Group ON GroupProject.GroupId = Group.Id", con);
            cmd.Parameters.AddWithValue("@Id", comboBox3.Text);
            cmd.ExecuteNonQuery();
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        void loadDataIntoCombo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT p.Title FROM Project p LEFT JOIN GroupProject gp ON p.ID = gp.ProjectId WHERE gp.ProjectId IS NULL; ", con);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string value = reader.GetString(0);
                comboBox3.Items.Add(value);
            }
            reader.Close();
        }
        int getProjectId()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id from Project Where Title = @Title", con);
            cmd.Parameters.AddWithValue("@Title", comboBox3.Text);
            cmd.ExecuteNonQuery();
            id = (int)cmd.ExecuteScalar();
            return id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox3.Text == "")
                {
                    MessageBox.Show("Please fill all the fields");
                }
                else
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into [Group](Created_On) values (@CreatedDate)", con);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    int groupId = maxID();
                    groupId += 1;
                    cmd.ExecuteNonQuery();
                    var con1 = Configuration.getInstance().getConnection();
                    SqlCommand cmd1 = new SqlCommand("Insert into GroupProject values(@ProjectId,@GroupId,@AssignmentDate)", con1);
                    cmd1.Parameters.AddWithValue("@ProjectID", getProjectId());
                    cmd1.Parameters.AddWithValue("@GroupId", groupId);
                    cmd1.Parameters.AddWithValue("@AssignmentDate", DateTime.Now);
                    cmd1.ExecuteNonQuery();
                }
                MessageBox.Show("Group has been Created");
                comboBox3.Text = "";
                ShowData();
                comboBox3.Items.Clear();
                loadDataIntoCombo();
            }
            catch (Exception ex)
{
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void ShowData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select GroupId,Project.Title as [Project Title],Format(AssignmentDate,'dd-MM-yyyy') as [Assignment Date],Format(Created_On,'dd-MM-yyyy') as [Created Date] FROM [Group] JOIN GroupProject ON [Group].Id = GroupProject.GroupId join Project on GroupProject.ProjectId = Project.Id", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        int maxID()
        {
            string selectMaxIdQuery = "SELECT Top 1 Id FROM [Group] ORDER BY Id DESC";
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(selectMaxIdQuery, con);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                return 0;
            }
            else
            {
                return (int)result;
            }
            
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            { 
                int groupId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
                string project = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                groupStudent panel1 = new groupStudent(groupId,project);
                panel1.Show();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }
    }
}
