namespace CallCentre.Model
{
	public partial class Connection : IEntity
	{
		public int Id { get; set; }
		public string ConnectionId { get; set; }
		public string UserAgent { get; set; }
		public bool Connected { get; set; }
		public int ClientId { get; set; }
		public virtual Client Caller { get; set; }
	}
}