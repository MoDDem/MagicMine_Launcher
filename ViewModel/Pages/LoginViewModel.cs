using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.View.Pages;

namespace MagicMine_Launcher.ViewModel.Pages {
	class LoginViewModel : BaseVM, IDataErrorInfo, IPageViewModel {
		public MainViewModel MainVM { get; set; }

		private string username;
		public string UserName {
			get => username;
			set => Set(ref username, value, nameof(UserName));
		}

		private string password;
		public string Password {
			get => password;
			set => Set(ref password, value, nameof(Password));
		}

		private bool isAuthProcessing;
		public bool IsAuthProcessing {
			get => isAuthProcessing;
			set => Set(ref isAuthProcessing, value, nameof(IsAuthProcessing));
		}

		public string Error => string.Empty;
		public string this[string columnName] {
			get {
				string error = string.Empty;
				switch(columnName) {
					case "UserName":
						if(string.IsNullOrWhiteSpace(UserName)) {
							error = "Username field must not be empty";
							UserName = string.Empty;
						}
						break;
					case "Password":
						if(string.IsNullOrWhiteSpace(Password)) {
							error = "Password field must not be empty";
							break;
						}
						if(!Password.Any(char.IsUpper) & !Password.Any(char.IsDigit) & !Password.Any(char.IsPunctuation) & !Password.Any(char.IsControl)) {
							error = "Password field doesn't match mojang account rules";
						}
						break;
				}
				return error;
			}
		}

		public ICommand ForgotPassCommand { get; set; }
		public ICommand ProcessAuthCommand { get; set; }

		public LoginViewModel(MainViewModel mainVM) {
			MainVM = mainVM;

			ForgotPassCommand = new RelayCommand(ForgotPassword);
			ProcessAuthCommand = new RelayCommand(ProcessAuth);
		}
		public void PageOpened() { }
		public void PageClosed() { }

		private void ForgotPassword(object obj) {
			System.Diagnostics.Process.Start("https://my.minecraft.net/ru-ru/password/forgot/");
		}

		private void ProcessAuth(object obj) {
			if(string.IsNullOrEmpty(this[nameof(UserName)]) & this[nameof(Password)] != "Password field must not be empty")
				IsAuthProcessing = true;
			else
				return;

			MainVM.UserVM.AuthUserCommand.Execute(null);
		}
	}
}
