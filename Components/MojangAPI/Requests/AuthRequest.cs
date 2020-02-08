using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class AuthRequest : Request {
		class Query {
			[JsonProperty("agent")]
			public Dictionary<string, object> Agent { get; private set; } = new Dictionary<string, object> {
				{ "name", "Minecraft" },
				{ "version", 16 }
			};

			[JsonProperty("username")]
			public string Username { get; set; }
			[JsonProperty("password")]
			public string Password { get; set; }

			[JsonProperty("clientToken")]
			public string ClientToken { get; set; }
			[JsonProperty("requestUser")]
			public bool RequestUser { get; private set; } = true;

			public Query(string username, string password, string clientToken) {
				Username = username;
				Password = password;
				ClientToken = clientToken;
			}
		}
		private Query query;

		public async override Task<Response> PerformRequest() {
			string json = JsonConvert.SerializeObject(query, Formatting.Indented);
			
			var request = await HttpClient.PostAsync(@"https://authserver.mojang.com/authenticate", new StringContent(json, Encoding.UTF8, "application/json"));

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

		public AuthRequest(string username, string password, string clientToken) => query = new Query(username, password, clientToken);
	}
}
