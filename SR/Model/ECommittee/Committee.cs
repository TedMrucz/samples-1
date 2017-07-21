using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
    [Table("committees")]
    public partial class Committee
    {
        public Committee()
        {
            CommitteeMembers = new HashSet<CommitteeMember>();
			Roles = new HashSet<Role>();

		}
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "descriptor")]
        public string Descriptor { get; set; }
		[JsonProperty(PropertyName = "committee_type")]
		public CommitteeType CommitteeType { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
		[JsonProperty(PropertyName = "committee_members")]
		public virtual ICollection<CommitteeMember> CommitteeMembers { get; set; }

		[JsonProperty(PropertyName = "committee_type_id")]
		public int CommitteeTypeId { get; set; }
	}
}
