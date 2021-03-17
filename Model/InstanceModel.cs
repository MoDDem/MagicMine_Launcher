using MagicMine_Launcher.ViewModel.Pages.Instances;
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
		AllModpacks = 0, Tech = 4472, Magic = 4473, SciFi = 4471, Adventure = 4475, 
		Exploration = 4476, MiniGame = 4477, Quests = 4478, Hardcore = 4479, MapBased = 4480, 
		SmallModpack = 4481, ExtraLarge = 4482, Combat = 4483, Multiplayer = 4484, FTBOfficial = 4487, Skyblock = 4736
	}
	enum InstanceSort {
		// for curseforge: popularity = trending, total downloads = popular 
		Trending, Popular, Updated, New, Name
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
