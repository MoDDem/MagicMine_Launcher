using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI {
	class Error {
		public string ErrorTag { get; internal set; }
		public string ErrorMessage { get; internal set; }
		public Exception Exception { get; internal set; }
	}
}
