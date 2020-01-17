using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel {
	class SettingsViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }

		private SettingsModel settings;
		public SettingsModel Settings {
			get => settings;
			set {
				Set(ref settings, value, nameof(Settings));
			}
		}

		public ICommand SerializeJsonCommand { get; set; }

		public SettingsViewModel(MainViewModel main) {
			MainVM = main;

			SerializeJsonCommand = new RelayCommand(SerializeJson);

			LauncherModel launcher = new LauncherModel();
			MinecraftModel minecraft = new MinecraftModel();
			JavaModel java = new JavaModel();
			Settings = new SettingsModel(launcher, minecraft, java);

			Settings.Launcher.Folders = new FoldersModel {
				Data = "data",
				Graphics = "graphics",
				Minecraft = "minecraft"
			};

			MainVM.UserVM.PropertyChanged += (a, b) => Settings.Launcher.SelectedUser = MainVM.UserVM.SelectedUser.Name;
		}

		private void SerializeJson(object obj) => MessageBox.Show(JsonConvert.SerializeObject(Settings, Formatting.Indented));
	}
}
