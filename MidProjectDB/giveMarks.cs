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
    public partial class giveMarks : Form
    {
        int groupId;
        public giveMarks(int id)
        {
            groupId = id;
            InitializeComponent();
        }

        private void giveMarks_Load(object sender, EventArgs e)
        {
            label3.Text = groupId.ToString(); 
            showDataIntoGrid();
            getEvaluationName();
        }
        void showDataIntoGrid()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from GroupEvaluation", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int obtainMarks;
                int.TryParse(obtainM.Text, out obtainMarks);
                if (comboBox1.Text != "" || obtainM.Text != "")
                {
                    if (!checkComboValue())
                    {
                        if (getTotalM() > obtainMarks)
                        {
                            var con1 = Configuration.getInstance().getConnection();
                            SqlCommand cmd2 = new SqlCommand("Insert into GroupEvaluation values (@GroupId,@EvaluationId, @ObtainM,@Date)", con1);
                            cmd2.Parameters.AddWithValue("@GroupId", label3.Text);
                            cmd2.Parameters.AddWithValue("@EvaluationId", getId());
                            cmd2.Parameters.AddWithValue("@ObtainM", obtainM.Text);
                            cmd2.Parameters.AddWithValue("@Date", DateTime.Now);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Data has been added");
                            showDataIntoGrid();
                        }
                        else
                        {
                            MessageBox.Show("Obtain Marks should be less than Total Marks");
                        }
                    }
                    else
                    {
                        MessageBox.Show("All Evaluation have done");
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
        void getEvaluationName()
        { 
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Name from Evaluation", con);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string value = reader.GetString(0);
                comboBox1.Items.Add(value);
            }
            reader.Close();
        }
        string getName()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Name from Evaluation where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", comboBox1.Text);
            object result = cmd.ExecuteScalar(); 
            return result.ToString();
        }
        int getId()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Id from Evaluation where Name = @Id", con);
            cmd.Parameters.AddWithValue("@Id", comboBox1.Text);
            object result = cmd.ExecuteScalar();
            return int.Parse(result.ToString());
        }

        private void clear_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        bool checkComboValue()
        {
            int count1, count2;
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select Count(*) From GroupEvaluation Join Evaluation on GroupEvaluation.EvaluationId = Evaluation.Id Where GroupId = @Id", con);
            cmd.Parameters.AddWithValue("@Id",label3.Text);
            count1 = (int)cmd.ExecuteScalar();
            var con1 = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Count(*) From Evaluation", con1);
            cmd1.Parameters.AddWithValue("@Id", label1.Text);
            count2 = (int)cmd1.ExecuteScalar();
            if(count1 == count2)
            {
               flag = true;
            }
            return flag;

        }
        int getTotalM()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select TotalMarks From Evaluation Where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", getId());
            return (int)cmd.ExecuteScalar();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
