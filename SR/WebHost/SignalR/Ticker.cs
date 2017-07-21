using Microsoft.AspNetCore.SignalR.Hubs;
using System;

namespace WebHost.SignalR
{
	public class Ticker : ITicker
    {
		private Random rand = new Random();
		private System.Threading.Timer timer;

		public IHubCallerConnectionContext<dynamic> Clients { get; set; }

		public Ticker()
		{
		}

		public void Start()
		{
			if (timer == null)
				timer = new System.Threading.Timer(OnTimer, null, 2000, 2000);
		}

		public void OnTimer(object count)
		{
			if (Clients != null)
			{
				var tick = rand.Next(0, 100).ToString();
				Clients.All.OnNextTick(tick);
			}
		}
	}
}
