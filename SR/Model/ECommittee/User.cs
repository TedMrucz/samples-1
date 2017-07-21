namespace Ecommittees.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("users")]
    public partial class User
    {
        public int id { get; set; }

        public int? person_id { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string encrypted_password { get; set; }

        [StringLength(255)]
        public string first_name { get; set; }

        [StringLength(255)]
        public string last_name { get; set; }

        [StringLength(255)]
        public string phone_number { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        public DateTime? reset_password_sent_at { get; set; }

        public DateTime? remember_created_at { get; set; }

        public int sign_in_count { get; set; }

        public DateTime? current_sign_in_at { get; set; }

        public DateTime? last_sign_in_at { get; set; }

        [StringLength(255)]
        public string current_sign_in_ip { get; set; }

        [StringLength(255)]
        public string last_sign_in_ip { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public bool admin { get; set; }

        [StringLength(255)]
        public string username { get; set; }

        public string groups { get; set; }

        public string profile { get; set; }
    }
}
