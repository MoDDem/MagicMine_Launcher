using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MagicMine_Launcher.ViewModel.Pages {
	class TechnicPackViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }

		private int page = 1;

		public ObservableCollection<InstanceModel> Instances { get; set; }

		//0 - top, 1 - bottom
		private ObservableCollection<bool> canScrollTopOrBottom;
		public ObservableCollection<bool> CanScrollTopOrBottom {
			get => canScrollTopOrBottom;
			set => Set(ref canScrollTopOrBottom, value, nameof(CanScrollTopOrBottom));
		}

		private bool isProcessing;
		public bool IsProcessing {
			get => isProcessing;
			set => Set(ref isProcessing, value, nameof(IsProcessing));
		}

		private InstanceSort instanceSort;
		public InstanceSort InstanceSort {
			get => instanceSort;
			set => Set(ref instanceSort, value, nameof(InstanceSort));
		}

		private string processingStatus;
		public string ProcessingStatus {
			get => processingStatus;
			set => Set(ref processingStatus, value, nameof(ProcessingStatus));
		}

		private RelayCommand updateScrollViewer;
		public RelayCommand UpdateScrollViewer {
			get {
				return updateScrollViewer ?? (updateScrollViewer = new RelayCommand(obj => {
					ScrollViewer viewer = obj as ScrollViewer;
					if(Math.Abs(viewer.ScrollableHeight - viewer.VerticalOffset) < 1 & !isProcessing & CanScrollTopOrBottom[1]) {
						page++;
						CanScrollTopOrBottom[0] = true;

						Instances.Clear();
						LoadInstances(0);

						if(CanScrollTopOrBottom[0])
							viewer.ScrollToVerticalOffset(0 + 87.85D);
						else
							viewer.ScrollToTop();
					} else if(Math.Abs(viewer.ScrollableHeight - viewer.VerticalOffset) > viewer.ScrollableHeight - 1 & !IsProcessing & CanScrollTopOrBottom[0]) {
						page--;
						if(page <= 1) CanScrollTopOrBottom[0] = false;
						else CanScrollTopOrBottom[1] = true;

						Instances.Clear();
						LoadInstances(0);

						if(CanScrollTopOrBottom[1])
							viewer.ScrollToVerticalOffset(viewer.ScrollableHeight - 87.85D);
						else
							viewer.ScrollToBottom();
					}
				}));
			}
		}

		public TechnicPackViewModel(MainViewModel main) {
			MainVM = main;
			Instances = new ObservableCollection<InstanceModel>();
			PropertyChanged += TechnicPackViewModel_PropertyChanged;
		}

		private void TechnicPackViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			if(e.PropertyName == nameof(InstanceSort)) {
				Instances.Clear();
				page = 1;
				CanScrollTopOrBottom[0] = false;
				CanScrollTopOrBottom[1] = true;

				LoadInstances(0);
			}
		}

		public void PageClosed() { Instances.Clear(); }
		public void PageOpened() {
			CanScrollTopOrBottom = new ObservableCollection<bool> { false, true };
			InstanceSort = InstanceSort.Trending;
			LoadInstances(0);
		}

		private async void LoadInstances(int attempt) {
			IsProcessing = true;
			int i = 1;

			if(attempt >= 3) {
				IsProcessing = false;
				return;
			}

			ProcessingStatus = $"Page {(page > 0 ? page : 0)}. Retrieving versions from TechnicPack...";

			Response response = await new TechnicPackRequest(sorting: InstanceSort, page: page).PerformRequest();
			if(response.IsSuccess) {
				foreach(var item in response.Json["list"] as JArray) {
					ProcessingStatus = $"Sorting instances: {i} of {response.Json["list"].Count()}...";
					i++;

					Instances.Add(new InstanceModel {
						Title = item["title"].ToString(),
						Url = item["url"].ToString(),
						Type = InstanceType.Modded,
						Version = "undefined",
						Image = item["img"].Value<string>() == null 
						? new BitmapImage(new Uri(@"pack://application:,,,/"
							+ Assembly.GetExecutingAssembly().GetName().Name + ";component/Graphics/Icons/TechnicPack.png", UriKind.Absolute))
						: new BitmapImage(new Uri(item["img"].ToString()))
					});

					await Task.Delay(20);
				}
			} else {
				attempt++;
				ProcessingStatus = $"Error while retrieving versions... Trying again: {attempt} of 3 attempts.";

				await Task.Delay(5000);
				LoadInstances(attempt);
			}

			ProcessingStatus = "Done!";

			await Task.Delay(100);
			IsProcessing = false;
		}
	}
}
