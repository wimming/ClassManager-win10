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
	class OrganizationViewModel
	{
		static private OrganizationViewModel _instance;
		static public OrganizationViewModel Instance {
			get {
				if (_instance == null) {
					_instance = new OrganizationViewModel();
				}
				return _instance;
			}
		}

		private Organization organization;
		public Organization Organization {
			get { return organization; }
		}

		private OrganizationViewModel ()
		{
			organization = new Organization();
			organization.image = SingleService.Instance.getServerAddress() + "null";
		}

		public async void initialOVM (string account)
		{
			Result res = await SingleService.Instance.searchOrganizationDetail(account);
			if (!res.error) {
				_instance.Organization.DeepCopy(res.organization_data);
				_instance.Organization.image = SingleService.Instance.getServerAddress() + (_instance.Organization.image == null ? "null" : _instance.Organization.image);
				foreach (var item in _instance.Organization.members) {
					item.image = SingleService.Instance.getServerAddress() + (item.image == null ? "null" : item.image);
				}
			} else {
				await new Windows.UI.Popups.MessageDialog(res.message).ShowAsync();
			}
		}

		//public async void setPasswprd (string password)
		//{
		//	Dictionary<string, string> settingData = new Dictionary<string, string>();
		//	settingData.Add("password", password);

		//	Result result = await SingleService.Instance.userSetting(settingData);

		//	if (!result.error) {
		//		await new Windows.UI.Popups.MessageDialog("密码修改成功").ShowAsync();
		//	} else {
		//		await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
		//	}
		//}
		//public void setImage ()
		//{

		//}
		public async void setOrganizationData (string account, Dictionary<string, string> settingData)
		{
			Result result = await SingleService.Instance.organizationSetting(account, settingData);

			if (!result.error) {
				await new Windows.UI.Popups.MessageDialog("修改成功").ShowAsync();
				initialOVM(account);
			} else {
				await new Windows.UI.Popups.MessageDialog(result.message).ShowAsync();
			}
		}

		//public async void createOrganization (string account, string password)
		//{
		//	Result resilt = await SingleService.Instance.registerOrganization(account, password);

		//	if (!resilt.error) {
		//		await new Windows.UI.Popups.MessageDialog("创建班级成功").ShowAsync();

		//		Result result2 = await SingleService.Instance.updateUser();

		//		if (!result2.error) {
		//			User.DeepCopy(SingleService.Instance.getUser());
		//		} else {
		//			await new Windows.UI.Popups.MessageDialog(result2.message).ShowAsync();
		//		}
		//	} else {
		//		await new Windows.UI.Popups.MessageDialog(resilt.message).ShowAsync();
		//	}
		//}

		//public void logout ()
		//{
		//	_instance.User.DeepCopy(new User());
		//}
	}
}
