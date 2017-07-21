using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Hubs;


namespace WebHost.SignalR
{
	public interface ICalculator
	{
		IHubCallerConnectionContext<dynamic> Clients { get; set; }

		void Calculate(IList<decimal> items);
	}
}
