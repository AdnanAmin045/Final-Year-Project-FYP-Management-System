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
    public partial class createEvaluation : UserControl
    {
        public createEvaluation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int value1;
                bool isValue1Numeric = int.TryParse(weightage.Text, out value1);
                int value2;
                bool isValue2Numeric = int.TryParse(totalM.Text, out value2);
                if (comboBox1.Text == null || totalM.Text == "" || weightage.Text == "")
                {
                    MessageBox.Show("Fill All the Boxes");
                }
                else
                {
                    if (checkEvaluation())
                    {

                        if (value2 < 100)
                        {

                            if (value1 < 100)
                            {
                                var con = Configuration.getInstance().getConnection();
                                SqlCommand cmd = new SqlCommand("Insert into Evaluation values (@Name,@TotalM,@Weightage)", con);
                                cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
                                cmd.Parameters.AddWithValue("@TotalM", totalM.Text);
                                cmd.Parameters.AddWithValue("@Weightage", weightage.Text);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Data has been added");
                                showGridData();
                                comboBox1.Text = "";
                                totalM.Text = "";
                                weightage.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Weightage should be less than 100");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Marks should be less than 100");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Evaluation already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createEvaluation_Load(object sender, EventArgs e)
        {
            showGridData();
        }
        void showGridData()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * From Evaluation", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        bool checkEvaluation()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Name FROM Evaluation Where Name = @Name", con);
            cmd.Parameters.AddWithValue("@Name", comboBox1.Text);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                flag = true;
            }
            return flag;


        }
    }
}
