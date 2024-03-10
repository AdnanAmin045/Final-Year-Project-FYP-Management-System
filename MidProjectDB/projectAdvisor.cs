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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MidProjectDB
{
    public partial class projectAdvisor : UserControl
    {
        public projectAdvisor()
        {
            InitializeComponent();
        }
        void getAdvisorIntoCombo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select FirstName + LastName from Person JOIN Advisor ON Person.Id = Advisor.Id", con);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string value = reader.GetString(0);
                advisorNameCB.Items.Add(value);
            }
            reader.Close();

        }
        void getProjectIntoCombo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Title from Project", con);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string value = reader.GetString(0);
                projectCB.Items.Add(value);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (checkProjectRoles())
                {


                    if (CheckRole())
                    {
                        MessageBox.Show("AdvisorRole already exists");
                    }
                    else
                    {

                        if (advisorRoleCB.Text == "" || advisorNameCB.Text == "" || projectCB.Text == "")
                        {
                            MessageBox.Show("Please fill all the fields");

                        }
                        else
                        {
                            // if(IsValueInComboBox(advisorNameCB.Text, advisorNameCB) && IsValueInComboBox(projectCB.Text, projectCB))
                            // {

                            if (!checkDuplicate())
                                {


                                    int id1 = getProjectId();
                                    int id2 = getAdvisorId();
                                    var con = Configuration.getInstance().getConnection();
                                    SqlCommand cmd = new SqlCommand("Insert into ProjectAdvisor values (@AdvisorId,@ProjectId,@AdvisorRole,@Date)", con);
                                    cmd.Parameters.AddWithValue("@ProjectId", id1);
                                    cmd.Parameters.AddWithValue("@AdvisorId", id2);
                                    if (advisorRoleCB.Text == "Main Advisor")
                                    {
                                        cmd.Parameters.AddWithValue("@AdvisorRole", 11);
                                    }
                                    else if (advisorRoleCB.Text == "Co-Advisor")
                                    {
                                        cmd.Parameters.AddWithValue("@AdvisorRole", 12);
                                    }
                                    else if (advisorRoleCB.Text == "Industry Advisor")
                                    {
                                        cmd.Parameters.AddWithValue("@AdvisorRole", 14);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please select a role");
                                    }
                                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Data has been added");
                                    showData();
                                    clearData();
                                }
                                else
                                {
                                    MessageBox.Show("Data already exists");
                                }
                            //}
                           // else
                           // {
                           //     MessageBox.Show("Enter Valid Data");
                           // }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Project already has 3 advisors");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        int getProjectId()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id from Project Where Title = @Title", con);
            cmd.Parameters.AddWithValue("@Title", projectCB.Text);
            cmd.ExecuteNonQuery();
            id = (int)cmd.ExecuteScalar();
            return id;
        }
        int getAdvisorId()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Advisor.Id from Advisor JOIN Person ON Advisor.Id = Person.Id Where FirstName + LastName = @Name", con);
            cmd.Parameters.AddWithValue("@Name", advisorNameCB.Text);
            cmd.ExecuteNonQuery();
            id = (int)cmd.ExecuteScalar();
            return id;
        }

        private void projectAdvisor_Load(object sender, EventArgs e)
        {
            getAdvisorIntoCombo();
            getProjectIntoCombo();
            showData();
        }
        void showData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ProjectAdvisor", con);
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

        }
        void clearData()
        {
            advisorRoleCB.Text = "";
            advisorNameCB.Text = "";
            projectCB.Text = "";
        }
        bool CheckRole()
        {
            int id = getProjectId();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) from ProjectAdvisor Where ProjectId = @Id And AdvisorRole = @Role ", con);
            if (advisorRoleCB.Text == "Main Advisor")
            {
                cmd.Parameters.AddWithValue("@Role", 11);
            }
            else if (advisorRoleCB.Text == "Co-Advisor")
            {
                cmd.Parameters.AddWithValue("@Role", 12);
            }
            else if (advisorRoleCB.Text == "Industry Advisor")
            {
                cmd.Parameters.AddWithValue("@Role", 14);
            }
            cmd.Parameters.AddWithValue("@Id", id);
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
        bool checkProjectRoles()
        {
            int id = getProjectId();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) from ProjectAdvisor Where ProjectId = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            if (Convert.ToInt32(cmd.ExecuteScalar()) < 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool checkDuplicate()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) from ProjectAdvisor Where AdvisorId = @AdvisorId AND ProjectId = @ProjectId AND AdvisorRole = @AdvisorRole", con);
            cmd.Parameters.AddWithValue("@AdvisorId", getAdvisorId());
            cmd.Parameters.AddWithValue("@ProjectId", getProjectId());
            cmd.Parameters.AddWithValue("@AdvisorRole", advisorRoleCB.Text);
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IsValueInComboBox(string value,  string c)
        {
            foreach (var item in c)
            {
                if (item.ToString() == value)
                {
                   
                    return true;
                }
            }
            return false;
        }
    }
}
