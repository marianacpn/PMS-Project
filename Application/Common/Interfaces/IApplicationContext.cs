using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<AuditableLog> AuditableLogs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
