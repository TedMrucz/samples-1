namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;

	[Table("agendas")]
    public partial class Agenda
    {
		public Agenda()
		{
			AgendaItems = new HashSet<AgendaItem>();
            Documents = new List<Document>();
        }
		public int Id { get; set; }
		[JsonProperty(PropertyName = "meeting_id")]
		public int MeetingId { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
		[JsonProperty(PropertyName = "_deleted")]
		public bool Deleted { get; set; }
        [JsonProperty(PropertyName = "deleted_at")]
        [DataType(DataType.Date)]
        public DateTime? DeletedAt { get; set; }
        [JsonProperty(PropertyName = "version")]
		public int Version { get; set; }
		[JsonProperty(PropertyName = "agenda_items")]
		public ICollection<AgendaItem> AgendaItems { get; set; }

		[StringLength(255), JsonIgnore]
		public string state { get; set; }
		[JsonIgnore]
		public int? sharepoint_id { get; set; }
		[JsonIgnore]
		public int? sharepoint_version { get; set; }

        private List<Document> Documents { get; set; }

        public List<Document> GetNestedDocumentList()
        {
            if (this.Documents.Count < 1)
            {
                foreach (var agendaItem in AgendaItems)
                {
                    AddNestedItems(agendaItem);
                }
            }
            return Documents;
        }

        public void AddNestedItems(AgendaItem item)
        {
            if (item.Documents != null)
            {
                foreach (var agendaItemDocument in item.Documents)
                {
                    Documents.Add(agendaItemDocument);
                }
            }
            if (item.AgendaItems != null)
            {
                foreach (var agendaItem in item.AgendaItems)
                {
                    AddNestedItems(agendaItem);
                }
            }
        }
    }
}
