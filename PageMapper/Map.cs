using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace PageMapper
{
    public class Map
    {
        private PageMapperDataContext _db;

        private List<HtmlNode> _nodeMap;
        private string _baseUrl;
        private string _name;

        public Map(string baseUrl)
        {
            _db = new PageMapperDataContext();

            int secondSlash = baseUrl.IndexOf("/") + 1;
            string domain = baseUrl.Substring(secondSlash + 1);
            _name = domain.Substring(0, domain.IndexOf("/"));

            _baseUrl = baseUrl;
        }

        public Map(string name, string baseUrl)
        {
            _db = new PageMapperDataContext();
            _name = name;
            _baseUrl = baseUrl;
        }

        public List<HtmlNode> GetMap()
        {
            return _nodeMap;
        }

        public string GetMapAsText()
        {
            string map = "";

            foreach (HtmlNode node in _nodeMap)
            {
                map += string.Format("Name: {0}; ID: {1}" + Environment.NewLine, node.Name, node.Id);

                foreach (var attribute in node.Attributes)
                {
                    map += "  ";
                    map += string.Format("{0}: {1}" + Environment.NewLine, attribute.Name, attribute.Value);
                }

                map += Environment.NewLine;
            }

            return map;
        }

        public string GetText()
        {
            string trimmedText = "";
            string[] lumps = _nodeMap.Last().InnerText.Split(" ".ToCharArray());
            foreach (string lump in lumps)
            {
                if (!string.IsNullOrWhiteSpace(lump))
                    trimmedText += lump + " ";
            }

            return trimmedText;
        }

        public void SaveToDatabase()
        {
            PageMap pm;

            if (!_db.PageMaps.Any(m => m.Name == _name && m.BaseURL == _baseUrl))
            {
                pm = new PageMap();
                pm.PageMapID = Guid.NewGuid();
                pm.Name = _name;
                pm.BaseURL = _baseUrl;
                _db.PageMaps.InsertOnSubmit(pm);
            }
            else
            {
                pm = _db.PageMaps.Single(m => m.Name == _name && m.BaseURL == _baseUrl);
                _db.Nodes.DeleteAllOnSubmit(_db.Nodes.Where(n => n.PageMapID == pm.PageMapID));
            }

            int index = 0;
            foreach (var node in _nodeMap)
            {
                Node newNode = new Node();
                newNode.PageMapID = pm.PageMapID;
                newNode.NodeIndex = index;
                newNode.NodeName = node.Name;
                newNode.NodeID = node.Id;
                if (node.Attributes.Any(a => a.Name == "class"))
                {
                    newNode.NodeClass = node.Attributes.Single(a => a.Name == "class").Value;
                }

                _db.Nodes.InsertOnSubmit(newNode);

                index++;
            }

            _db.SubmitChanges();
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetNextNode(HtmlNode nextNode)
        {
            if (_nodeMap == null)
            {
                _nodeMap = new List<HtmlNode>();
            }

            _nodeMap.Add(nextNode);
        }

        public void SetNodes(List<HtmlNode> nodeMap)
        {
            this._nodeMap = nodeMap;
        }
    }
}
