using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel {
	class SettingsViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }

		private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"settings.json";

		private SettingsModel settings;
		public SettingsModel Settings {
			get => settings;
			set {
				Set(ref settings, value, nameof(Settings));
			}
		}

		public SettingsViewModel(MainViewModel main) {
			MainVM = main;

			CreateLoadSettingsFile();

			Settings.PropertyChanged += Settings_PropertyChanged;
			MainVM.UserVM.PropertyChanged += (a, b) => Settings.Launcher.SelectedUser = MainVM.UserVM.SelectedUser.ID?.Substring(0, 10);
		}

		private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if(Settings.HasErrors)
				return;

			File.WriteAllText(path, string.Empty);
			using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
				byte[] str = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Settings, Formatting.Indented));
				fs.Write(str, 0, str.Length);
			}
		}

		private void CreateLoadSettingsFile() {
			Settings = new SettingsModel {
				Launcher = new LauncherModel {
					ShowConsole = true,
					ClientToken = Guid.NewGuid().ToString(),
					Folders = new FoldersModel {
						Data = AppDomain.CurrentDomain.BaseDirectory + "Data",
						Graphics = AppDomain.CurrentDomain.BaseDirectory + "Graphics",
						Minecraft = AppDomain.CurrentDomain.BaseDirectory + "Minecraft",
					},
					SelectedUser = MainVM.UserVM.SelectedUser?.ID?.Substring(0, 10)
				},
				Minecraft = new MinecraftModel {
					Width = 1024,
					Height = 768,
					IsFullScreen = false
				},
				Java = new JavaModel {
					Path = Directory.GetDirectories(@"C:\Program Files\Java\")[0] + @"\bin",
					MinMemory = 256,
					MaxMemory = 2048
				}
			};

			if(File.Exists(path)) {
				Settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(path));
			} else {
				using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
					byte[] str = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Settings, Formatting.Indented));
					fs.Write(str, 0, str.Length);
				}
			}
		}
	}
}
