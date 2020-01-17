using System.Linq;
using MagicMine_Launcher.ViewModel.Pages;

namespace MagicMine_Launcher.ViewModel {
	class MainViewModel : BaseVM {
		public NavigationViewModel NavigationVM { get; set; }
		public UserViewModel UserVM { get; set; }
		public SettingsViewModel SettingsVM { get; set; }

		public MainViewModel() {
			NavigationVM = new NavigationViewModel(this);

			UserVM = new UserViewModel(this);
			if(UserVM.Users.Count > 0) {
				UserVM.SelectedUser = UserVM.Users.First();
				NavigationVM.SelectedPage = NavigationVM.Pages.SingleOrDefault(PageViewModel => PageViewModel.Title == "Home / Instances");
			} else {
				NavigationVM.SelectedPage = NavigationVM.Pages.SingleOrDefault(PageViewModel => PageViewModel.Title == "LoginPage");
			}

			SettingsVM = new SettingsViewModel(this);
		}
	}
}
