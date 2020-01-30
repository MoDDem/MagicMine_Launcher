using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MagicMine_Launcher.Model.SettingsModels {
	internal class LauncherModel : INotifyPropertyChanged {
		private string clientToken;
		private bool showConsole;
		private string selectedUser;
		private FoldersModel folders;

		public string ClientToken {
			get => clientToken;
			set {
				clientToken = value;
				OnPropertyChanged("ClientToken");
			}
		}
		public bool ShowConsole {
			get => showConsole;
			set {
				showConsole = value;
				OnPropertyChanged("ShowConsole");
			}
		}
		public string SelectedUser {
			get => selectedUser;
			set {
				selectedUser = value;
				OnPropertyChanged("SelectedUser");
			}
		}
		public FoldersModel Folders {
			get => folders;
			set {
				folders = value;
				Folders.PropertyChanged += (a, b) => OnPropertyChanged(nameof(Folders));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}