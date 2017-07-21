using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
    [Table("participants")]
    public partial class Participant
	{
		[JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

		[JsonProperty(PropertyName = "member_id")]
		public int? MemberId { get; set; }

		[JsonProperty(PropertyName = "conversation_id")]
		public int? ConversationId { get; set; }
		[JsonProperty(PropertyName = "deleted_at")]
		public DateTime? DeletedAt { get; set; }
		[JsonIgnore]
		public virtual Conversation Conversation { get; set; }
	}
}
