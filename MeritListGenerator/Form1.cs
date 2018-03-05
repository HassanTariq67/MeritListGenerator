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
            student.resendentialContact = ResedentialContactTextBox.Text;
            student.Country = CountryTextBox.Text;
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
            student.aggregate = ((student.fscPercentage) * 0.7 + (student.entrytestPercentage) * 0.3);
            student.appRefNum = applicationNumber;
            student.category = "A";
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            applications = applications.OrderByDescending(o => o.aggregate).ToList();
            ApllicationsTextBox.Text = String.Join(Environment.NewLine,applications.Select(s=>s.name));
            AggregatesTextBox.Text = String.Join(Environment.NewLine, applications.Select(s => s.aggregate));
            ApplicationNumberTextBox.Text = String.Join(Environment.NewLine, applications.Select(s => s.appRefNum));
            
        }
        
        private void GenerateListButton_Click(object sender, EventArgs e)
        {
            //looping for all departments
            for (int i = 0; i < department.Count; i++)
            {
                var selectedcandidates = 0;
                var depatnamename = department[i].name;
                var depatquotalist = department[i].quota;
                //looping for each category of that department
                for (int j = 0; j < department[i].quota.Count(); j++)
                {
                    var departquotaname = depatquotalist[j];   //Each Category for that department.
                    var seatlist = department[j].seats;
                    var noOfSeats = seatlist[j];     //seats for that category
                    //checking firstPreference,category for each application
                    for (int k = 0; k < applications.Count(); k++)
                    {
                        var listofstudents = applications[k];
                        var perferencelistofstudent = listofstudents.preferenceList;
                        var firstPreference = perferencelistofstudent[0];     //first preference of student.
                        //also checking if seats in that category are still available
                        if (firstPreference == depatnamename && selectedcandidates < noOfSeats && listofstudents.category == departquotaname)
                        {
                            studentsSelectedList.Add(listofstudents);
                            applications.Remove(student);
                            selectedcandidates++;
                        }
                    }
                }
                //removing first preferences of those students who were not selected.
                for (int m = 0; m < applications.Count(); m++)
                {
                    var unselectestudents = applications[m];
                    unselectestudents.preferenceList.Remove(depatnamename);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < department.Count(); i++)
            {
                NumberingTextBox.Text = i.ToString();
                departmentsTextBox.Text = String.Join(Environment.NewLine, department[i].name);
                NoOfCategoriesTextBox.Text = String.Join(Environment.NewLine,(department[i].quota.Count()).ToString());
                TotalSetasTextBox.Text =String.Join(Environment.NewLine,(department[i].seats.Sum().ToString()));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
