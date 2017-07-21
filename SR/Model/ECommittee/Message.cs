using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
    [Table("messages")]
    public partial class Message : IEntity
	{
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }
		[JsonProperty(PropertyName = "conversation_id")]
		public int ConversationId { get; set; }
		[JsonProperty(PropertyName = "author_id")]
		public int AuthorId { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }

		[StringLength(255), JsonIgnore]
        public string Ancestry { get; set; }
		[JsonIgnore]
		public string Annotation { get; set; }
		[JsonIgnore]
		public string OwnerName { get; set; }
		[JsonIgnore]
		public virtual Conversation Conversation { get; set; }
	}
}
