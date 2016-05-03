using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class User
    {
        public string _id { get; }
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string student_id { get; set; }
        public string gender { get; set; }
        public string image { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string qq { get; set; }
        public string wechat { get; set; }
        public List<Homework> homeworks { get; set; }
        public List<Relationship> relationships { get; set; }

        public User()
        {
            _id = "";
            name = "";
            account = "";
            password = "";
            student_id = "";
            gender = "female";
            image = "";
            email = "";
            phone = "";
            qq = "";
            wechat = "";
            homeworks = new List<Homework>();
            relationships = new List<Relationship>();
        }


        public class Relationship
        {
            public string _id { get; }
            public string name { get; set; }
            public string account { get; set; }
            public string image { get; set; }
            public string position { get; set; }
            public Relationship()
            {
                _id = "";
                name = "";
                account = "";
                image = "";
                position = "";
            }
        }
    }
}
