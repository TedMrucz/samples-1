using Hierarchy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Xml.Serialization;

namespace Hierarchy.Models
{
    [Export("TreeModel", typeof(IModel))]
    public class TreeModel : IModel
    {
        public IList<TreeItem> Items { get; set; } = new List<TreeItem>();

        [ImportingConstructor]
        public TreeModel()
        {
            Items.Add(new TreeItem("One", "\uED43"));
            Items.Add(new TreeItem("Two", "\uED43"));
            var item = new TreeItem("Three", "\uED43");
            item.Items.Add(new TreeItem("3311", "\uEA98"));
            item.Items.Add(new TreeItem("3322", "\uEA98"));
            item.Items.Add(new TreeItem("3333", "\uEA98"));
            Items.Add(item);
            Items.Add(new TreeItem("Four", "\uED43"));
        }
    }

    [Serializable]
    public class  TreeItem
    {
        [XmlAttribute]
        public string Header { get; set; }
        [XmlAttribute]
        public string Glyph { get; set; } = "\uED43";
        [XmlArray]
        public IList<TreeItem> Items { get; set; } = new List<TreeItem>();

        public TreeItem()
        {
        }

        public TreeItem(string header, string glyph)
        {
            Header = header;
            Glyph = glyph;
        }
    }
}
