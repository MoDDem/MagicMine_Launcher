using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model.SettingsModels {
	class JavaModel : INotifyPropertyChanged {
		private int minMemory;
		private int maxMemory;
		private string path;
		private string args;

		public int MinMemory {
			get => minMemory;
			set {
				minMemory = value;
				OnPropertyChanged("MinMemory");
			}
		}
		public int MaxMemory {
			get => maxMemory;
			set {
				maxMemory = value;
				OnPropertyChanged("MaxMemory");
			}
		}
		public string Path {
			get => path;
			set {
				path = value;
				OnPropertyChanged("Path");
			}
		}
		public string Args {
			get => args;
			set {
				args = value;
				OnPropertyChanged("Args");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
