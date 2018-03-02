using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeritListGenerator
{
    public partial class Form1 : Form
    {
        StudentApplication student = new StudentApplication();
        Departments deptt = new Departments();
        List<StudentApplication> applications = new List<StudentApplication>();
        List<Departments> department = new List<Departments>();
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
            student.resendentialContact = ResedentialContactTextBox.Text;
            student.Country = CountryTextBox.Text;
            student.matricBoardName = MatricBoardNameTextBox.Text;
            student.matricMarks = int.Parse(MatricMarksTextBox.Text);
            student.matricTotal = 1100;
            student.matricRollNo = MatricRollNOtextBox.Text;
            student.matricPercentage = (student.matricMarks / student.matricTotal) * 100;
            student.fscMarks = int.Parse(FscMarksTextBox.Text);
            student.fscRollNo = FscRollNoTextBox.Text;
            student.fscBoardName = FscBoardNameTextBox.Text;
            student.fscTotal = 1100;
            student.fscPercentage = (student.fscMarks / student.fscTotal) * 100;
            student.entryTestMarks = int.Parse(ETMarksTextBox.Text);
            student.entryTestTotalMarks = 400;
            student.aggregate = ((student.fscPercentage) * 0.7 + (student.entrytestPercentage) * 0.3);
            applications.Add(student);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            student.preferenceList.Add(PreferenceListTextBox.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deptt.name = DepartmentNameTextBox.Text;
            department.Add(deptt);
        }

        private void QuotaButton_Click(object sender, EventArgs e)
        {
            deptt.Quota.Add(QuotaNameTextBox.Text);
            deptt.seats.Add(int.Parse(QuotaSeatstextBox.Text));
        }
    }
}
