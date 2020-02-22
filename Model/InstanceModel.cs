using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MagicMine_Launcher.Model {
	enum InstanceType {
		Release, Snapshot, Beta, Alpha, Modded
	}

	class InstanceModel : INotifyPropertyChanged {
		private string title;
		private string version;
		private string url;
		private InstanceType type;
		private Image image;

		public string Title {
			get => title;
			set {
				title = value;
				OnPropertyChanged("Title");
			}
		}
		public string Version {
			get => version;
			set {
				version = value;
				OnPropertyChanged("Version");
			}
		}
		public string Url {
			get => url;
			set {
				url = value;
				OnPropertyChanged("Url");
			}
		}
		public InstanceType Type {
			get => type;
			set {
				type = value;
				OnPropertyChanged("Type");
			}
		}
		public Image Image {
			get => image;
			set {
				image = value;
				OnPropertyChanged("Image");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string propName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
	}
}
