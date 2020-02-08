using Newtonsoft.Json;
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
		[JsonIgnore]
		public bool IsValid {
			get { return isValid; }
			set {
				isValid = value;
				OnPropertyChanged("IsValid");
			}
		}
		[JsonIgnore]
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

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
