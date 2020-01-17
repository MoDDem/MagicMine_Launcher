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
		private MainViewModel MainVM { get; set; }
		private UserModel UserModel { get; set; }

		public ObservableCollection<UserModel> Users { get; set; }

		public ICommand UserListStateCommand { get; set; }

		private UserModel selectedUser;
		public UserModel SelectedUser {
			get => selectedUser; 
			set => Set(ref selectedUser, value, nameof(SelectedUser));
		}

		private bool isUserListOpened;
		public bool IsUserListOpened {
			get => isUserListOpened;
			set => Set(ref isUserListOpened, value, nameof(IsUserListOpened));
		}

		private void UserListState(object obj) => IsUserListOpened = Users.Count > 1 ? !IsUserListOpened : false;

		public UserViewModel(MainViewModel main) {
			MainVM = main;

			UserModel = new UserModel();
			Users = new ObservableCollection<UserModel>(UserModel.GetUsers());
			Users.All(a => { a.ClientToken = "a"; return true; });

			UserListStateCommand = new RelayCommand(UserListState);
		}
	}
}
