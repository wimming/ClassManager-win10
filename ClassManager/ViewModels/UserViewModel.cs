using ClassManager.Model;
using ClassManager.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
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
            User u = SingleService.Instance.getUser();
            u.Name = u.Name == null ? "（还没有设置）" : u.Name;
            u.Image = SingleService.Instance.getServerAddress() + (u.Image == null ? "null" : u.Image);
            foreach (var item in u.Relationships)
            {
                item.name = item.name == null ? "（还没有设置）" : item.name;
                item.image = SingleService.Instance.getServerAddress() + (item.image == null ? "null" : item.image);
            }

            User.DeepCopy(u);
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

                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public async void createOrganization(string account, string password)
        {
            Result result = await SingleService.Instance.registerOrganization(account, password);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("创建班级成功").ShowAsync();

                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public async Task<Result> searchOrganization(string account)
        {
            return await SingleService.Instance.searchOrganization(account);
        }

        public async void joinOrganizationWithPassword(string account, string password)
        {
            Result result = await SingleService.Instance.joinWithPassword(account, password);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("加入班级成功").ShowAsync();

                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }
        public async void joinOrganizationWithoutPassword(string account)
        {
            Result result = await SingleService.Instance.joinWithoutPassword(account);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("加入班级成功").ShowAsync();

                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public async void deleteOrganization(string account)
        {
            Result result = await SingleService.Instance.deleteOrganization(account);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("解散班级成功").ShowAsync();

                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public void logout()
        {
            User.DeepCopy(new User());
        }

        public async void complishHomework(string homeworkId, bool uncomplete)
        {
            Result result = await SingleService.Instance.complishHomework(homeworkId, uncomplete);
            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("homework标记为" + (uncomplete ? "未完成" : "完成")).ShowAsync();
                regetUser();
            }
            else {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        public void updateUser()
        {
            regetUser();
        }

        public async void uploadImage(Stream stream, string filename)
        {
            MultipartFormDataContent multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StreamContent(stream), "image", filename);

            Result result = await SingleService.Instance.userSetting(multipartContent);

            if (!result.error)
            {
                await new Windows.UI.Popups.MessageDialog("修改成功").ShowAsync();
                regetUser();
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
            }
        }

        // 私有方法 重新获取user数据
        private async void regetUser()
        {
            Result result = await SingleService.Instance.updateUser();

            if (!result.error)
            {
                User u = SingleService.Instance.getUser();
                u.Name = u.Name == null ? "（还没有设置）" : u.Name;
                u.Image = SingleService.Instance.getServerAddress() + (u.Image == null ? "null" : u.Image);
                foreach (var item in u.Relationships)
                {
                    item.name = item.name == null ? "（还没有设置）" : item.name;
                    item.image = SingleService.Instance.getServerAddress() + (item.image == null ? "null" : item.image);
                }

                User.DeepCopy(u);

            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("当前网络状况不佳").ShowAsync();
            }
        }
    }
}