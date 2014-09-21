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
                //throw new Exception("Cannot detect <span>s within pages. Please use a tag from higher up within the DOM.");
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
                // If we're at the start of the map, find where to go next
                if (mapNode.NodeIndex < map.MaxIndex)
                {
                    // Find out which child nodes have the right name and ID.
                    documentNodesOfInterest = documentNode.ChildNodes.Where(n => n.Name == mapNode.NodeName && n.Id == mapNode.NodeID).ToList();

                    // If there's only 1, use that.
                    if (documentNodesOfInterest.Count == 1)
                    {
                        documentNode = documentNodesOfInterest.First();
                        continue;
                    }
                    // If there are more, find out which ones have the right class
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

                        // If none had the right class, they won't have been put in documentNode
                        if (!documentNodesOfInterest.Contains(documentNode))
                        {
                            // Try to match on the ID of a child node in the map
                            foreach (HtmlNode docNode in documentNodesOfInterest)
                            {
                                Node nextNode = map.Nodes.Where(n => n.NodeIndex > mapNode.NodeIndex && !string.IsNullOrEmpty(n.NodeID)).OrderBy(n => n.NodeIndex).FirstOrDefault();
                                if (nextNode != null && docNode.InnerHtml.Contains(nextNode.NodeID))
                                {
                                    documentNode = docNode;
                                    break;
                                }
                            }

                            // If we still haven't found anything, it's usually because the
                            // loop has hit a listitem. Loop through from here, see what we
                            // get and don't bother going deeper.
                            if (!documentNodesOfInterest.Contains(documentNode))
                            {
                                foreach (HtmlNode node in documentNodesOfInterest.Where(n => n.Name == mapNode.NodeName && n.Id == mapNode.NodeID))
                                {
                                    {
                                        textItems.Add(node.InnerText.Trim());
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
                // If we're at the end of the map, start delving into the text
                else
                {
                    foreach (HtmlNode node in documentNode.ChildNodes.Where(n => n.Name == mapNode.NodeName && n.Id == mapNode.NodeID))
                    {
                        // Without class information, the next bit is useless. Grab any old text.
                        if (mapNode.NodeClass == null)
                        {
                            {
                                textItems.Add(node.InnerText.Trim());
                            }
                        }
                        else
                        {
                            // If we have class information, only pull the matching ones.
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
