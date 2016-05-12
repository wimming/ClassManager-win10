using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Controls
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class homeWorkAndNoticesCtl : Page
	{
		int whatControls = -1;
		public homeWorkAndNoticesCtl (Dictionary<string, string> type)
		{
			this.InitializeComponent();
			if (type["type"] == "homework") {
				rb3.Visibility = Visibility.Collapsed;
				notice_text.Visibility = Visibility.Collapsed;
				if (type["uncomplete"] == "True") {
					rb5.Visibility = Visibility.Collapsed;
					rb4.Visibility = Visibility.Visible;
				} else {
					rb4.Visibility = Visibility.Collapsed;
					rb5.Visibility = Visibility.Visible;
				}
				if (type["unlook"] == "True") {
					homework_text.Visibility = Visibility.Collapsed;
					rb2.Visibility = Visibility.Visible;
				} else {
					rb2.Visibility = Visibility.Collapsed;
					homework_text.Visibility = Visibility.Visible;
				}
				if (type["hasPowful"] != "True") {
					rb1.Visibility = Visibility.Collapsed;
				} else {
					rb1.Visibility = Visibility.Visible;
				}
			} else {
				rb2.Visibility = Visibility.Collapsed;
				rb4.Visibility = Visibility.Collapsed;
				rb5.Visibility = Visibility.Collapsed;
				homework_text.Visibility = Visibility.Collapsed;
				if (type["unlook"] == "True") {
					notice_text.Visibility = Visibility.Collapsed;
					rb3.Visibility = Visibility.Visible;
				} else {
					rb3.Visibility = Visibility.Collapsed;
					notice_text.Visibility = Visibility.Visible;
				}
				if (type["hasPowful"] != "True") {
					rb1.Visibility = Visibility.Collapsed;
				} else {
					rb1.Visibility = Visibility.Visible;
				}
			}
		}

		public int getwhatControls ()
		{
			return whatControls;
		}
		void Controls (object sender, RoutedEventArgs e)
		{
			RadioButton li = (sender as RadioButton);
			if (li.Name == "rb1") {
				whatControls = 0;
			} else if (li.Name == "rb2") {
				whatControls = 1;
			} else if (li.Name == "rb3") {
				whatControls = 2;
			} else if (li.Name == "rb4") {
				whatControls = 3;
			} else if (li.Name == "rb5") {
				whatControls = 4;
			} else if (li.Name == "rb6") {
				whatControls = 5;
			}
		}
	}
}
