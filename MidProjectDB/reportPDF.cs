using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CLO_Management_System.PDF
{
    public static class PDFGenerator
    {
        public static void CreatePDF(DataTable dt, string fileName, string title)
        {
            try
            {
                fileName = $"D:\\University Material\\4th Semester\\Database System\\MidProjectDB\\PDF Report{fileName} At {DateTime.Now.ToString("yyyy-MM-dd-HH=mm-ss")}.pdf";

                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                document.Open();
                AddTitle(document, title);
                AddDataTable(document, dt);
                document.Close();

                MessageBox.Show("PDF created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in creating PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AddTitle(Document document, string title)
        {
            iTextSharp.text.Paragraph titleParagraph = new iTextSharp.text.Paragraph(title, FontFactory.GetFont(FontFactory.HELVETICA, 16, iTextSharp.text.Font.BOLD));
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            titleParagraph.SpacingAfter = 10f;
            document.Add(titleParagraph);
        }

        private static void AddDataTable(Document document, DataTable dt)
        {
            iTextSharp.text.Font font = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 10);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
                widths[i] = 4f;

            table.SetWidths(widths);
            table.WidthPercentage = 100;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font));
            }

            foreach (DataRow r in dt.Rows)
            {
                for (int h = 0; h < dt.Columns.Count; h++)
                {
                    table.AddCell(new Phrase(r[h].ToString(), font));
                }
            }

            document.Add(table);
        }
    }
}