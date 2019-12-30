using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MagicMine_Launcher.Components;
using MagicMine_Launcher.View.Pages;

namespace MagicMine_Launcher.ViewModel.Pages {
	class LoginViewModel : BaseVM, IDataErrorInfo {
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

		public string Error => throw new NotImplementedException();

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
						}
						break;
				}
				return error;
			}
		}
	}
}
