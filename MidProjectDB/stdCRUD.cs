using CRUD_Operations;
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
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MidProjectDB
{
    public partial class stdCRUD : UserControl
    {
        private const string V = " ";
        string regNo;
        string firstName;
        string lastName;
        string contact;
        string email;
        DateTime date;
        int gender;
        public stdCRUD()
        {
            InitializeComponent();
        }

        private void stdCRUD_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void clear_Click(object sender, EventArgs e)
        {
            clearBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (regNotxt.Text == "" || Fnametxt.Text == "" || Lnametxt.Text == "" || contacttxt.Text == "" || emailtxt.Text == "")
                {
                    MessageBox.Show("Fill AlL the boxes");
                }
                else if (!checkDuplication())
                {
                    if ((maleCheck.Checked || femaleCheck.Checked))
                    {
                        if (checkContactNo(contacttxt.Text) && IsNumeric(contacttxt.Text))
                        {
                            if (!checkRegNo())
                            {

                                if (IsValidRegistrationNumber(regNotxt.Text))
                                {
                                    if (IsValidEmail(emailtxt.Text))
                                    {
                                        var con = Configuration.getInstance().getConnection();
                                        SqlCommand cmd = new SqlCommand("Insert into Person values (@FirstName, @LastName, @Contact, @Email, @DateOfBirth, @Gender)", con);
                                        cmd.Parameters.AddWithValue("@FirstName", (Fnametxt.Text));
                                        cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
                                        cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
                                        cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
                                        cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dobPicker.Text));
                                        if (maleCheck.Checked)
                                        {
                                            cmd.Parameters.AddWithValue("@Gender", 1);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Gender", 2);
                                        }
                                        cmd.ExecuteNonQuery();

                                        int Id = GetMaxPersonId();

                                        var con2 = Configuration.getInstance().getConnection();
                                        SqlCommand cmd2 = new SqlCommand("Insert into Student values (@Id, @RegisterationNo)", con2);
                                        cmd2.Parameters.AddWithValue("@Id", Id);
                                        cmd2.Parameters.AddWithValue("@RegisterationNo", regNotxt.Text);
                                        cmd2.ExecuteNonQuery();
                                        MessageBox.Show("Successfully saved");
                                        loadData();
                                        clearBoxes();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter the valid email");
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("Wrong Format of Registration No");
                                }
                            }
                            else
                            {
                                MessageBox.Show(" RegistrationNo Already Exist");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter the valid contact number");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select the Gender");
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

        private void maleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(maleCheck.Checked)
            {
                femaleCheck.Checked = false;
            }
 
        }

        private void femaleCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(femaleCheck.Checked)
            {
                maleCheck.Checked = false;
            }
        }
        int GetMaxPersonId()
        {
            string selectMaxIdQuery = "SELECT Top 1 Id FROM Person ORDER BY Id DESC ;";
            var con = Configuration.getInstance().getConnection();
            SqlCommand command = new SqlCommand(selectMaxIdQuery, con);
            return (int)command.ExecuteScalar();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM Student JOIN Person ON Student.Id = Person.Id  WHERE RegistrationNo = @RegistrationNo", con);
            cmd.Parameters.AddWithValue("@RegistrationNo", (regNotxt.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Deleted");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                maleCheck.Checked = false;
                femaleCheck.Checked = false;
                regNotxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                Fnametxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                Lnametxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                contacttxt.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                emailtxt.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString();
                string dateString = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                DateTime dateValue;
                if (DateTime.TryParseExact(dateString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    dobPicker.Value = dateValue;
                    Console.WriteLine(dateValue);
                }
                else
                {
                    MessageBox.Show("Invalid date format!");
                }
                string value = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                if (value == "Male")
                {
                    maleCheck.Checked = true;
                }
                else
                {
                    femaleCheck.Checked = true;
                }
                regNo = regNotxt.Text;
                firstName = Fnametxt.Text;
                lastName = Lnametxt.Text;
                contact = contacttxt.Text;
                email = emailtxt.Text;
                date = DateTime.Parse(dobPicker.Text);
                if(maleCheck.Checked)
                {
                    gender = 1;
                }
                else
                {
                    gender = 2;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try 
            { 
                int number;
                bool isValidNumber1 = int.TryParse(contacttxt.Text, out number);
                if (regNotxt.Text == "" || Fnametxt.Text == "" || Lnametxt.Text == "" || contacttxt.Text == "" || emailtxt.Text == "" || (maleCheck.Checked && femaleCheck.Checked) )
                {
                    MessageBox.Show("Fill AlL the boxes");
                }
                else if (!(regNo == null || firstName == null || lastName == null))
                {
                    if (regNo == regNotxt.Text.ToString())
                    { 

                         if ((maleCheck.Checked || femaleCheck.Checked))
                         {

                            if (checkContactNo(contacttxt.Text) && IsNumeric(contacttxt.Text))
                            {
                                if(IsValidEmail(emailtxt.Text))
                                {
                                    var con = Configuration.getInstance().getConnection();
                                    int id = getPersonId();
                                    SqlCommand cmd = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE Id = @Id", con);
                                    cmd.Parameters.AddWithValue("@FirstName", (Fnametxt.Text));
                                    cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
                                    cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
                                    cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
                                    cmd.Parameters.AddWithValue("@Id", id);
                                    cmd.Parameters.AddWithValue("@DateOfBirth",Convert.ToDateTime(dobPicker.Text));
                                    if(maleCheck.Checked)
                                    {
                                        cmd.Parameters.AddWithValue("@Gender", 1);
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue("@Gender", 2);
                                    }
                                    cmd.ExecuteNonQuery();
                                    var con2 = Configuration.getInstance().getConnection();
                                    SqlCommand cmd2 = new SqlCommand("UPDATE Student SET Id = @Id, RegistrationNo = @RegistrationNo", con2);
                                    cmd2.Parameters.AddWithValue("@Id", id);
                                    cmd2.Parameters.AddWithValue("@RegistrationNo", regNotxt.Text);
                                    MessageBox.Show("Updated Successfully");
                                    clearBoxes();            
                                    loadData();
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
                            MessageBox.Show("Select the Gender");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot change the registration number ");
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
        bool checkDuplication()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Person WHERE FirstName = @FirstName AND LastName = @LastName AND Contact = @Contact AND Email = @Email AND DateOfBirth = @DateOfBirth", con);
            cmd.Parameters.AddWithValue("@FirstName", Fnametxt.Text);
            cmd.Parameters.AddWithValue("@LastName", Lnametxt.Text);
            cmd.Parameters.AddWithValue("@Contact", contacttxt.Text);
            cmd.Parameters.AddWithValue("@Email", emailtxt.Text);
            cmd.Parameters.AddWithValue("@DateOfBirth", dobPicker.Value.Date);
            if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                flag = true;
            }
            return flag;
        }
        void loadData() 
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select RegistrationNo ,FirstName,LastName,Contact, Email,FORMAT(DateOfBirth, 'dd-MMM-yyyy') AS DateOfBirth, CASE WHEN Gender = 1 THEN 'Male' ELSE 'Female' END AS Gender from Student JOIN Person ON Student.Id = Person.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void clearBoxes()
        {
            regNotxt.Text = "";
            Fnametxt.Text = "";
            Lnametxt.Text = "";
            contacttxt.Text = "";
            emailtxt.Text = "";
            maleCheck.Checked = false;
            femaleCheck.Checked = false;
            dobPicker.Text = " ";
        }
        bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@(?:gmail\.com|student\.uet\.edu\.pk)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        bool checkRegNo()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) From Student Where RegistrationNo = @Id", con);
            cmd.Parameters.AddWithValue("@Id", regNotxt.Text);
            cmd.ExecuteNonQuery();
            if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool IsValidRegistrationNumber(string regNo)
        {
            string trimmedText = regNo.Replace(" ", "");
            string pattern = @"^\d{4}-[A-Z]{2}-\d{2,3}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(trimmedText);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void stdCRUD_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void dobPicker_ValueChanged(object sender, EventArgs e)
        {
            dobPicker.CustomFormat = "dd-MMM-yyyy";
        }

        private void dobPicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dobPicker.CustomFormat = " ";
            }
        }
    }
}

