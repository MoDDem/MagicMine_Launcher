using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages {
	class NavigationViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }
		private PageModel PageModel { get; set; }

		public ObservableCollection<PageModel> Pages { get; set; }

		public ICommand ChangeVMCommand { get; set; }

		private PageModel selectedPage;
		public PageModel SelectedPage {
			get => selectedPage;
			set { 
				Set(ref selectedPage, value, nameof(SelectedPage));
				if(selectedPage.ViewModel != null)
					selectedPage.ViewModel.MainVM = MainVM;
			}
		}

		public NavigationViewModel(MainViewModel main) {
			MainVM = main;

			PageModel = new PageModel();
			Pages = new ObservableCollection<PageModel>(PageModel.GetPages());

			ChangeVMCommand = new RelayCommand(ChangeVM);
		}

		private void ChangeVM(object obj) {
			PageModel vm = Pages.SingleOrDefault(PageViewModel => PageViewModel.Title == obj.ToString());
			SelectedPage = vm ?? Pages[0];
		}
	}
}
