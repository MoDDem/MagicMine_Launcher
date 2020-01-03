using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model.SettingsModels {
	internal class FoldersModel : INotifyPropertyChanged {
		private string minecraft;
		private string graphics;
		private string data;

		public string Minecraft {
			get => minecraft;
			set {
				minecraft = value;
				OnPropertyChanged("Minecraft");
			}
		}
		public string Graphics {
			get => graphics;
			set {
				graphics = value;
				OnPropertyChanged("Graphics");
			}
		}
		public string Data {
			get => data;
			set {
				data = value;
				OnPropertyChanged("Data");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}