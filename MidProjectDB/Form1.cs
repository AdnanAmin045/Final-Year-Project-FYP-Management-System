using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidProjectDB
{
    public partial class home : Form
    {
        public home()
        {
            
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void home_Load(object sender, EventArgs e)
        {
            
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void addStd1_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            stdCRUD panel1 = new stdCRUD();
            addUserControl(panel1);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            advisor panel1 = new advisor();
            addUserControl(panel1);
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
           project panel1 = new project();
            addUserControl(panel1);
        }

        private void project1_Load(object sender, EventArgs e)
        {

        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            projectAdvisor panel1 = new projectAdvisor();
            addUserControl(panel1);
        }

        private void label7_Click(object sender, EventArgs e)
        {
           addGroup panel1 = new addGroup();
           addUserControl(panel1);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            createEvaluation panel1 = new createEvaluation();
            addUserControl(panel1);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            markEvaluation panel1 = new markEvaluation();
            addUserControl(panel1);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            pdfReport panel1 = new pdfReport();
            addUserControl(panel1);
        }
    }
}
