using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class AuthRequest : Request {
		public async override Task<Response> PerformRequest() {
			return new Response {
				IsSuccess = true,
				RawMessage = "test",
				StatusCode = System.Net.HttpStatusCode.OK
			};
		}
	}
}
