namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("revoked_meetings")]
    public partial class RevokedMeeting
    {
        public int id { get; set; }

        public int? meeting_id { get; set; }

        public int? person_id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
