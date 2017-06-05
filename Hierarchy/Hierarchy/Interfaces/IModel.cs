using Hierarchy.Models;
using System.Collections.Generic;

namespace Hierarchy.Interfaces
{
    public interface IModel
    {
        IList<TreeItem> Items { get; set; }
    }
}
