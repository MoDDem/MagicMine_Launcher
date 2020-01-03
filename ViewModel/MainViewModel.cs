using MagicMine_Launcher.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MagicMine_Launcher.ViewModel {
	class MainViewModel : BaseVM {
		public SettingsViewModel SettingsVM { get; set; }
		public NavigationViewModel NavigationVM { get; set; }
		public UserViewModel UserVM { get; set; }

		public MainViewModel() {
			SettingsVM = new SettingsViewModel();
			NavigationVM = new NavigationViewModel();
			UserVM = new UserViewModel();

			if(UserVM.Users.Count > 0) {
				UserVM.SelectedUser = UserVM.Users.First();
				NavigationVM.SelectedPage = NavigationVM.Pages.SingleOrDefault(PageViewModel => PageViewModel.Title == "Home / Instances");
			} else {
				NavigationVM.SelectedPage = NavigationVM.Pages.SingleOrDefault(PageViewModel => PageViewModel.Title == "LoginPage");
			}
		}
	}
}
