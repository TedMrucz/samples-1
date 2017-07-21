using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CallCentre.Model
{
	public partial class Message : IEntity
	{
		[Key]
		public int Id { get; set; }
		//[CreditCard]
		//[DefaultValue(0)]
		//[EmailAddress]
		//[EnumDataType(System.Type)]
		//[Phone]
		//[Range(0,10)]
		public int UserId { get; set; }
		public int User_Id { get; set; }
		[MaxLength(256), Required]
		public string Text { get; set; }
		[DataType(DataType.Date), Required]
		public System.DateTime Time { get; set; }
		[JsonIgnore]
		public virtual User User { get; set; }
	}
}
