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
    public partial class groupStudent : Form
    {
        advisor advisor = new advisor();
        int group;
        string project;
        public groupStudent(int groupId, string project)
        {
            InitializeComponent();
            this.group = groupId;
            this.project = project;
        }

        private void groupStudent_Load(object sender, EventArgs e)
        {
            showDataIntoCombo();
            loadDataIntoGrid();
            label3.Text = "Group Id: " + group;
            label4.Text = project;
        }
        void showDataIntoCombo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT T1.RegistrationNo FROM Student AS T1 LEFT JOIN GroupStudent AS T2 ON T1.Id = T2.StudentId WHERE T2.StudentId IS NULL", con);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string value = reader.GetString(0);
                comboBox1.Items.Add(value);
            }
            reader.Close();
        }
       string getProjectName()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title from Project where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", project);
            cmd.ExecuteNonQuery();
            return cmd.ExecuteScalar().ToString();
       }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            
                if(comboBox1.Text!= "")
                {
                
                    if (checkLimit())
                    {
                        MessageBox.Show("Group is full");
                    
                    }
                    else
                    {
                        if(checkComboBox() == false)
                        {
                            MessageBox.Show("Student does not exist");
                        }
                        else
                        {
                            var con = Configuration.getInstance().getConnection();
                            SqlCommand cmd = new SqlCommand("Insert into GroupStudent values (@GroupId,@StudentId,@Status,@Date)", con);
                            cmd.Parameters.AddWithValue("@GroupId", group);
                            cmd.Parameters.AddWithValue("@StudentId",getStudentId());
                            cmd.Parameters.AddWithValue("@Status", 3);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                            cmd.ExecuteNonQuery(); 
                            MessageBox.Show("Data has been added"); 
                            comboBox1.Text = "";
                            loadDataIntoGrid();
                            comboBox1.Items.Clear();
                            showDataIntoCombo();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please fill all the fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

}
        int getStudentId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Student where RegistrationNo = @RegistrationNo", con);
            cmd.Parameters.AddWithValue("@RegistrationNo", comboBox1.Text);
            cmd.ExecuteNonQuery();
            return (int)cmd.ExecuteScalar();
        }
        void loadDataIntoGrid()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Student.RegistrationNo,Status,Format(AssignmentDate,'dd-MM-yyyy') as [Assingment Date] from GroupStudent join Student on GroupStudent.StudentId = Student.Id Where GroupId = @Id", con);
            cmd.Parameters.AddWithValue("@Id", group);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        bool checkLimit()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) from GroupStudent Where GroupId = @Id", con);
            cmd.Parameters.AddWithValue("@Id", group);
            if((Convert.ToInt32(cmd.ExecuteScalar())) == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool checkComboBox()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select CASE WHEN EXISTS( SELECT 1 from Student Where RegistrationNo = @Id) THEN 1 ELSE 0 END", con);
            cmd.Parameters.AddWithValue("@Id", comboBox1.Text);
            return (int)cmd.ExecuteScalar() == 1;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            showDataIntoCombo();
            this.Hide();
        }
    }
}
