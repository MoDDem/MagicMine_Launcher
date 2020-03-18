using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using MagicMine_Launcher.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class TechnicPackRequest : Request {
		class Query {
			/*
			 * SearchFilter - text to search in headline, 
			 * InstanceSort - sort filter
			 * Page - modpacks page
			*/
			public string SearchFilter { get; set; }
			public InstanceSort InstanceSort { get; set; }
			public int Page { get; set; }
		}
		private Query query;

		private List<Dictionary<string, object>> itemCollection;

		public override async Task<Response> PerformRequest() {
			string url = $@"https://www.technicpack.net/modpacks/sort-by/{query.InstanceSort.ToString().ToLower()}?page={query.Page}";
			if(query.SearchFilter != null)
				url += "&q=" + query.SearchFilter;

			var request = await HttpClient.GetAsync(url);

			bool isSuccess = request.IsSuccessStatusCode;
			var json = await ConvertHtmlToJson(await request.Content.ReadAsStringAsync());
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = JsonConvert.SerializeObject(json),
				StatusCode = httpCode,
				Json = json
			};
		}

		private async Task<JObject> ConvertHtmlToJson(string rawMessage) {
			itemCollection = new List<Dictionary<string, object>>();

			var document = await BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(rawMessage));

			var modpacks = document.QuerySelectorAll(".modpack-item");
			foreach(var item in modpacks) {
				string url, img, title;
				if(item.FindChild<IHtmlDivElement>().ClassList.Contains("modpack-meta")) {
					var obj = item.FindChild<IHtmlDivElement>().Children[0].Children[0];

					title = obj.TextContent;
					url = obj.GetAttribute("href");
					img = null;
				} else {
					var obj = item.FindChild<IHtmlDivElement>().FirstElementChild;

					url = obj.GetAttribute("href");
					img = obj.FindChild<IHtmlImageElement>().GetAttribute("data-cfsrc");
					title = obj.FindChild<IHtmlImageElement>().GetAttribute("title");
				}

				itemCollection.Add(new Dictionary<string, object> {
					{ "title", title },
					{ "url", url },
					{ "img", img },
				});
			}

			return new JObject(new JProperty("list", JArray.FromObject(itemCollection)));
		}

		public TechnicPackRequest(string search = null, InstanceSort sorting = InstanceSort.Trending, int page = 1) => query = new Query {
			SearchFilter = search,
			InstanceSort = sorting,
			Page = page
		};
	}
}
