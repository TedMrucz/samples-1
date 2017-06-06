using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using VGTrader.Common;
using VGTrader.Entities;

namespace VGTrader
{
	[Serializable, Export(typeof(IViewContext))]
	public class ViewContext : IViewContext
	{
		private static ViewContext viewContext = null;
		
		public CurrentUserParam CurrentUserParam { get; set; }
		public CompositionContainer Container { get; set; }
		
		public ViewContext()
		{
			viewContext = this;
		}

		public void Initialize()
		{
			viewContext = this;
		}
		public static ViewContext Current
		{
			get
			{
				if (viewContext == null)
					return new ViewContext();

				return viewContext;
			}
		}

		public IViewContext GetCurrent()
		{
			return viewContext as IViewContext;
		}

		//public void LoadConfigItem(string xmlFile, string topElementName, string subElementName, IViewModelBase view)
		//{
		//	if (this.configParser != null)
		//	{
		//		configParser.LoadItem(xmlFile, topElementName, subElementName, view, this);
		//	}
		//}

		//public void SerializeRegionViews(IList<IRegionView> views, string regionName)
		//{
		//	if (this.configParser != null)
		//	{
		//		configParser.SerializeRegionViews(views, regionName);
		//	}
		//}

		//public IList<IRegionView> DeSerializeRegionViews(string regionName)
		//{
		//	IList<IRegionView> views = new List<IRegionView>();
		//	if (this.configParser != null)
		//	{
		//		views = configParser.DeSerializeRegionViews(regionName);
		//	}
		//	return views;
		//}
	}
}
