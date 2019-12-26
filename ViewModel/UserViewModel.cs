using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel {
	class UserViewModel : BaseVM {
		public ObservableCollection<UserModel> Users { get; set; }

		public ICommand UserListStateCommand { get; set; }

		private UserModel selectedUser;
		public UserModel SelectedUser {
			get => selectedUser; 
			set => Set(ref selectedUser, value, nameof(SelectedUser));
		}

		private bool isUserListOpened;
		public bool IsUserListOpened {
			get { return isUserListOpened; }
			set { 
				Set(ref isUserListOpened, value, nameof(IsUserListOpened));
			}
		}

		private void UserListState(object obj) => IsUserListOpened = Users.Count > 1 ? !IsUserListOpened : false;

		public UserViewModel() {
			UserListStateCommand = new RelayCommand(UserListState);

			Users = new ObservableCollection<UserModel> {
				new UserModel { Name = "Deficento", ID = "kdmsld2i1nsa23", AccessToken = "rnd", ClientToken = "rnd client token", IsValid = false, IsInGame = true },
				new UserModel { Name = "MoDDem", ID = "dsakj213blasdx", AccessToken = "rnd", ClientToken = "rnd client token", IsValid = true, IsInGame = false }
			};
		}
	}
}
