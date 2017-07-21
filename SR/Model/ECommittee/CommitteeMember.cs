using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
	[Table("committee_members")]
    public partial class CommitteeMember
    {
        public int Id { get; set; }
		[JsonProperty(PropertyName = "committee_id")]
		public int CommitteeId { get; set; }

		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }

        public virtual Person Member { get; set; }

        public virtual Role Role { get; set; }
		[JsonIgnore]
		public virtual Committee Committee { get; set; }
		[JsonIgnore]
		public int MemberId { get; set; }
		[JsonIgnore]
		public int RoleId { get; set; }
	}
}
