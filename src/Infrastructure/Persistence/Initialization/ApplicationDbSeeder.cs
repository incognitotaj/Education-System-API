using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Domain.Identity;

namespace Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }
}