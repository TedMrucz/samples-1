using System;
using System.Collections.Generic;
using DataProvider.Entities;

namespace DataProvider.Common
{
	public interface IDataService
	{
		IList<RoleType> GetRoleTypes();
	}
}
