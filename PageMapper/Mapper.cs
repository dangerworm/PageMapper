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

        private static Map GenerateMap(string name, string baseUrl, string query, string target, bool isCode)
        {
            if (baseUrl == "" || target == "")
            {
                throw new Exception("Must enter a URL and target code to search for.");
            }
            else
            {
                // Strange as it looks, this actually changes " to \" in the HTML
                target = target.Replace("\"", "\"");

                Map map = new Map(name, baseUrl);

                Browser = new HtmlWeb();
                Document = Browser.Load(baseUrl + query);

                HtmlNode searchNode = Document.DocumentNode.SelectSingleNode("//html/body");
                map.SetNextNode(searchNode);

                if (isCode)
                {
                    while (searchNode.InnerHtml.Contains(target))
                    {
                        bool canGoDeeper = false;

                        foreach (var child in searchNode.ChildNodes)
                        {
                            if (child.InnerHtml.Contains(target))
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
                }
                else
                {
                    while (searchNode.InnerText.Contains(target))
                    {
                        bool canGoDeeper = false;

                        foreach (var child in searchNode.ChildNodes)
                        {
                            if (child.InnerText.Contains(target))
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
                }

                return map;
            }
        }

        public static Map GenerateMapFromCodeTarget(string name, string baseUrl, string query, string targetCode)
        {
            return GenerateMap(name, baseUrl, query, targetCode, true);
        }

        public static Map GenerateMapFromTextTarget(string name, string baseUrl, string query, string targetText)
        {
            return GenerateMap(name, baseUrl, query, targetText, false);
        }
    }
}
