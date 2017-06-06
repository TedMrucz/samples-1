using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DataProvider.Entities;
using DataProvider.Common;

namespace DataProvider.Client
{
	[Export(typeof(IDataProvider))]
	public class DataProvider : DataAccess, IDataProvider
	{
		[ImportingConstructor]
		public DataProvider() : base()
		{ }
		public async Task<IList<RoleType>> GetRoleTypes() => await base.Get<IList<RoleType>>("GetRoleTypes");
		
	}
}
