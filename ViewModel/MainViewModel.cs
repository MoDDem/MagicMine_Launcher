using MagicMine_Launcher.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher.ViewModel {
	class MainViewModel : BaseVM {
		public NavigationViewModel NavigationVM { get; }

		public MainViewModel() {
			NavigationVM = new NavigationViewModel();

			NavigationVM.SelectedPage = NavigationVM.Pages.First();
		}
	}
}
