using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebHost.SignalR
{
	[Authorize]
	public class RoadAssistant : Hub
	{
		public RoadAssistant()
		{
		}

		public override Task OnConnected()
		{
			return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
		}

		public override Task OnReconnected()
		{
			return Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
		}

		public Task Subscribe(int matchId)
		{
			return Groups.Add(Context.ConnectionId, "assistant");
		}

		public Task Unsubscribe(int matchId)
		{
			return Groups.Remove(Context.ConnectionId, "assistant");
		}

		public Task OnUsersJoined(UserDetails[] users)
		{
			return Clients.Client(Context.ConnectionId).InvokeAsync("UsersJoined", new[] { users });
		}

		public Task OnUsersLeft(UserDetails[] users)
		{
			return Clients.Client(Context.ConnectionId).InvokeAsync("UsersLeft", new[] { users });
		}

		public async Task SendToAll(string message)
		{
			await Clients.All.InvokeAsync("Send", Context.User.Identity.Name, message);
		}
		public async Task SendToOthers(string message)
		{
			await Clients.Others.InvokeAsync("Send", Context.User.Identity.Name, message);
		}

		//await Groups.AddAsync(topic);
		//await Clients.Group(message.Topic).InvokeAsync("Publish", message);
		//await Clients.Caller.InvokeAsync("Publish", message);

		//public class HubCallerContext
		//{
		//	public HubCallerContext(HttpRequest request, string connectionId);
		//	protected HubCallerContext();

		//	public virtual string ConnectionId { get; }
		//	public virtual IRequestCookieCollection RequestCookies { get; }
		//	public virtual IHeaderDictionary Headers { get; }
		//	public virtual IQueryCollection QueryString { get; }
		//	public virtual IPrincipal User { get; }
		//	public virtual HttpRequest Request { get; }
		//}
	}
}