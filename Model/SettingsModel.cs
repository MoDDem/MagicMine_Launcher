using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model {
	class SettingsModel : INotifyPropertyChanged {
		private MinecraftModel minecraftModel;
		private LauncherModel launcherModel;
		private JavaModel javaModel;

		[JsonProperty("Minecraft")]
		public MinecraftModel MinecraftModel {
			get => minecraftModel;
			set { 
				minecraftModel = value; 
				OnPropertyChanged("MinecraftModel"); 
			} 
		}

		[JsonProperty("Launcher")]
		public LauncherModel LauncherModel {
			get => launcherModel;
			set { 
				launcherModel = value; 
				OnPropertyChanged("LauncherModel"); 
			}
		}

		[JsonProperty("Java")]
		public JavaModel JavaModel {
			get => javaModel;
			set { 
				javaModel = value; 
				OnPropertyChanged("JavaModel"); 
			}
		}

		public SettingsModel(LauncherModel launcher, MinecraftModel minecraft, JavaModel java) {
			MinecraftModel = minecraft;
			LauncherModel = launcher;
			JavaModel = java;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}