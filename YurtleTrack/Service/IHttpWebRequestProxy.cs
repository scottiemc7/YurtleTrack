﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

namespace YurtleTrack.Service
{
	public interface IHttpWebRequestProxy
	{
		string Method { get; set; }
		string ContentType { get; set; }
		long ContentLength { get; set; }
		CookieContainer CookieContainer { get; set; }
		WebHeaderCollection Headers { get; }

		Stream GetRequestStream();
		IHttpWebResponseProxy GetResponse();
	}
}
