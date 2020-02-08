using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using System;
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
				Set(ref selectedPage, value, nameof(SelectedPage));
				if(selectedPage.ViewModel != null)
					selectedPage.ViewModel.MainVM = MainVM;
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
			Pages = new ObservableCollection<PageModel>(PageModel.GetPages());
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
