using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace Hierarchy.Interfaces
{
    public interface IShellWnd
    {
        Window Shell { get; }
        CompositionContainer Container { set; }
    }
}