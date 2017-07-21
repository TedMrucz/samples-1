using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CallCentre.Data
{
	public class MigrationsContextFactory : IDbContextFactory<DataContext>
	{
		public DataContext Create(DbContextFactoryOptions options)
		{
			var builder = new DbContextOptionsBuilder<DataContext>();
			builder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CallCentre;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			return new DataContext(builder.Options);
		}
	}
}