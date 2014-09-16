using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace RITCHARD_Data
{
    public static class Mapper
    {
        private static HtmlWeb Browser;
        public static HtmlAgilityPack.HtmlDocument Document;

        private static Map GenerateMap(string baseUrl, string query, string target, bool isCode)
        {
            if (baseUrl == "" || target == "")
            {
                throw new Exception("Must enter a URL and target code to search for.");
            }
            else
            {
                // Strange as it looks, this actually changes " to \" in the HTML
                target = target.Replace("\"", "\"");

                // Used to handle non case-sensitive comparison
                CultureInfo culture = CultureInfo.CurrentCulture;

                Map map = new Map(baseUrl);

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
                    while (culture.CompareInfo.IndexOf(searchNode.InnerText, target, CompareOptions.IgnoreCase) >= 0)
                    {
                        bool canGoDeeper = false;

                        foreach (var child in searchNode.ChildNodes)
                        {
                            if (culture.CompareInfo.IndexOf(child.InnerText, target, CompareOptions.IgnoreCase) >= 0)
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

        public static Map GenerateMapFromCodeTarget(string baseUrl, string query, string targetCode)
        {
            if (targetCode.StartsWith("<span"))
            {
                throw new Exception("Cannot detect <span>s within pages. Please use a tag from higher up within the DOM.");
            }

            return GenerateMap(baseUrl, query, targetCode, true);
        }

        public static Map GenerateMapFromTextTarget(string baseUrl, string query, string targetText)
        {
            return GenerateMap(baseUrl, query, targetText, false);
        }

        public static List<string> GetRelevantTextFromDocumentUsingMap(HtmlDocument document, PageMap map) 
        {
            HtmlNode documentNode = document.DocumentNode.SelectSingleNode("//html");
            List<HtmlNode> documentNodesOfInterest = new List<HtmlNode>();
            List<string> textItems = new List<string>();

            foreach (Node mapNode in map.Nodes.OrderBy(n => n.NodeIndex))
            {
                if (mapNode.NodeIndex == map.MaxIndex)
                {
                    foreach (HtmlNode node in documentNode.ChildNodes.Where(n => n.Name == mapNode.NodeName && n.Id == mapNode.NodeID))
                    {
                        foreach (HtmlAttribute a in node.Attributes.AttributesWithName("class"))
                        {
                            string className = map.Nodes.Single(n => n.NodeIndex == map.MaxIndex).NodeClass;
                            if (a.Value == className)
                            {
                                textItems.Add(node.InnerText.Trim());
                            }
                        }
                    }
                }
                else
                {
                    documentNodesOfInterest = documentNode.ChildNodes.Where(n => n.Name == mapNode.NodeName && n.Id == mapNode.NodeID).ToList();

                    if (documentNodesOfInterest.Count == 1)
                    {
                        documentNode = documentNodesOfInterest.First();
                        continue;
                    }
                    else
                    {
                        foreach (HtmlNode docNode in documentNodesOfInterest)
                        {
                            foreach (HtmlAttribute a in docNode.Attributes.AttributesWithName("class"))
                            {
                                if (a.Value == mapNode.NodeClass)
                                {
                                    documentNode = docNode;
                                    break;
                                }
                            }
                            if (documentNode == docNode)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return textItems;
        }

        public static PageMap RetrieveMapFromDatabase(string name)
        {
            return new PageMapperDataContext().PageMaps.Single(m => m.Name == name);
        }
    }
}
