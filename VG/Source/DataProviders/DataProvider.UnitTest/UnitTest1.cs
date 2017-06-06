using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataProvider.Common;
using DataProvider.Entities;
using DataProvider.Service;

namespace DataProvider.UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public async Task Test1()
		{
			var nwDataProvider = new EFModel();
			var items = await Task.Factory.StartNew(() => nwDataProvider.RoleTypes);
			IList<RoleType> list = items.ToList();
			Assert.AreNotEqual(0, items.ToList().Count);
		}
	}
}
