using System;
using System.Collections.Generic;

namespace CallCentre.Model
{
	public partial class Client : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid Identifier { get; set; }
		public bool IsOnline { get; set; }

		public virtual HashSet<Connection> Connections { get; set; } = new HashSet<Connection>();
	}
}
