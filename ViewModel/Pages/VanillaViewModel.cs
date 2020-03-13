using System;
using System.Reflection;
using System.Threading.Tasks;
using MagicMine_Launcher.Model;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using MagicMine_Launcher.Components;
using System.Collections.ObjectModel;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;

namespace MagicMine_Launcher.ViewModel.Pages {
	class VanillaViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }

		private List<string> categories;
		private bool isClosing;

		private ObservableCollection<bool> selectedCategories;
		public ObservableCollection<bool> SelectedCategories {
			get => selectedCategories;
			set => Set(ref selectedCategories, value, nameof(SelectedCategories));
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

		public List<InstanceModel> Instances { get; set; }
		public ObservableCollection<InstanceModel> SortedInstances { get; set; }

		private InstanceModel activeInstance;
		public InstanceModel ActiveInstance {
			get => activeInstance;
			set {
				Set(ref activeInstance, value, nameof(ActiveInstance));
			}
		}

		private RelayCommand updateInstanceCategory;
		public RelayCommand UpdateInstanceCategory {
			get {
				return updateInstanceCategory ??
					(updateInstanceCategory = new RelayCommand(async obj => {
						if(!isClosing) {
							bool enable = false, multipler = false;
							string category = null;
							if(obj.ToString().Contains("+")) {
								category = obj.ToString().Replace("+", string.Empty);
								enable = true;
								categories.Add(category);
							} else if(obj.ToString().Contains("-")) {
								category = obj.ToString().Replace("-", string.Empty);
								enable = false;
								categories.Remove(category);
							}else if(obj.ToString().Contains("*")) {
								category = obj.ToString().Replace("*", string.Empty);
								multipler = true;
								categories.Clear();
								categories.Add(category);
								for(int i = 0; i < SelectedCategories.Count; i++)
									SelectedCategories[i] = false;
							}

							if(category != null) {
								switch(category) {
									case "Release":
										SelectedCategories[0] = multipler == false ? enable : multipler;
										break;
									case "Snapshot":
										SelectedCategories[1] = multipler == false ? enable : multipler;
										break;
									case "Beta":
										SelectedCategories[2] = multipler == false ? enable : multipler;
										break;
									case "Alpha":
										SelectedCategories[3] = multipler == false ? enable : multipler;
										break;
								}
							}

							foreach(var item in Instances) {
								if(categories.Contains(item.Type.ToString())) {
									if(SortedInstances.Contains(item))
										continue;

									SortedInstances.Add(item);
									await Task.Delay(20);
								} else {
									if(SortedInstances.Contains(item))
										SortedInstances.Remove(item);
								}
							}
						}
				}));
			}
		}

		public VanillaViewModel(MainViewModel mainVM) {
			MainVM = mainVM;

			Instances = new List<InstanceModel>();
			SortedInstances = new ObservableCollection<InstanceModel>();

			SelectedCategories = new ObservableCollection<bool>{ true, false, false, false };
			categories = new List<string> { 
				"Release"
			};
		}
		public void PageOpened() {
			isClosing = false;
			LoadInstances(0);
		}
		public void PageClosed() { 
			isClosing = true; 
			SortedInstances.Clear();
			Instances.Clear();
		}

		private async void LoadInstances(int attempt) {
			SortedInstances.Clear();
			IsProcessing = true;
			int i = 1;

			if(attempt >= 3) {
				IsProcessing = false;
				return;
			}

			if(Instances.Count > 0) {
				ProcessingStatus = "Building list of instances...";

				foreach(var item in Instances) {
					ProcessingStatus = $"Sorting instances: {i} of {Instances.Count}...";
					i++;

					if(categories.Contains(item.Type.ToString())) {
						if(SortedInstances.Contains(item))
							continue;

						SortedInstances.Add(item);
						await Task.Delay(20);
					} else {
						if(SortedInstances.Contains(item))
							SortedInstances.Remove(item);
					}
				}

				ProcessingStatus = "Done!";

				await Task.Delay(100);
				IsProcessing = false;

				return;
			}

			ProcessingStatus = "Retrieving versions from mojang...";

			Response versions = await new VersionsRequest().PerformRequest();

			if(versions.IsSuccess) {
				ProcessingStatus = "Building list of instances...";

				foreach(var item in versions.Json["versions"]) {
					InstanceModel instance = new InstanceModel {
						Title = "Vanilla " + item["id"].ToString(),
						Url = item["url"].ToString(),
						Version = item["id"].ToString(),
						Image = new BitmapImage(new Uri(@"pack://application:,,,/"
						+ Assembly.GetExecutingAssembly().GetName().Name + ";component/Graphics/Icons/vanilla.jpg", UriKind.Absolute)),
						Type = item["type"].ToString().Contains("old_") ?
							(InstanceType) Enum.Parse(typeof(InstanceType), item["type"].ToString().Replace("old_", string.Empty), true) :
							(InstanceType) Enum.Parse(typeof(InstanceType), item["type"].ToString(), true)
					};
					Instances.Add(instance);
				}
			} else {
				attempt++;
				ProcessingStatus = $"Error while retrieving versions... Trying again: {attempt} of 3 attempts.";

				await Task.Delay(5000);
				LoadInstances(attempt);
			}

			foreach(var item in Instances) {
				ProcessingStatus = $"Sorting instances: {i} of {Instances.Count}...";
				i++;

				if(categories.Contains(item.Type.ToString())) {
					if(SortedInstances.Contains(item))
						continue;

					SortedInstances.Add(item);
					await Task.Delay(20);
				} else {
					if(SortedInstances.Contains(item))
						SortedInstances.Remove(item);
				}
			}

			ProcessingStatus = "Done!";

			await Task.Delay(100);
			IsProcessing = false;
		}
	}
}
