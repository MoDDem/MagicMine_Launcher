using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Model.SettingsModels {
	class MinecraftModel : INotifyPropertyChanged {
		private int width;
		private int height;
		private bool isFullScreen;

		public int Width {
			get => width;
			set {
				width = value;
				OnPropertyChanged("Width");
			}
		}
		public int Height {
			get => height;
			set {
				height = value;
				OnPropertyChanged("Height");
			}
		}
		public bool IsFullScreen {
			get => isFullScreen;
			set {
				isFullScreen = value;
				OnPropertyChanged("IsFullScreen");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
