namespace Ecommittees.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("manifests")]
    public partial class Manifest
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "device_id")]
        public int DeviceId { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

		public DateTime? published_at { get; set; }

        public DateTime? received_at { get; set; }

        public DateTime? expired_at { get; set; }

        public DateTime? deleted_at { get; set; }
        [JsonProperty(PropertyName = "agenda_version_id")]
        public int AgendaId { get; set; }
    }
}
