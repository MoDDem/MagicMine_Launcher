using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI {
	class Response {
		public HttpStatusCode StatusCode { get; internal set; }
		public bool IsSuccess { get; internal set; }
		public string RawMessage { get; internal set; }
		public Error Error { get; internal set; }

		public Response() {
			Error = Error ?? new Error();
		}
	}
}
