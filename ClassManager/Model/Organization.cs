using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Model
{
    public class Organization : INotifyPropertyChanged
    {
        public string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("ID");
            }
        }

        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string account;
        public string Account
        {
            get { return account; }
            set
            {
                account = value;
                NotifyPropertyChanged("Account");
            }
        }

        public string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }

        public bool need_password;
        public bool NeedPassword
        {
            get { return need_password; }
            set
            {
                need_password = value;
                NotifyPropertyChanged("NeedPassword");
            }
        }

        public string image;
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        public string join_on;
        public string JoinOn
        {
            get { return join_on; }
            set
            {
                join_on = value;
                NotifyPropertyChanged("JoinOn");
            }
        }

        public ObservableCollection<User> members;
        public ObservableCollection<User> Members { get { return members; } }

        public ObservableCollection<Homework> homeworks;
        public ObservableCollection<Homework> Homeworks { get { return homeworks; } }

        public ObservableCollection<Notice> notices;
        public ObservableCollection<Notice> Notices { get { return notices; } }

        public ObservableCollection<Vote> votes;
        public ObservableCollection<Vote> Votes { get { return votes; } }

        public Organization()
        {
            _id = "";
            name = "";
            account = "";
            password = "";
            image = "";
            need_password = false;
            join_on = "";
            members = new ObservableCollection<User>();
            homeworks = new ObservableCollection<Homework>();
            notices = new ObservableCollection<Notice>();
            votes = new ObservableCollection<Vote>();
        }

        public void DeepCopy(Organization or)
        {
            ID = or.ID;
            Name = or.Name;
            Account = or.Account;
            Password = or.Password;
            Image = or.Image;
            NeedPassword = or.NeedPassword;
            JoinOn = or.JoinOn;

            Members.Clear();
            foreach (User item in or.Members)
            {
                Members.Add(item);
            }
            Homeworks.Clear();
            foreach (Homework item in or.Homeworks)
            {
                Homeworks.Add(item);
            }

            Notices.Clear();
            foreach (Notice item in or.Notices)
            {
                Notices.Add(item);
            }

            Votes.Clear();
            foreach (Vote item in or.Votes)
            {
                Votes.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
