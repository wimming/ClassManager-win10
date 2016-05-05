using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class Homework
    {
       
        public string _id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string join_on { get; set; }
        public string deadline { get; set; }
        public bool unlook { get; set; }
        public bool uncomplish { get; set; }
        public Homework()
        {
            _id = "";
            name = "";
            content = "";
            join_on = "";
            deadline = "";
            unlook = true;
            uncomplish = true;
        }
    }
}
