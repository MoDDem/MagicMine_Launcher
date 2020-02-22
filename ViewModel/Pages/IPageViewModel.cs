using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.ViewModel.Pages {
	interface IPageViewModel {
		MainViewModel MainVM { get; set; }
		void PageOpened();
		void PageClosed();
	}
}
