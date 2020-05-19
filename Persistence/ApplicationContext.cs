using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Support.Configuration;
using Shared.Support.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task = Domain.Entities.Task;

namespace Persistence
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        private readonly DbConnectionConfig dbConnection;
        private readonly ICurrentUserService _currentUserService;

        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<AuditableLog> AuditableLogs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

        public ApplicationContext(ICurrentUserService currentUserService,
        IOptionsSnapshot<DbConnectionConfig> config)
        {
            _currentUserService = currentUserService;
            dbConnection = config.Value;
            ChangeTracker.LazyLoadingEnabled = false;

            try
            {
                if (Database.GetPendingMigrations().Count() > 0)
                    Database.Migrate();
            }
            catch (Exception)
            {

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbConnection.GetConnectionString());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (ChangeTracker.Entries<ApplicationLog>().Any(e => e.Entity.ApplicationLogType == (int)ApplicationLogTypesEnum.Error))
            {
                var entities = ChangeTracker.Entries().Where(e => e.Entity.GetType() != typeof(ApplicationLog));

                while (entities.Any(e => e.State != EntityState.Unchanged))
                {
                    foreach (var entry in entities)
                    {
                        try
                        {
                            if (entry.State != EntityState.Unchanged)
                                entry.State = EntityState.Detached;

                        }
                        catch (Exception) { }
                    }
                }

                return base.SaveChangesAsync(cancellationToken);
            }

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.MarkAsCreated(_currentUserService.UserId, _currentUserService.UserName);
                        break;
                    case EntityState.Modified:
                        entry.Entity.MarkAsChanged(_currentUserService.UserId, _currentUserService.UserName);
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
