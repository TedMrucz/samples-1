namespace CallCentre.Model
{
	public partial class CallCentre : IEntity
	{
		public int Id { get; set; }
		public string UserId { get; set; }

		public User  User { get; set; }
}
}
