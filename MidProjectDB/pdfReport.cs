using CLO_Management_System.PDF;
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
    public partial class pdfReport : UserControl
    {
        public pdfReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT Id FROM Person WHERE  Firstname = @FirstName AND LastName = @LastName AND Contact = @Contact AND Email = @Email AND Gender = @Gender", con);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT PA.ProjectId,MAX(P.Title) AS Title, MAX(P.Description) AS Description,MAX(CASE WHEN PA.AdvisorRole = 11 THEN CONCAT(Pers.FirstName, ' ', Pers.LastName) END) AS MainAdvisor,MAX(CASE WHEN PA.AdvisorRole = 12 THEN CONCAT(Pers.FirstName, ' ', Pers.LastName) END) AS CoAdvisor,MAX(CASE WHEN PA.AdvisorRole = 14 THEN CONCAT(Pers.FirstName, ' ', Pers.LastName) END) AS IndustryAdvisor FROM  ProjectAdvisor PA INNER JOIN Advisor A ON PA.AdvisorId = A.Id JOIN   Project P ON P.Id = PA.ProjectId JOIN Person Pers ON Pers.Id = A.Id WHERE LEFT(P.Title,1) <> '-' AND LEFT(Pers.FirstName,1) <> '-' GROUP BY PA.ProjectId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PDFGenerator.CreatePDF(dt, "Report1", "List of projects along with advisory board and list of students");
        }

        private void pdfReport_Load(object sender, EventArgs e)
        {

        }

        private void report2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT GroupStudent.GroupId,Person.FirstName+' '+Person.LastName AS StudentName,Project.Title AS ProjectTitle,Evaluation.Name AS EvaluationName,Evaluation.TotalMarks,GroupEvaluation.ObtainedMarks FROM GroupEvaluation JOIN GroupStudent ON GroupEvaluation.GroupId=GroupStudent.GroupId JOIN Evaluation ON GroupEvaluation.EvaluationId=Evaluation.Id JOIN GroupProject ON GroupProject.GroupId=GroupEvaluation.GroupId JOIN Project ON GroupProject.ProjectId=Project.Id JOIN Person ON Person.Id=GroupStudent.StudentId WHERE LEFT(Person.FirstName,1) <> '-' AND LEFT(Person.FirstName,1) <> '^' AND LEFT(Evaluation.Name,1) <> '-' ORDER BY Person.FirstName+' '+Person.LastName;", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PDFGenerator.CreatePDF(dt, "Report2", "Marks sheet of projects that shows the marks in each evaluation against each student and project and any \r\nother reports that you can help the committee to streamline the process.");
        }

        private void report3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT   p.Title AS ProjectTitle,  COUNT(DISTINCT gs.StudentID) AS TotalStudentsEnrolled FROM Project p INNER JOIN GroupProject gp ON p.Id = gp.ProjectId INNER JOIN GroupStudent gs ON gp.GroupId = gs.GroupId GROUP BY p.Title;", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PDFGenerator.CreatePDF(dt, "Report3", "Student Enrollment by Project");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select GroupId,RegistrationNo,FirstName,LastName From Person Join (Select * FROM GroupStudent as T1 Join Student On StudentId = Student.Id) as T1 on Person.Id = T1.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PDFGenerator.CreatePDF(dt, "Report4", "Student Group Information With Detail");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select ProjectId,AdvisorRole,FirstName,LastName,Designation,Salary\r\nFrom Person Join (Select *\r\nFrom ProjectAdvisor Join Advisor on ProjectAdvisor.AdvisorId = Advisor.Id) as T1 on Person.Id = T1.AdvisorId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            PDFGenerator.CreatePDF(dt, "Report5", "Advisor Detail With Project");
        }
    }
}
