namespace Ecommittees.Model
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("attendance_types")]
    public partial class AttendanceType
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [StringLength(255), JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

		[StringLength(255), JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
	}
}
