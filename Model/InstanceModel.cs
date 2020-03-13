using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MagicMine_Launcher.Model {
	enum InstanceType {
		Release, Snapshot, Beta, Alpha, Modded
	}
	enum InstanceCategory {
		AllModpacks, Tech, Magic, SciFi, Adventure, Exploration, MiniGame, Quests, Hardcore, 
		MapBased, SmallModpack, ExtraLarge, Combat, Multiplayer, FTBOfficial, Skyblock
	}
	class InstanceModel : INotifyPropertyChanged {
		private string title;
		private string version;
		private string url;
		private InstanceType type;
		private InstanceCategory[] category;
		private BitmapImage image;

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
		public InstanceCategory[] Category {
			get => category;
			set {
				category = value;
				OnPropertyChanged("Category");
			}
		}
		public BitmapImage Image {
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
