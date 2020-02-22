using MagicMine_Launcher.Components;
using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MagicMine_Launcher.ViewModel.Pages {
	class VanillaViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }

		private List<string> categories;

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
						if(obj.ToString().Contains("+"))
							categories.Add(obj.ToString().Replace("+", string.Empty));
						else
							categories.Remove(obj.ToString().Replace("-", string.Empty));

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
				}));
			}
		}

		public VanillaViewModel(MainViewModel mainVM) {
			MainVM = mainVM;

			Instances = new List<InstanceModel>();
			SortedInstances = new ObservableCollection<InstanceModel>();
			categories = new List<string> { 
				"Release"
			};
		}
		public void PageOpened() { LoadInstances(0); }
		public void PageClosed() { }

		private async void LoadInstances(int attempt) {
			if(attempt >= 3)
				return;

			Response versions = await new VersionsRequest().PerformRequest();
			if(versions.IsSuccess) {
				foreach(var item in versions.Json["versions"]) {
					InstanceModel instance = new InstanceModel {
						Title = "Vanilla " + item["id"].ToString(),
						Url = item["url"].ToString(),
						Version = item["id"].ToString(),
						Type = item["type"].ToString().Contains("old_") ?
							(InstanceType) Enum.Parse(typeof(InstanceType), item["type"].ToString().Replace("old_", string.Empty), true) :
							(InstanceType) Enum.Parse(typeof(InstanceType), item["type"].ToString(), true)
					};
					Instances.Add(instance);
				}
			} else {
				attempt++;
				LoadInstances(attempt);
				await Task.Delay(5000);
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
	}
}
