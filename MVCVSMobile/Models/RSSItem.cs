﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCVSMobile.Models
{
    public class RSSItem
    {
        public string title { get; set; }

        public string link { get; set; }

        public string description { get; set; }

        public string pubDate { get; set; }
    }
}