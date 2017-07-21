using CallCentre.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCentre.Data
{
	public interface IDataContext
	{
		IQueryable<Message> Messages { get; }
		IQueryable<User> Users { get; }

		void Entity<T>(T entity, EntityState state) where T : class, IEntity;

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}

	public static class DataContextExtensions
	{
		public static void Entity<T>(this IDataContext dataContext, T entity) where T : class, IEntity
		{
			if (entity.Id == 0)
				dataContext.Entity<T>(entity, EntityState.Added);
			else
				dataContext.Entity<T>(entity, EntityState.Modified);
		}
	}

	public enum EntityState
	{
		Added,
		Modified,
		Deleted
	}
}