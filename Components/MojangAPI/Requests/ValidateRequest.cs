using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class ValidateRequest : Request {
		class Query {
			[JsonProperty("accessToken")]
			public string AccessToken { get; set; }
			[JsonProperty("clientToken")]
			public string ClientToken { get; set; }

			public Query(string accessToken, string clientToken) {
				AccessToken = accessToken;
				ClientToken = clientToken;
			}
		}
		private Query query;

		public override async Task<Response> PerformRequest() {
			string json = JsonConvert.SerializeObject(query, Formatting.Indented);

			var request = await HttpClient.PostAsync(@"https://authserver.mojang.com/validate", new StringContent(json, Encoding.UTF8, "application/json"));

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content?.ReadAsStringAsync();
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = rawMessage,
				StatusCode = httpCode,
				Json = null
			};
		}

		public ValidateRequest(string accessToken, string clientToken) => query = new Query(accessToken, clientToken);
	}
}
