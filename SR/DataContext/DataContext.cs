using CallCentre.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Linq;

namespace CallCentre.Data
{
	[Export(typeof(IDataContext))]
	public partial class DataContext : IdentityDbContext<User, IdentityRole<int>, int>, IDataContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		IQueryable<User> IDataContext.Users => Set<User>().AsNoTracking();
		public IQueryable<Message> Messages => Set<Message>().AsNoTracking();
		public IQueryable<User> UserMessages => Set<User>().Include(p => p.Messages).AsNoTracking();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Message>().ToTable("Messages").HasKey(e => e.Id);
			modelBuilder.Entity<User>().ToTable("Users").HasKey(e => e.Id);
			modelBuilder.Entity<User>().HasMany(p => p.Messages).WithOne(p => p.User).HasForeignKey(p => p.User_Id);
			modelBuilder.Entity<User>().Property(e => e.ConnectionId).HasColumnName("ConnectionId");

			base.OnModelCreating(modelBuilder);
		}

		public void Entity<T>(T entity, EntityState state) where T : class, IEntity
		{
			if (this.ChangeTracker.Entries<T>().Any(e => e.Entity.Id == entity.Id))
				return;

			switch (state)
			{
				case EntityState.Added:
					Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
					break;
				case EntityState.Modified:
					Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					break;
				case EntityState.Deleted:
					Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
					break;
			}
		}
	}
}