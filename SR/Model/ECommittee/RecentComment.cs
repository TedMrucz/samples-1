using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ecommittees.Model
{
    public class RecentComment
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "author_id")]
        public int AuthorId { get; set; }
        [JsonProperty(PropertyName = "document_id")]
        public int DocumentId { get; set; }
        [JsonProperty(PropertyName = "conversation_id")]
        public int ConversationId { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        //public DocumentViewModel DocumentName { get; set; }
        public string DocumentName { get; set; }
        public string AuthorName { get; set; }
    }
}
