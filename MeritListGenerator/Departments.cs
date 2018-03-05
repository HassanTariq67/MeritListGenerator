using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeritListGenerator
{
    class Departments
    {
        public string name { get; set; }
        public List<string> quota = new List<string>();
        public List<int> seats = new List<int>();
    }
}
