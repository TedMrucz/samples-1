using CallCentre.Data;
using CallCentre.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebHost.WebAPI
{
	public class CallerCentreController : Controller
	{
		private readonly IDataContext dataContext;

		public CallerCentreController(DataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		//[Authorize(Roles="User")]
		public string Hello(string name) => $"Hello, {name}";

		public IList<Message> GetMessages() => this.dataContext.Messages.ToList();

		public IList<User> GetUsers() => this.dataContext.Users.ToList();
	}
}
