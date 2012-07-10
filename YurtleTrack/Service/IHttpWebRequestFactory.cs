﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YurtleTrack.Service
{
    public interface IHttpWebRequestFactory
    {
        IHttpWebRequestProxy Create(string requestUriString);
    }
}
