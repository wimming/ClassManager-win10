using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class Organization
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public bool need_password { get; set; }
        public string image { get; set; }
        public string join_on { get; set; }
        public List<User> members { get; set; }
        public List<Homework> homeworks { get; set; }
        public List<Notice> notices { get; set; }
        public List<Vote> votes { get; set; }


        public Organization()
        {
            _id = "";
            name = "";
            account = "";
            password = "";
            image = "";
            need_password = false;
            join_on = "";
            members = new List<User>();
            homeworks = new List<Homework>();
            notices = new List<Notice>();
            votes = new List<Vote>();
        }
    }
}
