using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.ViewModel.Pages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel {
	class UserViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }

		public ObservableCollection<UserModel> Users { get; set; }

		public ICommand UserListStateCommand { get; set; }
		public ICommand ValidateUserCommand { get; set; }

		public ICommand AuthUserCommand { get; set; }
		public ICommand LogoutUserCommand { get; set; }

		private UserModel selectedUser;
		public UserModel SelectedUser {
			get => selectedUser;
			set { 
				Set(ref selectedUser, value, nameof(SelectedUser));
				CheckIfValid(SelectedUser);
			}
		}

		private bool isUserListOpened;
		public bool IsUserListOpened {
			get => isUserListOpened;
			set => Set(ref isUserListOpened, value, nameof(IsUserListOpened));
		}

		public UserViewModel(MainViewModel main) {
			MainVM = main;

			MainVM.Constructed += () => Users = new ObservableCollection<UserModel>(LoadUsers());

			UserListStateCommand = new RelayCommand(UserListState);
			ValidateUserCommand = new RelayCommand(ValidateUser);

			AuthUserCommand = new RelayCommand(AuthUser);
			LogoutUserCommand = new RelayCommand(LogoutUser);
		}

		private List<UserModel> LoadUsers() {
			string settingsPath = MainVM.SettingsVM.Settings.Launcher.Folders.Data + @"\Users\";
			Directory.CreateDirectory(settingsPath);

			List<UserModel> userList = new List<UserModel>();
			foreach(var item in Directory.GetFiles(settingsPath)) {
				UserModel user = JsonConvert.DeserializeObject<UserModel>(File.ReadAllText(item));
				userList.Add(user);

				if(user.ID.Substring(0, 10) == MainVM.SettingsVM.Settings.Launcher.SelectedUser)
					SelectedUser = user;
			}

			if(SelectedUser == null & userList.Count > 0)
				SelectedUser = userList.First();

			return userList;
		}

		//test function
		private void CheckIfValid(UserModel user) {
			/*
			string clientToken = MainVM.SettingsVM.Settings.Launcher.ClientToken;
			
			Response validate = await new ValidateRequest(user.AccessToken, clientToken).PerformRequest();
			*/
			user.IsValid = true;
		}

		private void UserListState(object obj) => IsUserListOpened = Users?.Count > 1 ? !IsUserListOpened : false;

		private void ValidateUser(object obj) {
			MainVM.NavigationVM.ChangeVM(typeof(LoginViewModel));
			(MainVM.NavigationVM.SelectedPage.ViewModel as LoginViewModel).UserName = SelectedUser.Name;
		}

		private void LogoutUser(object obj) { 
			if(obj is UserModel) {
				Users.Remove((UserModel) obj);
				if(SelectedUser == obj)
					SelectedUser = Users.Count > 0 ? Users.First() : null;
				return;
			}

			if(SelectedUser == null)
				return;

			string settingsPath = MainVM.SettingsVM.Settings.Launcher.Folders.Data + @"\Users\";
			Directory.CreateDirectory(settingsPath);

			File.Delete(settingsPath + SelectedUser.ID.Substring(0, 10) + ".json");

			var cUsers = Users.Where(a => a != SelectedUser).ToList();
			SelectedUser = cUsers.Count > 0 ? cUsers.First() : null;
			Users = new ObservableCollection<UserModel>(cUsers);
		}

		private async void AuthUser(object obj) {
			string name, password;
			string clientToken = MainVM.SettingsVM.Settings.Launcher.ClientToken;
			LoginViewModel login = MainVM.NavigationVM.GetPageVM<LoginViewModel>();

			if(obj is string[] & string.IsNullOrEmpty(login.UserName)) {
				name = (obj as string[])[0];
				password = (obj as string[])[1];
			} else {
				name = login.UserName;
				password = login.Password;
			}

			Response auth = await new AuthRequest(name, password, clientToken).PerformRequest();

			if(auth.IsSuccess) {
				UserModel user = new UserModel {
					AccessToken = auth.Json["accessToken"].ToString(),
					ID = auth.Json["selectedProfile"]["id"].ToString(),
					Name = auth.Json["selectedProfile"]["name"].ToString(),
					IsInGame = false,
					IsValid = true
				};

				Users.Add(user);
				SelectedUser = user;

				SaveUserData(user);

				MainVM.NavigationVM.BlockNavigation = false;
				MainVM.NavigationVM.ChangeVM(typeof(HomeViewModel));
			}

			login.UserName = null;
			login.Password = null;
			login.IsAuthProcessing = false;
		}

		private void SaveUserData(UserModel user) {
			string settingsPath = MainVM.SettingsVM.Settings.Launcher.Folders.Data + @"\Users\";
			Directory.CreateDirectory(settingsPath);

			string name = user.ID.Substring(0, 10) + ".json";

			using(FileStream fs = new FileStream(settingsPath + name, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
				byte[] str = Encoding.Default.GetBytes(JsonConvert.SerializeObject(user, Formatting.Indented));
				fs.Write(str, 0, str.Length);
			}
		}
	}
}
