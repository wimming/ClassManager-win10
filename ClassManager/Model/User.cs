using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class User
    {
        public string _id { get; set; }
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
        public List<UserHomework> homeworks { get; set; }
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
            homeworks = new List<UserHomework>();
            relationships = new List<Relationship>();
        }
    }

    public class Relationship
    {
        public string _id { get; set; }
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

    public class UserHomework
    {

        public string _id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string join_on { get; set; }
        public string deadline { get; set; }
        public string account { get; set; }
        public bool unlook { get; set; }
        public bool uncomplish { get; set; }
        public UserHomework()
        {
            _id = "";
            name = "";
            content = "";
            join_on = "";
            deadline = "";
            account = "";
            unlook = true;
            uncomplish = true;
        }
    }
}
