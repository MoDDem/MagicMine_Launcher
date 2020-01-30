using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MagicMine_Launcher.Model {
	class SettingsModel : INotifyPropertyChanged {
		private MinecraftModel minecraft;
		private LauncherModel launcher;
		private JavaModel java;

		public MinecraftModel Minecraft {
			get => minecraft;
			set { 
				minecraft = value;
				Minecraft.PropertyChanged += (a, b) => OnPropertyChanged(nameof(Minecraft));
			} 
		}

		public LauncherModel Launcher {
			get => launcher;
			set { 
				launcher = value;
				Launcher.PropertyChanged += (a, b) => OnPropertyChanged(nameof(Launcher));
			}
		}

		public JavaModel Java {
			get => java;
			set { 
				java = value;
				Java.PropertyChanged += (a, b) => OnPropertyChanged(nameof(Java));
			}
		}

		[JsonIgnore]
		public bool HasErrors {
			get {
				bool hasErrors = false;
				if(!string.IsNullOrEmpty(Java[nameof(Java.MinMemory)]) | !string.IsNullOrEmpty(Java[nameof(Java.MaxMemory)]))
					hasErrors = true;

				return hasErrors;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}