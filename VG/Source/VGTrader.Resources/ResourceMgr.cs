using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Diagnostics;
using System.Resources;
using System.Threading;
using VGTrader.Common;


namespace VGTrader.Resources
{
	[Export(typeof(IResourceMgr)), PartCreationPolicy(CreationPolicy.Shared)]
	public class ResourceMgr : IResourceMgr
	{
		public static ResourceManager resourceManager = null;

		public ResourceMgr() { }

		public static ResourceManager Init()
		{
			if (resourceManager == null)
			{
				resourceManager = new ResourceManager("VGTrader.Resources.Resource", typeof(ResourceMgr).Assembly);
			}

			return resourceManager;
		}

		public ResourceManager Initialize()
		{
			if (resourceManager == null)
			{
				resourceManager = new ResourceManager("VGTrader.Resources.Resource", typeof(ResourceMgr).Assembly);
			}

			return resourceManager;
		}
		/// <summary>
		/// access to resource manager
		/// </summary>
		public static ResourceManager GetResourceManager
		{
			get { return ResourceMgr.resourceManager; }
		}

		/// <summary>
		/// gets string resource
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetString(string value)
		{
			return ResourceMgr.GetString(value, string.Empty);
		}

		public string GetStringID(string value)
		{
			return ResourceMgr.GetString(value, string.Empty);
		}

		/// <summary>
		/// gets string resource
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetString(string value, string cultureName)
		{
			try
			{
				if (string.IsNullOrEmpty(value))
					return "Err";

				if (string.IsNullOrEmpty(cultureName))
				{
					if (resourceManager == null)
						ResourceMgr.Init();

					return resourceManager.GetString(value);
				}
				else if (Thread.CurrentThread.CurrentCulture.Name.Equals("en-US"))
				{
					return resourceManager.GetString(value, Thread.CurrentThread.CurrentCulture);
				}
				else
				{
					CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(cultureName);
					return resourceManager.GetString(value, cultureInfo);
				}
			}
			catch (MissingManifestResourceException ex)
			{
				Trace.TraceError(ex.Message);
			}
			return value;
		}
	}
}
