using MagicMine_Launcher.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model {
	class PageModel : INotifyPropertyChanged {
		private int index;
		private string title;
		private IPageViewModel viewModel;
		private bool isHidden;

		public string Title {
			get => title;
			set {
				title = value;
				OnPropertyChanged("Title");
			}
		}
		public int Index {
			get => index; 
			set {
				index = value;
				OnPropertyChanged("Index");
			}
		}
		public IPageViewModel ViewModel {
			get => viewModel; 
			set {
				viewModel = value;
				OnPropertyChanged("ViewModel");
			}
		}
		public bool IsHidden {
			get => isHidden;
			set {
				isHidden = value;
				OnPropertyChanged("IsHidden");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
