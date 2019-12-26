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
		private object viewModel;

		public string Title {
			get { return title; }
			set {
				title = value;
				OnPropertyChanged("Title");
			}
		}
		public int Index {
			get { return index; }
			set {
				index = value;
				OnPropertyChanged("Index");
			}
		}
		public object ViewModel {
			get { return viewModel; }
			set {
				viewModel = value;
				OnPropertyChanged("ViewModel");
			}
		}

		public PageModel[] GetPages() {
			return new[] {
				new PageModel { Index = 0, Title = "Home / Instances", ViewModel = new HomeViewModel() },
				new PageModel { Index = 1, Title = "Vanilla", ViewModel = new LoginViewModel() },
				new PageModel { Index = 2, Title = "CurseForge", },
				new PageModel { Index = 3, Title = "ATLauncher", },
				new PageModel { Index = 4, Title = "TechnicLauncher", },
				new PageModel { Index = 5, Title = "Settings", },
				new PageModel { Index = 6, Title = "Visit website", },
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
