
using System;

namespace WebHost.SignalR
{
	public class Message
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Text { get; set; }
		public string ConnectionId { get; set; }
		public DateTime Time { get; set; }
	}
}
