using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using DataProvider.Common;
using DataProvider.Service;
using DataProvider.Entities;

namespace DataProvider.Host
{
	[RoutePrefix("ef")]
	public class EFController : ApiController, IDataService
	{
		private readonly EFModel context;

		public EFController()
		{
			this.context = new EFModel();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.context.Dispose();

			base.Dispose(disposing);
		}
		[HttpGet, Route("GetRoleTypes")]
		public IList<RoleType> GetRoleTypes() => this.context.RoleTypes.ToList();
	}
}
