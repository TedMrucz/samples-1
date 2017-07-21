using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using CallCentre.Model;
using CallCentre.Data;

namespace WebHost.SignalR
{
    public class CalculatorHub : Hub
    {
		public readonly ICalculator calculator;
		private readonly IDataContext dataContext;

		public CalculatorHub(ICalculator calculator, DataContext dataContext)
		{
			this.calculator = calculator;
			this.dataContext = dataContext;
			if (dataContext == null)
				throw new ArgumentNullException("DataContext dataContext == null");
		}

		public override Task OnConnected()
		{
			return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			return Task.CompletedTask;
		}

		public override Task OnReconnected()
		{
			return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
		}

		public async Task Subscribe(int userId)
		{
			if (this.calculator != null && this.calculator.Clients == null)
			{
				this.calculator.Clients = Clients;
			}
			if (this.dataContext != null)
			{
				this.dataContext.Entity(new User { ConnectionId = Context.ConnectionId, UserId = userId, Group = "calculator" });
				await this.dataContext.SaveChangesAsync();
			}

			await Groups.Add(Context.ConnectionId, "calculator");
		}

		public async Task Unsubscribe(int userId)
		{
			User user = this.dataContext.Users.FirstOrDefault(p => p.UserId == userId);
			if (user != null)
			{
				this.dataContext.Entity(user, EntityState.Deleted);
				await this.dataContext.SaveChangesAsync();
			}

			await Groups.Remove(Context.ConnectionId, "calculator");
		}

		public void Calculate(IList<decimal> items)
		{
			this.calculator.Calculate(items);
		}
	}
}
