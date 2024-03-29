﻿using MagicMine_Launcher.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	class CurseForgeRequest : Request {
		class Query {
			/*
			 * searchFilter - text to search in headline, 
			 * categoryId - hardcore, magic, hitech, etc.
			 * gameId - minecraft id:432
			 * gameVersion - version of game
			 * index - looks like it's a start index of instances
			 * pageSize - i guess it's a how much instances will be recieved
			 * sectionId - id of section. Like a modpacks(4471), maps, textures
			 * sort - id of sorting ???? 1 - popularity & 3 - name & 2 - last updated & total dwnlds = 5
			*/
			[JsonProperty("searchFilter")]
			public string SearchFilter { get; set; }

			[JsonProperty("categoryId")]
			public InstanceCategory CategoryId { get; set; }

			[JsonProperty("gameVersion")]
			public string GameVersion { get; set; }

			[JsonProperty("index")]
			public int Index { get; set; }

			[JsonProperty("sort")]
			public InstanceSort Sort { get; set; }
		}
		private Query query;

		public async override Task<Response> PerformRequest() {
			int sortInt = 1;
			switch(query.Sort) {
				case InstanceSort.Trending:
					sortInt = 1;
					break;
				case InstanceSort.Popular:
					sortInt = 5;
					break;
				case InstanceSort.Updated:
					sortInt = 2;
					break;
				case InstanceSort.Name:
					sortInt = 3;
					break;
			}

			string url = $@"https://addons-ecs.forgesvc.net/api/v2/addon/search?gameId=432&sectionId=4471&pageSize=20&categoryId={(int)query.CategoryId}&index={query.Index}&sort={sortInt}";
			_ = query.SearchFilter != null ? url += "&searchFilter=" + query.SearchFilter : null;
			_ = query.GameVersion != null ? url += "&gameVersion=" + query.GameVersion : null;

			var request = await HttpClient.GetAsync(url);

			bool isSuccess = request.IsSuccessStatusCode;
			string rawMessage = await request.Content.ReadAsStringAsync();
			var httpCode = request.StatusCode;

			return new Response {
				IsSuccess = isSuccess,
				RawMessage = rawMessage,
				StatusCode = httpCode,
				Json = new JObject(new JProperty("list", JsonConvert.DeserializeObject(rawMessage)))
			};
		}

		public CurseForgeRequest(string search = null, string version = null, InstanceCategory category = 0, int index = 0, InstanceSort sort = InstanceSort.Trending) => query = new Query { 
			SearchFilter = search,
			GameVersion = version,
			CategoryId = category,
			Index = index,
			Sort = sort
		};
	}
}
