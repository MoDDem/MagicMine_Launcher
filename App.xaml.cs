using MagicMine_Launcher.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher {
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application {
		private new MainWindow MainWindow;

		private void Application_Startup(object sender, StartupEventArgs e) {
			MainViewModel mainVM = new MainViewModel();
			MainWindow = new MainWindow { DataContext = mainVM };
			MainWindow.Show();
		}
	}
}
