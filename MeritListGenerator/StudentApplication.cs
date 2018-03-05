using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeritListGenerator
{
    class StudentApplication
    {
        public string name { get; set; }
        public string fatherName { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string MobileNumber { get; set; }
        public string resendentialContact { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public double matricMarks { get; set; }
        public double matricTotal { get; set; }
        public string matricRollNo { get; set; }
        public string matricBoardName { get; set; }
        public double matricPercentage { get; set; }
        public double fscMarks { get; set; }
        public double fscTotal { get; set; }
        public string fscRollNo { get; set; }
        public double fscPercentage { get; set; }
        public string fscBoardName { get; set; }
        public string testType { get; set; }
        public double entryTestTotalMarks { get; set; }
        public double entryTestMarks { get; set; }
        public double entrytestPercentage { get; set; }
        public double aggregate { get; set; }
        public int appRefNum { get; set; }
        public string category { get; set; }
        public List<string> preferenceList = new List<string>();

        public StudentApplication()
        {
            name = "";
            fatherName = "";
            age = 0;
            gender = "";
            email = "";
            MobileNumber = "";
            resendentialContact = "";
            matricMarks = 0;
            matricTotal = 0;
            fscMarks = 0;
            fscTotal = 0;
            testType = "";
            entryTestMarks = 0;
            aggregate = 0.0;
            appRefNum = 0;
            matricPercentage = 0.0;
            fscPercentage = 0.0;
            entrytestPercentage = 0.0;
        }
    }
}
