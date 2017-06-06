using System;

namespace VGTrader.Common
{
    public interface IViewBase
    {
        IViewModelBase Model { set; get; }
        string ViewName { set; get; }
		string RegionName { set; get; }
        string ViewTitle { set; get; }
        bool IsActive { set; get; }
    }
}
