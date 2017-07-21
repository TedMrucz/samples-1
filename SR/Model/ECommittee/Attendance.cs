namespace Ecommittees.Model
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("attendances")]
    public partial class Attendance
    {
        public int Id { get; set; }
		[JsonProperty(PropertyName = "meeting_id")]
		public int MeetingId { get; set; }
		[JsonProperty(PropertyName = "person_id")]
		public int PersonId { get; set; }
		[JsonProperty(PropertyName = "attendance_type")]
		public AttendanceType AttendanceType { get; set; }
		public int attendance_type_id { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }
    }
}
