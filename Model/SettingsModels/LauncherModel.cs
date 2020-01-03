using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model.SettingsModels {
	internal class LauncherModel : INotifyPropertyChanged {
		private UserModel selectedUser;
		private string clientToken;
		private bool showConsole;
		private FoldersModel folders;

		public UserModel SelectedUser {
			get => selectedUser;
			set {
				selectedUser = value;
				OnPropertyChanged("SelectedUser");
			}
		}
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
		public FoldersModel Folders {
			get => folders;
			set {
				folders = value;
				OnPropertyChanged("Folders");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}