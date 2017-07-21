namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;

	[Table("meetings")]
    public partial class Meeting
    {
		public Meeting()
		{
			Attendances = new HashSet<Attendance>();
		}

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
		[JsonProperty(PropertyName = "committee_id")]
		public int CommitteeId { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime? CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime? UpdatedAt { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
		[JsonProperty(PropertyName = "starts_at")]
		public DateTime StartsAt { get; set; }
		[JsonProperty(PropertyName = "ends_at")]
		public DateTime EndsAt { get; set; }
		[JsonProperty(PropertyName = "deleted_at")]
		public DateTime? DeletedAt { get; set; }
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; set; }

        public bool Deleted { get; set; }
		[JsonProperty(PropertyName = "starts_at_was_changed")]
		public bool StartsAtWasChanged { get; set; }
		[JsonProperty(PropertyName = "ends_at_was_changed")]
		public bool EndsAtWasChanged { get; set; }
		[JsonProperty(PropertyName = "location_was_changed")]
		public bool LocationWasChanged { get; set; }

		public string Version { get; set; }
		[JsonProperty(PropertyName = "agenda")]
		public Agenda AgendaItem { get; set; }
		
		public ICollection<Attendance> Attendances { get; set; }

		public int? sharepoint_id { get; set; }

        public int? sharepoint_version { get; set; }
    }
}
