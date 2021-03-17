using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages.Instances {
	class VanillaInstanceViewModel : BaseVM {
		private MainViewModel mainVM;
		private VanillaViewModel page;

		private InstanceModel instance;
		public InstanceModel Instance {
			get => instance;
			set => Instance.PropertyChanged += (a, b) => Set(ref instance, value, nameof(Instance));
		}

		public ICommand DownloadInstanceCommand { get; set; }
		public ICommand ConfigureDownloadCommand { get; set; }

        public VanillaInstanceViewModel(InstanceModel model, MainViewModel main) {
			instance = model;
			mainVM = main;
			page = mainVM.NavigationVM.GetPageVM<VanillaViewModel>();

			DownloadInstanceCommand = new RelayCommand(DownloadInstance);
			ConfigureDownloadCommand = new RelayCommand(ConfigureDownload);
		}

		private async void ConfigureDownload(object obj) {
			var minecraftFolder = mainVM.SettingsVM.Settings.Launcher.Folders.Minecraft;

			Directory.CreateDirectory(minecraftFolder + "\\Instances\\" + Instance.Title);

			page.SelectedInstance = null;
			page.IsProcessingDownload = false;
		}

		private async void DownloadInstance(object obj) {
			page.SelectedInstance = this;
			page.IsProcessingDownload = true;

			Response response = await new VanillaRequests().GetVersionInfo(Instance.Url);
			
			if(response.IsSuccess) {
				
			}
		}
	}
}
