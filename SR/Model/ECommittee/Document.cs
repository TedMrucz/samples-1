namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using Newtonsoft.Json;


	[Table("documents")]
    public partial class Document
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "sharepoint_version")]
        public int Version { get; set; }
		[JsonProperty(PropertyName = "sort_order")]
		public int SortOrder { get; set; }
		[JsonProperty(PropertyName = "created_at")]
		public DateTime CreatedAt { get; set; }
		[JsonProperty(PropertyName = "updated_at")]
		public DateTime UpdatedAt { get; set; }
		[JsonProperty(PropertyName = "deleted_at")]
		public DateTime? DeletedAt { get; set; }
		[StringLength(255), JsonProperty(PropertyName = "encrypted_sha_256")]
		public string EncryptedSha256 { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "plain_sha_256")]
		public string PlainSha256 { get; set; }
		[JsonProperty(PropertyName = "replaced_by_document_id")]
		public int? ReplacedByDocumentId { get; set; }
		[JsonProperty(PropertyName = "_deleted")]
		public bool Deleted { get; set; }
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
        [JsonProperty(PropertyName = "full_path")]
        public string FullPath { get; set; }
        [JsonProperty(PropertyName = "descriptor")]
        public string Descriptor { get; set; }
        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

    }
}
