using System;
using VGTrader.Entities;

namespace VGTrader.Common
{
	public interface IViewContext
	{
		//ITileBarModel TileBar { get; }
		//IMenuBarModel MenuBar { get; }
		//IPopupModel Popup { get; }
		//IPopupModel PopupView { get; }
		//IPopupModel RegionModal { get; }
		//IPopupModel FloatCanvas { get; }
		//IPopupModel HelpDoc { get; }
		//ITextBlocksModel TextArray { get; }
		//IStatusBarService StatusBarService { get; }

		void Initialize();
		//void LoadConfigItem(string xmlFile, string topElementName, string subElementName, IViewModelBase view);
		IViewContext GetCurrent();
		CurrentUserParam CurrentUserParam { get; set; }
		//CompositionContainer Container { get; set; }

		//void SerializeRegionViews(IList<IRegionView> views, string regionName);
		//IList<IRegionView> DeSerializeRegionViews(string regionName);

		//IList<IViewBase> Floats { get; set; }
	}
}
