using Microsoft.EntityFrameworkCore;
using MMGTS.Domain.Entities;
using System.Data.Entity.Infrastructure;

namespace MMGTS.Infra.EF.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
              (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLowerCaseNamingConvention();

        public virtual DbSet<MatchData> MatchData { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();

            return await base.SaveChangesAsync(cancellationToken);
        }


        private void AddTimestamps()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            });
        }
    }
}
