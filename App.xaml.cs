using MagicMine_Launcher.View;
using MagicMine_Launcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher {
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application {
		private void Application_Startup(object sender, StartupEventArgs e) {
			new MainViewModel();
			Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
		}

		private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
			string path = AppDomain.CurrentDomain.BaseDirectory + "CrashReport.txt";

			using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
				if(fs.CanSeek) {
					fs.Seek(fs.Length, SeekOrigin.Begin);
				}
				string _str = $"---New Exception Catched---\n{DateTime.Now}\n-Message:\n\u0020\u0020\u0020{e.Exception.Message}\n-------\n-StackTrace:\n{e.Exception.StackTrace}\n-------\n-Source:\n\u0020\u0020\u0020{e.Exception.Source}\n---End of Exception log---\n\n";
				byte[] str = Encoding.Default.GetBytes(_str);
				fs.Write(str, 0, str.Length);
			}
		}
	}
}
