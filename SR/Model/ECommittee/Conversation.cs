using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
    [Table("conversations")]
    public partial class Conversation : IEntity
	{
        public Conversation()
        {
            Members = new HashSet<Participant>();
            Messages = new HashSet<Message>();
			Participants = new HashSet<int>();
		}

		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }
		[JsonProperty(PropertyName = "document_id")]
		public int DocumentId { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "document_plain_sha_256")]
		public string DocumentPlainSha256 { get; set; }
		[JsonProperty(PropertyName = "owner_id")]
		public int OwnerId { get; set; }
		[JsonProperty(PropertyName = "document_reference_x")]
		public int DocumentReferenceX { get; set; }
		[JsonProperty(PropertyName = "document_reference_y")]
		public int DocumentReferenceY { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "visibility")]
		public string Visibility { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
		[JsonProperty(PropertyName = "_deleted")]
		public bool Deleted { get; set; }

		[JsonProperty(PropertyName = "participants")]
		public virtual ICollection<int> Participants { get; set; }

		[JsonProperty(PropertyName = "messages")]
		public virtual ICollection<Message> Messages { get; set; }

		[JsonIgnore]
		public virtual ICollection<Participant> Members { get; set; }
		public void UpdateVisualMessageCollection()
		{
			ViewMessages = new ObservableCollection<Model.Message>(Messages);
            MessagesCount = ViewMessages.Count;
            ParticipantsCount = Participants.Count;
        }

		[JsonIgnore]
		public ObservableCollection<Message> ViewMessages { set; get; }  = new ObservableCollection<Message>();

		[JsonIgnore]
		public Person Owner { get; set; }

        [JsonIgnore]
        public string Message { set; get; }

        [JsonIgnore]
        public int MessagesCount { set; get; }

        [JsonIgnore]
        public int ParticipantsCount { set; get; }

		[JsonIgnore]
		public bool IsMessageTab { set; get; }

		[JsonIgnore]
		public bool IsMemberTab { set; get; }
	}
}
