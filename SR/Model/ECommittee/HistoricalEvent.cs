namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("historical_events")]
    public partial class HistoricalEvent
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? device_id { get; set; }

        public int? subject_id { get; set; }

        [StringLength(255)]
        public string subject_type { get; set; }

        [StringLength(255)]
        public string subject_descriptor { get; set; }

        [StringLength(255)]
        public string event_type { get; set; }

        public string event_data { get; set; }

        public DateTime? observed_at { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [StringLength(255)]
        public string uuid { get; set; }
    }
}
