using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class VanillaRequests : Request {
		public override async Task<Response> PerformRequest() {
			var request = await HttpClient.GetAsync(@"https://launchermeta.mojang.com/mc/game/version_manifest.json");

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content?.ReadAsStringAsync();
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = rawMessage,
				StatusCode = httpCode,
				Json = (JObject) JsonConvert.DeserializeObject(rawMessage)
			};
		}

		public async Task<Response> GetVersionInfo(string url) {
			var request = await HttpClient.GetAsync(url);

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content?.ReadAsStringAsync();
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
