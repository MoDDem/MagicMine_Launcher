﻿using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MagicMine_Launcher.ViewModel.Pages {
	class CurseForgeModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }
		//TODO: category sorting(only one category can be selected)

		private int index = 0;
		
		public ObservableCollection<InstanceModel> Instances { get; set; }

		//0 - top, 1 - bottom
		private ObservableCollection<bool> canScrollTopOrBottom;
		public ObservableCollection<bool> CanScrollTopOrBottom {
			get => canScrollTopOrBottom;
			set => Set(ref canScrollTopOrBottom, value, nameof(CanScrollTopOrBottom));
		}

		private int bufferHeight;
		public int BufferHeight {
			get => bufferHeight;
			set => Set(ref bufferHeight, value, nameof(BufferHeight));
		}

		private bool isProcessing;
		public bool IsProcessing {
			get => isProcessing;
			set => Set(ref isProcessing, value, nameof(IsProcessing));
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
						index += 20;
						CanScrollTopOrBottom[0] = true;

						Instances.Clear();
						LoadInstances(0);

						if(CanScrollTopOrBottom[0])
							viewer.ScrollToVerticalOffset(0 + 87.85D);
						else
							viewer.ScrollToTop();
					}else if (Math.Abs(viewer.ScrollableHeight - viewer.VerticalOffset) > viewer.ScrollableHeight - 1 & !IsProcessing & CanScrollTopOrBottom[0]) {
						index -= 20;
						if(index <= 0) CanScrollTopOrBottom[0] = false;
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

		public CurseForgeModel(MainViewModel main) {
			MainVM = main;
			Instances = new ObservableCollection<InstanceModel>();
		}

		public void PageClosed() { Instances.Clear(); }
		public void PageOpened() {
			CanScrollTopOrBottom = new ObservableCollection<bool> { false, true };
			LoadInstances(0);
		}

		private async void LoadInstances(int attempt) {
			IsProcessing = true;
			int i = 1;

			ProcessingStatus = "Retrieving versions from CurseForge...";

			Response response = await new CurseForgeRequest(index: index).PerformRequest();
			foreach(var item in response.Json["list"] as JArray) {
				ProcessingStatus = $"Sorting instances: {i} of {Instances.Count}...";
				i++;

				Instances.Add(new InstanceModel {
					Title = item["name"].ToString(),
					Url = item["latestFiles"][0]["downloadUrl"].ToString(),
					Type = InstanceType.Modded,
					Version = item["latestFiles"][0]["sortableGameVersion"][0]["gameVersion"].ToString(),
					Image = new BitmapImage(new Uri(item["attachments"][0]["thumbnailUrl"].ToString()))
				});

				await Task.Delay(20);
			}

			ProcessingStatus = "Done!";

			await Task.Delay(100);
			IsProcessing = false;
		}
	}
}
