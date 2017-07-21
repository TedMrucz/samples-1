using Microsoft.AspNetCore.SignalR.Hubs;

namespace WebHost.SignalR
{
	public interface ITicker
	{
		IHubCallerConnectionContext<dynamic> Clients { get; set; }
		void Start();
	}
}