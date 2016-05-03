using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class Vote
    {
        public string _id { get; }
        public string name { get; set; }
        public string content { get; set; }
        public string join_on { get; set; }
        public string deadline { get; set; }
        public List<Option> options { get; set; }
        public List<User> unvotes { get; set; }
        public Vote()
        {
            _id = "";
            name = "";
            content = "";
            join_on = "";
            deadline = "";
            options = new List<Option>();
            unvotes = new List<User>();
        }
        public class Option
        {
            public string _id { get; }
            public string name { get; set; }
            public int votes { get; set; }
            public List<User> supporters { get; set; }
            public Option()
            {
                _id = "";
                name = "";
                votes = 0;
                supporters = new List<User>();
            }
        }
    }
}
