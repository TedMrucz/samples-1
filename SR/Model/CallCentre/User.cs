using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CallCentre.Model
{
	public class User : IdentityUser<int>, IEntity
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public User()
		{
			this.Messages = new HashSet<Message>();
		}

		public int UserId { get; set; }
		[Required]
		public string Group { get; set; }
		[Required]
		public string ConnectionId { get; set; }

		[JsonIgnore, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Message> Messages { get; set; }
	}
}
