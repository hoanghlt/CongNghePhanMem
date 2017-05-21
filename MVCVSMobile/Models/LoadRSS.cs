using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MVCVSMobile.Models
{
    public class LoadRSS
    {

       public string title;
        public string link;
        public string language;
        public string description;
        public string copyright;
        public string ttl;
        public string generator;
        public List<RSSItem> items = new List<RSSItem>();

        public LoadRSS(string url)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(url);

            XmlElement channel = doc["rss"]["channel"];
            XmlNodeList items = channel.GetElementsByTagName("item");
            this.title = channel["title"].InnerText;
            this.link = channel["link"].InnerText;
            this.description = channel["description"].InnerText;
            this.language = channel["language"].InnerText;
            this.copyright = channel["copyright"].InnerText;
            this.ttl = channel["ttl"].InnerText;
            this.generator = channel["generator"].InnerText;

            foreach (XmlNode item in items)
            {
                RSSItem rssItem = new RSSItem();
                rssItem.title = item["title"].InnerText;
                rssItem.description = item["description"].InnerText;
                rssItem.link = item["link"].InnerText;
                rssItem.pubDate = item["pubDate"].InnerText;
                this.items.Add(rssItem);
            }
        }
    }
}