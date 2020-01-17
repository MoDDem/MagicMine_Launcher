using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MagicMine_Launcher.Model {
	class UserModel : INotifyPropertyChanged {
		private string id;
		private string name;
		private Image image;
		private bool isValid;
		private bool isInGame;
		private string accessToken;
		private string clientToken;// from settings

		public string Name {
			get { return name; }
			set {
				name = value;
				OnPropertyChanged("Name");
			}
		}
		public string ID {
			get { return id; }
			set {
				id = value;
				OnPropertyChanged("ID");
			}
		}
		public bool IsValid {
			get { return isValid; }
			set {
				isValid = value;
				OnPropertyChanged("IsValid");
			}
		}
		public bool IsInGame {
			get { return isInGame; }
			set {
				isInGame = value;
				OnPropertyChanged("IsInGame");
			}
		}
		public Image Image {
			get { return image; }
			set {
				image = value;
				OnPropertyChanged("Image");
			}
		}
		public string AccessToken {
			get { return accessToken; }
			set {
				accessToken = value;
				OnPropertyChanged("AccessToken");
			}
		}
		public string ClientToken {
			get { return clientToken; }
			set {
				clientToken = value;
				OnPropertyChanged("ClientToken");
			}
		}

		public UserModel[] GetUsers() {
			return new[] {
				new UserModel { Name = "Deficento", ID = "kdmsld2i1nsa23", AccessToken = "rnd", ClientToken = "rnd client token", IsValid = false, IsInGame = true },
				new UserModel { Name = "MoDDem", ID = "dsakj213blasdx", AccessToken = "rnd", ClientToken = "rnd client token", IsValid = true, IsInGame = false }
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
