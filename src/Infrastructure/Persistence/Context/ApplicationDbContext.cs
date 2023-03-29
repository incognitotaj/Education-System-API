using Domain.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration _config;
        private readonly DatabaseSettings _dbSettings;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration config,
            IOptions<DatabaseSettings> dbSettings) : base(options)
        {
            _config = config;
            _dbSettings = dbSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseDatabase(_dbSettings.DBProvider, _dbSettings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = base.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}
