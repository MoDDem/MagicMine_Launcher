using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;

namespace MagicMine_Launcher.ViewModel {
	class SettingsViewModel : BaseVM {
		private SettingsModel settingsModel;
		public SettingsModel SettingsModel {
			get => settingsModel;
			set {
				Set(ref settingsModel, value, nameof(SettingsModel));
			}
		}

		public SettingsViewModel() {
			LauncherModel launcher = new LauncherModel();
			MinecraftModel minecraft = new MinecraftModel();
			JavaModel java = new JavaModel();
			SettingsModel = new SettingsModel(launcher, minecraft, java);

			SettingsModel.LauncherModel.Folders = new FoldersModel {
				Data = "data",
				Graphics = "graphics",
				Minecraft = "minecraft"
			};

			MessageBox.Show(JsonConvert.SerializeObject(SettingsModel, Formatting.Indented));
		}
	}
}
