using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Hubs;


namespace WebHost.SignalR
{
	public class Calculator : ICalculator
	{
		public IHubCallerConnectionContext<dynamic> Clients { get; set; }

		void ICalculator.Calculate(IList<decimal> items)
		{
			decimal result = 0M;

			if (0 < items.Count)
				result = items.Sum<decimal>(p => p);

			if (Clients != null)
			{
				Clients.Caller.OnResult(result);
			}
		}
	}
}
