namespace DataProvider.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class RoleType
    {
        public int Id { get; set; }

        [StringLength(8)]
        public string ShortDesc { get; set; }
    }
}
