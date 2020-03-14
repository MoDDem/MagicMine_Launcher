using MagicMine_Launcher.Components.MojangAPI;
using MagicMine_Launcher.Components.MojangAPI.Requests;
using MagicMine_Launcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MagicMine_Launcher.ViewModel.Pages {
	class ATLauncherViewModel : BaseVM, IPageViewModel {
		public MainViewModel MainVM { get; set; }

		public ObservableCollection<InstanceModel> Instances { get; set; }

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

		public ATLauncherViewModel(MainViewModel main) {
			MainVM = main;
			Instances = new ObservableCollection<InstanceModel>();
		}

		public void PageOpened() {
			Instances.Clear();

			LoadInstances(0);
		}

		public void PageClosed() { Instances.Clear(); }

		private async void LoadInstances(int attempt) {
			IsProcessing = true;
			int i = 1;

			if(attempt >= 3) {
				IsProcessing = false;
				return;
			}

			ProcessingStatus = "Retrieving versions from ATLauncher...";

			Response response = await new ATLauncherRequest().PerformRequest();
			if(response.IsSuccess) {
				foreach(var item in response.Json["data"]) {
					ProcessingStatus = $"Adding instances: {i} of {response.Json["data"].Count()}...";
					i++;

					Instances.Add(new InstanceModel {
						Title = item["name"].ToString(),
						Url = item["versions"][0]["__LINK"].ToString(),
						Type = InstanceType.Modded,
						Version = item["versions"][0]["minecraft"].ToString(),
						Image = new BitmapImage(new Uri(@"pack://application:,,,/"
							+ Assembly.GetExecutingAssembly().GetName().Name + ";component/Graphics/Icons/ATLauncher.png", UriKind.Absolute))
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
