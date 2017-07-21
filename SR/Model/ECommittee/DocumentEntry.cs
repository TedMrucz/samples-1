namespace Ecommittees.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;


	public class DocumentEntry
	{
		[JsonProperty(PropertyName = "agenda_id")]
		public int AgendaId { get; set; }
		[JsonProperty(PropertyName = "manifest_entries")]
		public ManifestEntry[] ManifestEntries { get; set; }
	}
}
