using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class Result
    {
        public bool error { get; set; }
        public string message { get; set; }
        public User user_data { get; set; }
        public Organization organization_data { get; set; }
        public Result()
        {
            error = false;
            message = "";
            user_data = new User();
            organization_data = new Organization();
        }
    }
}
