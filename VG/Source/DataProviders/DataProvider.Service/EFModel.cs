namespace DataProvider.Service
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using DataProvider.Entities;

	public partial class EFModel : DbContext
	{
		public EFModel()
			: base("name=EFModel")
		{
		}

		public virtual DbSet<RoleType> RoleTypes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
