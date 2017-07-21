using Newtonsoft.Json;
using System;


namespace ClientMessangerWpf
{
	public class Message
	{
		public Message()
		{
		}

		public Message(int id, string text)
		{
			Id = 0;
			UserId = id;
			Text = text;
			Time = DateTime.Now;
		}

		public int Id { get; set; }
		public int UserId { get; set; }
		public int User_Id { get; set; }
		public string Text { get; set; }
		public System.DateTime Time { get; set; }
	}
}
