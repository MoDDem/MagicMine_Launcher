using System;
using System.Windows;
using System.Windows.Input;
using MagicMine_Launcher.Components;

namespace MagicMine_Launcher.ViewModel.Pages {
	class HomeViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }
	}
}
