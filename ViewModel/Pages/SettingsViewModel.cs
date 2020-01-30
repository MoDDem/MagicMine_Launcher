using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.Model.SettingsModels;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages {
	class SettingsViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }
	}
}
