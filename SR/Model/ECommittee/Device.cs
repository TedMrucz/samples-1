namespace Ecommittees.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("devices")]
    public partial class Device
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        public int? user_id { get; set; }

        [StringLength(255)]
        public string device_uuid { get; set; }

        [StringLength(255)]
        public string device_type { get; set; }

        [StringLength(255)]
        public string auth_token { get; set; }

        [StringLength(255)]
        public string auth_uuid { get; set; }

        [StringLength(255)]
        public string device_status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public string public_key { get; set; }

        [StringLength(255)]
        public string push_token { get; set; }
    }
}
