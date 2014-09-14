using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HtmlAgilityPack;

namespace PageMapper
{
    public static class Mapper
    {
        private static HtmlWeb Browser;
        public static HtmlAgilityPack.HtmlDocument Document;

        public static Map GenerateMap(string name, string baseUrl, string query, string targetCode)
        {
            if (baseUrl == "" || targetCode == "")
            {
                throw new Exception("Please enter a URL and target code to search for.");
            }
            else
            {
                // Strange as it looks, this actually changes " to \" in the HTML
                targetCode = targetCode.Replace("\"", "\"");

                Map map = new Map(name, baseUrl);

                Browser = new HtmlWeb();
                Document = Browser.Load(baseUrl + query);

                HtmlNode searchNode = Document.DocumentNode.SelectSingleNode("//html/body");
                map.SetNextNode(searchNode);

                while (searchNode.InnerHtml.Contains(targetCode))
                {
                    bool canGoDeeper = false;

                    foreach (var child in searchNode.ChildNodes)
                    {
                        if (child.InnerHtml.Contains(targetCode))
                        {
                            searchNode = child;
                            canGoDeeper = true;

                            map.SetNextNode(child);

                            break;
                        }
                    }

                    if (!canGoDeeper)
                    {
                        break;
                    }
                }

                return map;
            }
        }
    }
}
