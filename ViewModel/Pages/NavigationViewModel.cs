using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages {
	class NavigationViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }
		private PageModel PageModel { get; set; }

		public ObservableCollection<PageModel> Pages { get; set; }

		private PageModel selectedPage;
		public PageModel SelectedPage {
			get => selectedPage;
			set {
				if(selectedPage != null)
					selectedPage.ViewModel?.PageClosed();
				
				Set(ref selectedPage, value, nameof(SelectedPage));
				
				if(selectedPage != null)
					selectedPage.ViewModel?.PageOpened();
			}
		}

		private bool blockNavigation;
		public bool BlockNavigation {
			get => blockNavigation;
			set {
				Set(ref blockNavigation, value, nameof(BlockNavigation));
			}
		}

		public NavigationViewModel(MainViewModel main) {
			MainVM = main;

			PageModel = new PageModel();
			Pages = new ObservableCollection<PageModel>(LoadPages());
		}

		private List<PageModel> LoadPages() {
			return new List<PageModel>() {
				new PageModel { IsHidden = true, Title = "LoginPage", ViewModel = new LoginViewModel(MainVM) },

				new PageModel { Index = 0, Title = "Home / Instances", ViewModel = new HomeViewModel(MainVM) },
				new PageModel { Index = 1, Title = "Vanilla", ViewModel = new VanillaViewModel(MainVM) },
				new PageModel { Index = 2, Title = "CurseForge", ViewModel = new CurseForgeViewModel(MainVM) },
				new PageModel { Index = 3, Title = "ATLauncher", ViewModel = new ATLauncherViewModel(MainVM) },
				new PageModel { Index = 4, Title = "TechnicLauncher", },
				new PageModel { Index = 5, Title = "Settings", ViewModel = new SettingsViewModel(MainVM) },
				new PageModel { Index = 6, Title = "Visit website", },
			};
		}

		public void ChangeVM(Type obj) {
			if(obj.GetInterface("IPageViewModel") == null)
				throw new Exception("Not a page");

			SelectedPage = Pages.SingleOrDefault(a => a.ViewModel?.GetType() == obj);
		}

		public T GetPageVM<T>() where T : class {
			return (T) Pages.SingleOrDefault(a => a.ViewModel?.GetType() == typeof(T)).ViewModel;
		}
	}
}
