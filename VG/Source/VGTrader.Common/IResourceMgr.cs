using System;
using System.Resources;

namespace VGTrader.Common
{
	public interface IResourceMgr
	{
		ResourceManager Initialize();
		string GetStringID(string resID);
	}
}