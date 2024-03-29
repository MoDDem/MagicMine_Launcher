﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MagicMine_Launcher.Components.MojangAPI.Requests {
	abstract class Request {
		public abstract Task<Response> PerformRequest();
		public static TimeSpan Timeout = TimeSpan.FromSeconds(10);
		public static HttpClient HttpClient { 
			get => new HttpClient() { Timeout = Request.Timeout }; 
			private set => HttpClient = value; 
		}

		public Request() {
			HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
	}
}
