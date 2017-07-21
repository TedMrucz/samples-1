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
    public class TickerHub : Hub
    {
		public readonly ITicker ticker;
		private readonly IDataContext dataContext;

		public TickerHub(ITicker ticker, DataContext dataContext)
		{
			this.ticker = ticker;
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
			if (this.ticker != null && this.ticker.Clients == null)
			{
				this.ticker.Clients = Clients;
				this.ticker.Start();
			}
			if (this.dataContext != null)
			{
				this.dataContext.Entity(new User { ConnectionId = Context.ConnectionId, UserId = userId, Group = "ticker" });
				await this.dataContext.SaveChangesAsync();
			}

			await Groups.Add(Context.ConnectionId, "ticker");
		}

		public async Task Unsubscribe(int userId)
		{
			User user = this.dataContext.Users.FirstOrDefault(p => p.UserId == userId);
			if (user != null)
			{
				this.dataContext.Entity(user, EntityState.Deleted);
				await this.dataContext.SaveChangesAsync();
			}

			await Groups.Remove(Context.ConnectionId, "ticker");
		}
	}
}
