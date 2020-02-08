using System;
using System.Windows;
using System.Windows.Input;
using MagicMine_Launcher.Components;

namespace MagicMine_Launcher.ViewModel.Pages {
	class HomeViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }

        private RelayCommand openVanillaPage;
        public RelayCommand OpenVanillaPage {
            get {
                return openVanillaPage ??
                  (openVanillaPage = new RelayCommand(obj => {
                      MainVM.NavigationVM.ChangeVM(typeof(SettingsViewModel));
                  }));
            }
        }
	}
}
