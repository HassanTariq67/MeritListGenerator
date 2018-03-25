using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SautinSoft;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using System.Net;



namespace MeritListGenerator
{
    public partial class Form1 : Form
    {
        StudentApplication student = new StudentApplication();
        List<StudentApplication> studentsSelectedList = new List<StudentApplication>();
        Departments deptt = new Departments();
        List<StudentApplication> applications = new List<StudentApplication>();
        List<Departments> department = new List<Departments>();
        int applicationNumber = 1;
        public Form1()
        {
            InitializeComponent();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            
            student.name = NameTextBox.Text;
            student.fatherName = FatherNameTextBox.Text;
            student.age = int.Parse(AgeTextBox.Text);
            student.email = EmailTextBox.Text;
            if (MaleRadioButton.Checked)
                student.gender = "Male";
            else
                student.gender = "Female";

            student.MobileNumber = MobileNumberTextBox.Text;
            student.Address = Address.Text;
            student.resendentialContact = ContactTextBox.Text;
            student.Country = Country.Text;
            student.matricBoardName = MatricBoardname.Text;
            student.matricMarks = int.Parse(MatricMarks.Text);
            student.matricTotal = 1100;
            student.matricRollNo = MatricRollNOtextBox.Text;
            student.matricBoardName = MatricBoardname.Text; 
            student.matricPercentage = (student.matricMarks / student.matricTotal) * 100;
            student.fscMarks = int.Parse(FscMarks.Text);
            student.fscRollNo = FscRollNo.Text;
            student.fscBoardName = FscBoardName.Text;
            student.fscTotal = 1100;
            student.fscPercentage = (student.fscMarks / student.fscTotal) * 100;
            student.entryTestMarks = int.Parse(EtMarks.Text);
            student.entryTestTotalMarks = 400;
            student.entrytestPercentage = (student.entryTestMarks/student.entryTestTotalMarks)*100;
            student.aggregate = ((student.fscPercentage * 0.7) + (student.entrytestPercentage * 0.3));
            student.appRefNum = applicationNumber;
            student.category = CategoryTextBox.Text;
            applicationNumber++;
            applications.Add(student);
            student = new MeritListGenerator.StudentApplication();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            student.preferenceList.Add(PreferenceList.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deptt.name = DepartmentNameTextBox.Text;
            department.Add(deptt);
            deptt = new Departments();
        }

        private void QuotaButton_Click(object sender, EventArgs e)
        {
            deptt.quota.Add(QuotaNameTextBox.Text);
            deptt.seats.Add(int.Parse(QuotaSeatstextBox.Text));
            deptt.candidateSelected.Add(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            applications = applications.OrderByDescending(o => o.aggregate).ToList();
            ApllicationsTextBox.Text = String.Join(Environment.NewLine,applications.Select(s=>s.name));
            AggregatesTextBox.Text = String.Join(Environment.NewLine, applications.Select(s => s.aggregate));
            ApplicationNumberTextBox.Text = String.Join(Environment.NewLine, applications.Select(s => s.appRefNum));
        }
        
       

        private void Generate_MeritList()
        {
            int totalpreferences = 0;
            for (int i=0;i<applications.Count();i++)
            {
                totalpreferences += applications[i].preferenceList.Count();
            }
            //untill all students are selected 
            while (applications.Count!=0 && totalpreferences!=0)
            {
                //looping for all departments
                for (int i = 0; i < department.Count; i++)
                {
                    var depatnamename = department[i].name;
                    var depatquotalist = department[i].quota;
                    //looping for each category of that department
                    for (int j = 0; j < department[i].quota.Count(); j++)
                    {
                        var departquotaname = depatquotalist[j];   //Each Category for that department.
                        var seatlist = department[i].seats;
                        var noOfSeats = seatlist[j];     //seats for that category
                                                         //checking firstPreference,category for each application
                        for (int k = 0; k < applications.Count(); k++)
                        {
                            var listofstudents = applications[k];
                            var perferencelistofstudent = listofstudents.preferenceList;
                            var firstPreference = perferencelistofstudent[0];     //first preference of student.
                            //checking the first preference available seats and applied category of student. 
                            if (firstPreference == depatnamename && department[i].candidateSelected[j] < noOfSeats && listofstudents.category == departquotaname)
                            {
                                studentsSelectedList.Add(listofstudents);
                                department[i].candidateSelected[j]++;
                                totalpreferences -= listofstudents.preferenceList.Count();
                            }
                        }
                    }
                    //removing those students who hhave been selected 
                    for (int l = 0; l < studentsSelectedList.Count(); l++)
                    {
                        var removeStuents = studentsSelectedList[l];
                        applications.Remove(removeStuents);
                    }
                }
                    //removing first preferences of those students who were not selected.
                    for (int m = 0; m < applications.Count(); m++)
                    {
                        var unselectestudents = applications[m].preferenceList[0];
                        applications[m].preferenceList.Remove(unselectestudents);
                        totalpreferences--;
                    }
            }
            Generate_PDFs();
        }


        private void Generate_PDFs()
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "AppNo";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[1, 3] = "Department";
            xlWorkSheet.Cells[1, 4] = "Category";
            xlWorkSheet.Cells[1, 5] = "Aggregate";
            studentsSelectedList = studentsSelectedList.OrderByDescending(o => o.aggregate).ToList();
            for (int i = 0; i < studentsSelectedList.Count(); i++)
            {
                //Setting width of the Excel Coloumns.
                var studentinfo = studentsSelectedList[i];  
                xlWorkSheet.Columns[2].ColumnWidth = 18;
                xlWorkSheet.Columns[3].ColumnWidth = 21;
                xlWorkSheet.Columns[4].ColumnWidth = 18;
                xlWorkSheet.Columns[5].ColumnWidth = 18;
                
                //Setting information in Excel Workbook
                xlWorkSheet.Cells[i + 2, 1] = studentinfo.appRefNum;
                xlWorkSheet.Cells[i + 2, 2] = studentinfo.name;
                xlWorkSheet.Cells[i + 2, 3] = studentinfo.preferenceList[0];
                xlWorkSheet.Cells[i + 2, 4] = studentinfo.category;
                xlWorkSheet.Cells[i + 2, 5] = studentinfo.aggregate;
            }

            //Saving as PDF
            xlWorkBook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, "C:\\Users\\hassa\\Desktop\\MeritListGenerator\\MeritListGenerator\\MeritList.pdf");
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
        }

        private void SendEmailButton_Click(object sender, EventArgs e)
        {
            Send_Emails();
        }

        private void Send_Emails()
        {
            SmtpClient smpts = new SmtpClient();
            smpts.Host = "smtp.gmail.com";
            smpts.Port = 587;
            smpts.UseDefaultCredentials = false;
            smpts.EnableSsl = true;
            NetworkCredential cn = new NetworkCredential("2015cs67@gmail.com", "Hassan@73.76822cs67");
            smpts.Credentials = cn;

            for (int i = 0; i < studentsSelectedList.Count(); i++)
            {
                //mail subject and content.
                MailMessage mail = new MailMessage("2015cs67@gmail.com", studentsSelectedList[i].email);
                mail.Subject = "UET Undergrad Admissions";
                mail.Body = "Congratulations!!! You have been selected in "+ studentsSelectedList[i].preferenceList[0] +" department in UET Lahore.";
                smpts.Send(mail);
            }    
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            Generate_MeritList();
            //Send_Emails();
        }
    }
}
