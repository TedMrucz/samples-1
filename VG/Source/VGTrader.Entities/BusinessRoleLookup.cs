using System;

namespace VGTrader.Entities
{
    public class BusinessRoleLookup
    {
		public BusinessRoleLookup(int id, string shortDesc)
		{
			Id = id;
			ShortDesc = shortDesc;
		}
		public int Id { set; get; }
		public string ShortDesc { set; get; }
	}
}
