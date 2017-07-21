using CallCentre.Data;
using CallCentre.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebHost.SignalR
{
	[HubName("MessangerHub")]
	public class MessangerHub : Hub
	{
		private readonly IDataContext dataContext;

		public MessangerHub(DataContext dataContext)
		{
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
			this.dataContext.Entity(new User { ConnectionId = Context.ConnectionId, UserId = userId, Group = "messager" });
			await this.dataContext.SaveChangesAsync();

			await Groups.Add(Context.ConnectionId, "messager");
		}

		public async Task Unsubscribe(int userId)
		{
			User user = this.dataContext.Users.FirstOrDefault(p => p.UserId == userId);
			if (user != null)
			{
				this.dataContext.Entity(user, EntityState.Deleted);
				await this.dataContext.SaveChangesAsync();
			}

			await Groups.Remove(Context.ConnectionId, "messager");
		}

		public async void PostMessage(Message message)
		{
			User user = this.dataContext.Users.FirstOrDefault(p => p.UserId == message.UserId);
			if (user != null)
				message.User_Id = user.Id;

			this.dataContext.Entity(message);
			await this.dataContext.SaveChangesAsync();
			await Clients.Others.Invoke("OnReceiveMessage", message);
		}
	}
}
