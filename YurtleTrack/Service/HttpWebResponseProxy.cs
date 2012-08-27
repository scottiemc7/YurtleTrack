﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YurtleTrack.Service
{
	class HttpWebResponseProxy : IHttpWebResponseProxy
	{
		private readonly HttpWebResponse _response;
		public HttpWebResponseProxy(HttpWebResponse actualResponse)
		{
			_response = actualResponse;
		}

		public System.Net.CookieCollection Cookies
		{
			get
			{
				return _response.Cookies;
			}
			set
			{
				_response.Cookies = value;
			}
		}

		public System.IO.Stream GetResponseStream()
		{
			return _response.GetResponseStream();
		}

		public void Dispose()
		{
			_response.Close();
		}

		public HttpStatusCode StatusCode
		{
			get { return _response.StatusCode; }
		}

		public WebHeaderCollection Headers
		{
			get
			{
				return _response.Headers;
			}
		}
	}
}
