using System;

namespace VGTrader.Entities
{
	public class CurrentUserParam
	{
		public CurrentUserParam(string loginName, int userRoleLevel, string userRole)
		{
			this.LoginName = loginName;
			this.UserRole = userRole;
			this.UserRoleLevel = userRoleLevel;
		}
		public string UserName;
		public string LoginName;
		public string UserRole;
		public int UserRoleLevel;
	}
}
