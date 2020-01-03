using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class Query {
		public Dictionary<string, object> Agent { get; private set; } = new Dictionary<string, object> {
			{ "Name", "Minecraft" },
			{ "Version", 1f }
		};

		public string Username { get; set; }
		public string Password { get; set; }

		public string ClientToken { get; private set; }
		public bool RequestUser { get; private set; } = true;

		public Query(string username, string password) {
			Username = username;
			Password = password;
		}
	}

	class AuthRequest : Request {
		private Query Query { get; set; }

		public async override Task<Response> PerformRequest() {
			string json = JsonConvert.SerializeObject(Query, Formatting.Indented);
			MessageBox.Show(json);
			var request = await HttpClient.PostAsync(@"https://authserver.mojang.com/authenticate", new StringContent(json, Encoding.UTF8, "application/json"));

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content.ReadAsStringAsync();
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = rawMessage,
				StatusCode = httpCode
			};
		}

		public AuthRequest(Query query) => Query = query;
	}
}
