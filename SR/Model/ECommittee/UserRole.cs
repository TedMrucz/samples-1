namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("user_roles")]
    public partial class UserRole
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        public int? role_id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public int? committee_id { get; set; }
    }
}
