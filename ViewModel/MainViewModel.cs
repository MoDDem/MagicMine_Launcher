using System.Linq;
using System.Windows;
using System.Windows.Input;
using MagicMine_Launcher.Components;
using MagicMine_Launcher.View;
using MagicMine_Launcher.ViewModel.Pages;

namespace MagicMine_Launcher.ViewModel {
	class MainViewModel : BaseVM {
		public MainWindow MainWindow { get; private set; }

		private NavigationViewModel navigationVM;
		public NavigationViewModel NavigationVM {
			get => navigationVM;
			set {
				navigationVM = value;
				NavigationVM.PropertyChanged += (a, b) => OnPropertyChanged(nameof(NavigationVM));
			}
		}

		private UserViewModel userVM;
		public UserViewModel UserVM {
			get => userVM;
			set {
				userVM = value;
				UserVM.PropertyChanged += (a, b) => OnPropertyChanged(nameof(UserVM));
			}
		}

		private SettingsViewModel settingsVM;
		public SettingsViewModel SettingsVM {
			get => settingsVM;
			set {
				settingsVM = value;
				SettingsVM.PropertyChanged += (a, b) => OnPropertyChanged(nameof(SettingsVM));
			}
		}

		private RelayCommand openLoginPage;
		public RelayCommand OpenLoginPage {
			get {
				return openLoginPage ??
					(openLoginPage = new RelayCommand(obj => {
						NavigationVM.ChangeVM(typeof(LoginViewModel));
					}));
			}
		}

		public MainViewModel() {
			MainWindow = new MainWindow { DataContext = this };
			MainWindow.Show();

			NavigationVM = new NavigationViewModel(this);
			UserVM = new UserViewModel(this);
			SettingsVM = new SettingsViewModel(this);

			Constructed?.Invoke();

			if(UserVM.Users?.Count > 0) {
				NavigationVM.ChangeVM(typeof(HomeViewModel));
			} else {
				NavigationVM.ChangeVM(typeof(LoginViewModel));
				NavigationVM.BlockNavigation = true;
			}
		}

		public delegate void ConstructorDelegate();
		public event ConstructorDelegate Constructed;
	}
}
