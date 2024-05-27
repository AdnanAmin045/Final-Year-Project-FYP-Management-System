using CRUD_Operations;
using CrystalDecisions.ReportSource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MidProjectDB
{
    public partial class advisor : UserControl

    { 
        string firstName;
        string lastName;
        string contact;
        string email;
        string date;
        int gender;
        string designation;
       
        public advisor()
        {
            InitializeComponent();
        }

        private void clear_Click(object sender, EventArgs e)
        {
           clearBoxes();
        }

        private void maleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (maleCheck.Checked)
            {
                femaleCheck.Checked = false;
            }
        }

        private void femaleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (femaleCheck.Checked)
            {
                maleCheck.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (salarytxt.Text == "" || Fnametxt.Text == "" || Lnametxt.Text == "" || contacttxt.Text == "" || emailtxt.Text == "" || salarytxt.Text == "" || desginationCB.Text == "")
                {
                    MessageBox.Show("Fill AlL the boxes");
                }
                else if (!checkDuplication())
                {
                    if (contacttxt.Text.Length == 11 && IsNumeric(contacttxt.Text))
                    {
                        if (IsValidEmail(emailtxt.Text))
                        {
                            if ((maleCheck.Checked || femaleCheck.Checked))
                            {
                                var con = Configuration.getInstance().getConnection();
                                SqlCommand cmd = new SqlCommand("Insert into Person values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", con);
                                cmd.Parameters.AddWithValue("@FirstName", (Fnametxt.Text));
                                cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
                                cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
                                cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
                                cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateTimePicker1.Text));
                                if (maleCheck.Checked)
                                {
                                    cmd.Parameters.AddWithValue("@Gender", 1);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Gender", 2);
                                }
                                cmd.ExecuteNonQuery();
                                var con1 = Configuration.getInstance().getConnection();
                                SqlCommand cmd2 = new SqlCommand("Insert into Advisor values (@Id,@Designation, @Salary)", con1);
                                cmd2.Parameters.AddWithValue("@Id", GetMaxPersonId());
                                if (desginationCB.Text == "Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 6);
                                }
                                else if (desginationCB.Text == "Associate Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 7);
                                }
                                else if (desginationCB.Text == "Assistant Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 8);
                                }
                                else if (desginationCB.Text == "Lecturer")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 9);
                                }
                                else if (desginationCB.Text == "Industry Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 10);
                                }
                                cmd2.Parameters.AddWithValue("@Salary", salarytxt.Text);
                                cmd2.ExecuteNonQuery();
                                MessageBox.Show("Data has been added");
                                loadData();
                                clearBoxes();
                            }
                            else
                            {
                                MessageBox.Show("Select the Gender");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter the valid email");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter the valid contact number");
                    }
                }
                else
                {
                    MessageBox.Show("Data already exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool checkDuplication()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*)from Person Where FirstName = @FirstName AND LastName = @LastName AND Contact = @Contact AND Email = @Email AND DateOfBirth = @DateOfBirth AND Gender = @Gender", con);
            cmd.Parameters.AddWithValue("@FirstName", (Fnametxt.Text));
            cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
            cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
            cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
            cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Text);
            if (maleCheck.Checked)
            {
                cmd.Parameters.AddWithValue("@Gender", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Gender", 2);
            }
            cmd.ExecuteNonQuery();
            if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                flag = true;
            }
            return flag;
                      
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                maleCheck.Checked = false;
                femaleCheck.Checked = false;
                Fnametxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                Lnametxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                contacttxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                emailtxt.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                string dateString = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                DateTime dateValue;
                if (DateTime.TryParseExact(dateString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    dateTimePicker1.Value = dateValue;
                }
                else
                {
                    MessageBox.Show("Invalid date format!");
                }
                string value = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                if (value == "Male")
                {
                    maleCheck.Checked = true;
                }
                else
                {
                    femaleCheck.Checked = true;
                }
                desginationCB.Text = dataGridView1.SelectedRows[0].Cells[6].Value?.ToString();

                salarytxt.Text = dataGridView1.SelectedRows[0].Cells[7].Value?.ToString();
                firstName = Fnametxt.Text;
                lastName = Lnametxt.Text;
                contact = contacttxt.Text;
                email = emailtxt.Text;
                date = dateTimePicker1.Value.ToString("dd-MMM-yyyy");
                if (maleCheck.Checked)
                {
                    gender = 1;
                }
                else
                {
                    gender = 2;
                }
            }
        }
        private void advisor_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select FirstName,LastName,Contact, Email,FORMAT(DateOfBirth, 'dd-MMM-yyyy') AS DateOfBirth,CASE WHEN Gender = 1 THEN 'Male' ELSE 'Female' END AS Gender,Lookup.Value as Designation,Salary  from Advisor JOIN Person ON Advisor.Id = Person.Id Join Lookup on Designation = LookUp.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        int GetMaxPersonId()
        {
            string selectMaxIdQuery = "SELECT Top 1 Id FROM Person ORDER BY Id DESC ;";
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(selectMaxIdQuery, con);
            return (int)command.ExecuteScalar();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (salarytxt.Text == "" || Fnametxt.Text == "" || Lnametxt.Text == "" || contacttxt.Text == "" || emailtxt.Text == "" || salarytxt.Text == "" || desginationCB.Text == "")
                {
                    MessageBox.Show("Fill AlL the boxes");
                }
                else if (!(firstName == null || lastName == null || email == null || contact == null))
                {

                    if (checkContactNo(contacttxt.Text) && IsNumeric(contacttxt.Text))
                    {
                        if (IsValidEmail(emailtxt.Text))
                        {

                            if ((maleCheck.Checked || femaleCheck.Checked))
                            {
                                var con = Configuration.getInstance().getConnection();
                                int id = getPersonId();
                                SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE Id = @Id", con);
                                cmd.Parameters.AddWithValue("@FirstName", (Fnametxt.Text));
                                cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
                                cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
                                cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
                                cmd.Parameters.AddWithValue("@Id", id);
                                cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateTimePicker1.Text));
                                if (maleCheck.Checked)
                                {
                                    cmd.Parameters.AddWithValue("@Gender", 1);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Gender", 2);
                                }
                                cmd.ExecuteNonQuery();
                                var con2 = Configuration.getInstance().getConnection();
                                SqlCommand cmd2 = new SqlCommand("UPDATE Advisor SET Id = @Id, Designation = @Designation, Salary = @Salary", con2);
                                cmd2.Parameters.AddWithValue("@Id", id);
                                if (desginationCB.Text == "Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 6);
                                }
                                else if (desginationCB.Text == "Associate Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 7);
                                }
                                else if (desginationCB.Text == "Assistant Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 8);
                                }
                                else if (desginationCB.Text == "Lecturer")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 9);
                                }
                                else if (desginationCB.Text == "Industry Professor")
                                {
                                    cmd2.Parameters.AddWithValue("@Designation", 10);
                                }
                                cmd2.Parameters.AddWithValue("@Salary", salarytxt.Text);
                                MessageBox.Show("Updated Successfully");
                                loadData();
                                clearBoxes();
                            }
                            else
                            {
                                MessageBox.Show("Select the Gender");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter the valid email");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Enter the valid contact number");
                    }
                }
                else
                {
                    MessageBox.Show("Select the Existing Data for Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int getPersonId()
        {
            int id = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Person WHERE  Firstname = @FirstName AND LastName = @LastName AND Contact = @Contact AND Email = @Email AND Gender = @Gender", con);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Contact", contact);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@DateOfBirth", date);
            cmd.Parameters.AddWithValue("@Gender", gender);
            id = (int)cmd.ExecuteScalar();
            return id;
        }
        void clearBoxes()
        {
            salarytxt.Text = "";
            desginationCB.Text = "";
            Fnametxt.Text = "";
            Lnametxt.Text = "";
            contacttxt.Text = "";
            emailtxt.Text = "";
            maleCheck.Checked = false;
            femaleCheck.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
        }
        bool IsValidEmail(string email)
        {
            string trimmedText = email.Replace(" ", "");
            string pattern = @"^[a-zA-Z0-9._%+-]+@(?:gmail\.com|student\.uet\.edu\.pk)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(trimmedText);
        }
        bool IsNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        bool checkContactNo(string text)
        {
            string trimmedText = text.Replace(" ", "");
            if (trimmedText.Length == 11)
            {
               return true;
            }
            else
            {
                return false;
            }
        }

        private void desginationCB_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) 
            {
                dateTimePicker1.CustomFormat = " ";
            }
        }
    }
}
