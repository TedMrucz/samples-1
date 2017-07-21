namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("agenda_versions")]
    public partial class AgendaVersion
    {
        public int id { get; set; }

        [StringLength(255)]
        public string agenda_id { get; set; }

        public int? version_number { get; set; }

        [StringLength(255)]
        public string sha256 { get; set; }

        public string cache { get; set; }

        public DateTime? captured_at { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [Required]
        [StringLength(255)]
        public string state { get; set; }

        public DateTime? released_at { get; set; }

        public string document_ids { get; set; }
    }
}
