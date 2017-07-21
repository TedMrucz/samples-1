namespace Ecommittees.Model
{
    using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.Concurrent;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;

	[Table("agenda_items")]
    public partial class AgendaItem
    {
		public AgendaItem()
		{
			Documents = new HashSet<Document>();
			AgendaItems = new HashSet<AgendaItem>();
			AgendaDocuments = new ConcurrentDictionary<string, Document>();
		}

		public int Id { get; set; }
		[JsonProperty(PropertyName = "agenda_id")]
		public int? AgendaId { get; set; }

		[Column(TypeName = "date"), DataType(DataType.Date), JsonProperty(PropertyName = "reference_date")]
		public DateTime? ReferenceDate { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "presenter")]
		public string Presenter { get; set; }
		[JsonProperty(PropertyName = "parent_id")]
		public int? ParentId { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }

		public bool Deleted { get; set; }
		[JsonProperty(PropertyName = "deleted_at")]
		public object DeletedAt { get; set; }
		[JsonProperty(PropertyName = "sort_order")]
		public int SortOrder { get; set; }

		public int Version { get; set; }
		[JsonProperty(PropertyName = "documents")]
		public ICollection<Document> Documents { get; set; }
		[JsonProperty(PropertyName = "agenda_items")]
		public ICollection<AgendaItem> AgendaItems { get; set; }
		[JsonIgnore]
		public ConcurrentDictionary<string, Document> AgendaDocuments { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public void EnumerateDocuments()
		{
			AddNestedItems(this);
		}

		private void AddNestedItems(AgendaItem nestedAgendaItem)
		{
			if (nestedAgendaItem.Documents != null)
			{
				foreach (var document in nestedAgendaItem.Documents)
				{
					AgendaDocuments[document.PlainSha256] = document;
				}
			}
			if (nestedAgendaItem.AgendaItems != null)
			{
				foreach (var agendaItem in nestedAgendaItem.AgendaItems)
				{
					AddNestedItems(agendaItem);
				}
			}
		}

		public int ReferenceCount(string shaKey)
		{
			var count = 0;
			foreach (var agendaItem in AgendaItems)
			{
				if (agendaItem.Documents != null)
				{
					var docs = agendaItem.Documents.Where(d => d.PlainSha256 == shaKey);
					if (docs.Any())
					{
						count = docs.Count();
					}
				}
				if (agendaItem.AgendaItems != null)
				{
					foreach (var agendaItemAgendaItem in agendaItem.AgendaItems)
					{
						agendaItemAgendaItem.ReferenceCount(shaKey);
					}
				}
			}
			return count;
		}
	}
}
