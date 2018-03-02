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
        public int matricMarks { get; set; }
        public int matricTotal { get; set; }
        public string matricRollNo { get; set; }
        public string matricBoardName { get; set; }
        public double matricPercentage { get; set; }
        public int fscMarks { get; set; }
        public int fscTotal { get; set; }
        public string fscRollNo { get; set; }
        public double fscPercentage { get; set; }
        public string fscBoardName { get; set; }
        public string testType { get; set; }
        public int entryTestTotalMarks { get; set; }
        public int entryTestMarks { get; set; }
        public double entrytestPercentage { get; set; }
        public double aggregate { get; set; }
        public string appRefNum { get; set; }
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
            appRefNum = "";
            matricPercentage = 0.0;
            fscPercentage = 0.0;
            entrytestPercentage = 0.0;
        }
    }
}
