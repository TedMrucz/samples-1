using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataProvider.Entities;

namespace DataProvider.Common
{
    public interface IDataProvider
    {
		Task<IList<RoleType>> GetRoleTypes();

	}
}
