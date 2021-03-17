using MagicMine_Launcher.Components;
using MagicMine_Launcher.Model;
using MagicMine_Launcher.Model.SettingsModels;
using MagicMine_Launcher.View;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MagicMine_Launcher.ViewModel {
	class SettingsViewModel : BaseVM {
		private MainViewModel MainVM { get; set; }

		private JavaNotFound javaWindow;

		private readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"settings.json";

		private SettingsModel settings;
		public SettingsModel Settings {
			get => settings;
			set {
				Set(ref settings, value, nameof(Settings));
			}
		}

		private string javaPath;
		public string JavaPath {
			get => javaPath;
			set {
				Set(ref javaPath, value, nameof(JavaPath));
			}
		}

		private RelayCommand locateJavaCommand;
		public RelayCommand LocateJavaCommand {
			get {
				return locateJavaCommand ?? (locateJavaCommand = new RelayCommand(obj => {
					using(var dialog = new FolderBrowserDialog()) {
						dialog.ShowNewFolderButton = false;
						DialogResult result = dialog.ShowDialog();
						if(result == DialogResult.OK) {
							JavaPath = dialog.SelectedPath;
						}
					}
				}));
			}
		}

		private RelayCommand assignSelectedPathCommand;
		public RelayCommand AssignSelectedPathCommand {
			get {
				return assignSelectedPathCommand ?? (assignSelectedPathCommand = new RelayCommand(obj => {
					if(JavaPath.Contains("jre")) {
						JavaPath += JavaPath.EndsWith("\\") ? "lib\\javaw.exe" : "\\lib\\javaw.exe";
					} else {
						if(!File.Exists(JavaPath + "\\lib\\javaw.exe")) {
							JavaPath = "Java not found. Please try again.";
							return;
						} else
							JavaPath += JavaPath.EndsWith("\\") ? "lib\\javaw.exe" : "\\lib\\javaw.exe";
					}
					javaWindow.Close();
				}));
			}
		}

		private RelayCommand installJavaCommand;
		public RelayCommand InstallJavaCommand {
			get {
				return installJavaCommand ?? (installJavaCommand = new RelayCommand(obj => {
					//TODO: installation functionality
					javaWindow.Close();
				}));
			}
		}

		private RelayCommand closeWindowCommand;
		public RelayCommand CloseWindowCommand {
			get {
				return closeWindowCommand ?? (closeWindowCommand = new RelayCommand(obj => {
					javaWindow.Close();
				}));
			}
		}

		public SettingsViewModel(MainViewModel main) {
			MainVM = main;

			CreateLoadSettingsFile();

			Settings.PropertyChanged += Settings_PropertyChanged;
			MainVM.UserVM.PropertyChanged += (a, b) => Settings.Launcher.SelectedUser = MainVM.UserVM.SelectedUser.ID?.Substring(0, 10);
		}

		private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if(Settings.HasErrors)
				return;

			File.WriteAllText(path, string.Empty);
			using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
				byte[] str = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Settings, Formatting.Indented));
				fs.Write(str, 0, str.Length);
			}
		}

		private void CreateLoadSettingsFile() {
			bool isSettingsExist = File.Exists(path);
			
			string javaLoc = null;
			if(!isSettingsExist)
				javaLoc = SearchForJava();

			Settings = new SettingsModel {
				Launcher = new LauncherModel {
					ShowConsole = true,
					HideConsoleOnQuit = false,
					ClientToken = Guid.NewGuid().ToString(),
					Folders = new FoldersModel {
						Data = AppDomain.CurrentDomain.BaseDirectory + "Data",
						Graphics = AppDomain.CurrentDomain.BaseDirectory + "Graphics",
						Minecraft = AppDomain.CurrentDomain.BaseDirectory + "Minecraft",
					},
					SelectedUser = MainVM.UserVM.SelectedUser?.ID?.Substring(0, 10)
				},
				Minecraft = new MinecraftModel {
					Width = 1024,
					Height = 768,
					IsFullScreen = false
				},
				Java = new JavaModel {
					Path = javaLoc,
					MinMemory = 256,
					MaxMemory = 2048
				}
			};

			if(isSettingsExist) {
				Settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(path));
			} else {
				using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
					byte[] str = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Settings, Formatting.Indented));
					fs.Write(str, 0, str.Length);
				}
			}
		}

		private string SearchForJava() {
			var values = Environment.GetEnvironmentVariable("PATH");
			foreach(var item in values.Split(Path.PathSeparator)) {
				var fullPath = Path.Combine(item, "javaw.exe");
				if(File.Exists(fullPath))
					return fullPath;
			}
			string[] dirs = null;
			try
			{
				dirs = Directory.GetDirectories(@"C:\Program Files\Java\");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			
			if(dirs != null) 
				return dirs[0] + @"\bin\javaw.exe";

			javaWindow = new JavaNotFound {
				DataContext = this
			};
			javaWindow.Owner = MainVM.MainWindow;
			javaWindow.ShowDialog();

			return !string.IsNullOrEmpty(JavaPath) ? JavaPath : null;
		}
	}
}
