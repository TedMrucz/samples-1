namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;

	[Table("roles")]
    public partial class Role
    {
		public Role()
		{
			CommitteeMembers = new HashSet<CommitteeMember>();
		}
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "description")]
		public string Description { get; set; }
		[JsonIgnore]
		public virtual ICollection<CommitteeMember> CommitteeMembers { get; set; }
	}
}
