using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class ATLauncherRequest : Request {
		public async override Task<Response> PerformRequest() {
			var request = await HttpClient.GetAsync(@"https://api.atlauncher.com/v1/packs/full/all/");

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content.ReadAsStringAsync();
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = rawMessage,
				StatusCode = httpCode,
				Json = (JObject) JsonConvert.DeserializeObject(rawMessage)
			};
		}
	}
}
