using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YurtleTrack.Service
{
	class HttpWebRequestProxy : IHttpWebRequestProxy
	{
		private readonly HttpWebRequest _request;
		public HttpWebRequestProxy(string requestUriString) 
		{
			_request = (HttpWebRequest)WebRequest.Create(requestUriString);
		}

		public string Method
		{
			get
			{
				return _request.Method;
			}
			set
			{
				_request.Method = value;
			}
		}

		public string ContentType
		{
			get
			{
				return _request.ContentType;
			}
			set
			{
				_request.ContentType = value;
			}
		}

		public long ContentLength
		{
			get
			{
				return _request.ContentLength;
			}
			set
			{
				_request.ContentLength = value;
			}
		}

		public CookieContainer CookieContainer
		{
			get
			{
				return _request.CookieContainer;
			}
			set
			{
				_request.CookieContainer = value;
			}
		}

		public WebHeaderCollection Headers
		{
			get
			{
				return _request.Headers;
			}
		}

		public System.IO.Stream GetRequestStream()
		{
			return _request.GetRequestStream();
		}

		public IHttpWebResponseProxy GetResponse()
		{
			return new HttpWebResponseProxy((HttpWebResponse)_request.GetResponse());
		}
	}
}
