﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace YurtleTrack.Service
{
	public interface IHttpWebResponseProxy : IDisposable
	{
		CookieCollection Cookies { get; set; }
		WebHeaderCollection Headers { get; }
		HttpStatusCode StatusCode { get; }

		Stream GetResponseStream();
	}
}
