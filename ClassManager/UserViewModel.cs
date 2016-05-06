using ClassManager.Model;
using ClassManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ClassManager.ViewModels
{
    class UserViewModel
    {
        static private UserViewModel _instance;
        static public UserViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserViewModel();
                }
                return _instance;
            }
        }

        private User user;
        public User User
        {
            get { return user; }
        }

        private UserViewModel()
        {
            user = new User();
        }

        public void initialUVM()
        {
            _instance.User.DeepCopy(SingleService.Instance.getUser());
            _instance.User.image = SingleService.Instance.getServerAddress() + (_instance.User.image == null ? "null" : _instance.User.image);
        }

        public async void setPasswprd(string password)
        {
            Dictionary<string, string> settingData = new Dictionary<string, string>();
            settingData.Add("password", password);

            Result result = await SingleService.Instance.userSetting(settingData);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("密码修改成功").ShowAsync();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }
        public void setImage()
        {

        }
        public async void setUserData(Dictionary<string, string> settingData)
        {
            Result result = await SingleService.Instance.userSetting(settingData);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("修改成功").ShowAsync();

                Result result2 = await SingleService.Instance.updateUser();

                if (!result2.error)
                {
                    User.DeepCopy(SingleService.Instance.getUser());
                }
                else
                {
                    await new Windows.UI.Popups.MessageDialog(result2.message).ShowAsync();
                }
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public async void createOrganization(string account, string password)
        {
            Result resilt = await SingleService.Instance.registerOrganization(account, password);

            if (!resilt.error)
            {
                await new Windows.UI.Popups.MessageDialog("创建班级成功").ShowAsync();

                Result result2 = await SingleService.Instance.updateUser();

                if (!result2.error)
                {
                    User.DeepCopy(SingleService.Instance.getUser());
                }
                else
                {
                    await new Windows.UI.Popups.MessageDialog(result2.message).ShowAsync();
                }
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(resilt.message).ShowAsync();
            }
        }

        public void logout()
        {
            _instance.User.DeepCopy(new User());
        }
    }
}
