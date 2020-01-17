using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MagicMine_Launcher.Model {
	class SettingsModel : INotifyPropertyChanged {
		private MinecraftModel minecraft;
		private LauncherModel launcher;
		private JavaModel java;

		[JsonProperty("Minecraft")]
		public MinecraftModel Minecraft {
			get => minecraft;
			set { 
				minecraft = value; 
				OnPropertyChanged("Minecraft"); 
			} 
		}

		[JsonProperty("Launcher")]
		public LauncherModel Launcher {
			get => launcher;
			set { 
				launcher = value; 
				OnPropertyChanged("Launcher"); 
			}
		}

		[JsonProperty("Java")]
		public JavaModel Java {
			get => java;
			set { 
				java = value; 
				OnPropertyChanged("Java"); 
			}
		}

		public SettingsModel(LauncherModel launcher, MinecraftModel minecraft, JavaModel java) {
			Minecraft = minecraft;
			Launcher = launcher;
			Java = java;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}