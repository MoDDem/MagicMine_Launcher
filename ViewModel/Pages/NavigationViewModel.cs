using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages {
	class NavigationViewModel : BaseVM {
		public ObservableCollection<PageModel> Pages { get; set; }
		private PageModel PageModel { get; set; }

		public ICommand ChangeVMCommand { get; set; }

		private PageModel selectedPage;
		public PageModel SelectedPage {
			get => selectedPage;
			set => Set(ref selectedPage, value, nameof(SelectedPage)); 
		}

		public NavigationViewModel() {
			PageModel = new PageModel();
			Pages = new ObservableCollection<PageModel>(PageModel.GetPages());

			ChangeVMCommand = new RelayCommand(ChangeVM);
		}

		private void ChangeVM(object obj) {
			PageModel vm = Pages.SingleOrDefault(PageViewModel => PageViewModel.ViewModel?.GetType() == obj.GetType());
			SelectedPage = vm ?? Pages[0];
		}
	}
}
