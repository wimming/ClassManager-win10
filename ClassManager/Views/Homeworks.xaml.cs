using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ClassManager.Model;
using ClassManager.ViewModels;
using Windows.UI.Xaml;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Homeworks : Page
	{
		private OrganizationViewModel OVM;
		private SelectItemOfHomeworks SelectedItem;
		public Homeworks ()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			OVM.initialOVM((string)e.Parameter);
		}


	}

	public class SelectItemOfHomeworks
	{
		public SelectItemOfHomeworks ()
		{
			Users = new ObservableCollection<User>();
		}
		private ObservableCollection<User> users;
		public ObservableCollection<User> Users {
			get {
				return users;
			}
			set {
				foreach (User item in value) {
					users.Add(item);
				}
			}
		}
	}
}
