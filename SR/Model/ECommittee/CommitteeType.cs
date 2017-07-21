using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
	[Table("committee_types")]
	[DebuggerDisplay("{Description}")]
    public partial class CommitteeType
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
		[JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
		[JsonIgnore]
		public virtual ICollection<Committee> Committies { get; set; }
    }
}