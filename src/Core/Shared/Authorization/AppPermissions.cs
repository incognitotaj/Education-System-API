using System.Collections.ObjectModel;

namespace Shared.Authorization;

public static class AppAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class AppResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class AppPermissions
{
    private static readonly AppPermission[] _all = new AppPermission[]
    {
        new("View Dashboard", AppAction.View, AppResource.Dashboard),
        new("View Hangfire", AppAction.View, AppResource.Hangfire),
        new("View Users", AppAction.View, AppResource.Users),
        new("Search Users", AppAction.Search, AppResource.Users),
        new("Create Users", AppAction.Create, AppResource.Users),
        new("Update Users", AppAction.Update, AppResource.Users),
        new("Delete Users", AppAction.Delete, AppResource.Users),
        new("Export Users", AppAction.Export, AppResource.Users),
        new("View UserRoles", AppAction.View, AppResource.UserRoles),
        new("Update UserRoles", AppAction.Update, AppResource.UserRoles),
        new("View Roles", AppAction.View, AppResource.Roles),
        new("Create Roles", AppAction.Create, AppResource.Roles),
        new("Update Roles", AppAction.Update, AppResource.Roles),
        new("Delete Roles", AppAction.Delete, AppResource.Roles),
        new("View RoleClaims", AppAction.View, AppResource.RoleClaims),
        new("Update RoleClaims", AppAction.Update, AppResource.RoleClaims),
        new("View Products", AppAction.View, AppResource.Products, IsBasic: true),
        new("Search Products", AppAction.Search, AppResource.Products, IsBasic: true),
        new("Create Products", AppAction.Create, AppResource.Products),
        new("Update Products", AppAction.Update, AppResource.Products),
        new("Delete Products", AppAction.Delete, AppResource.Products),
        new("Export Products", AppAction.Export, AppResource.Products),
        new("View Brands", AppAction.View, AppResource.Brands, IsBasic: true),
        new("Search Brands", AppAction.Search, AppResource.Brands, IsBasic: true),
        new("Create Brands", AppAction.Create, AppResource.Brands),
        new("Update Brands", AppAction.Update, AppResource.Brands),
        new("Delete Brands", AppAction.Delete, AppResource.Brands),
        new("Generate Brands", AppAction.Generate, AppResource.Brands),
        new("Clean Brands", AppAction.Clean, AppResource.Brands),
        new("View Tenants", AppAction.View, AppResource.Tenants, IsRoot: true),
        new("Create Tenants", AppAction.Create, AppResource.Tenants, IsRoot: true),
        new("Update Tenants", AppAction.Update, AppResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", AppAction.UpgradeSubscription, AppResource.Tenants, IsRoot: true)
    };

    public static IReadOnlyList<AppPermission> All { get; } = new ReadOnlyCollection<AppPermission>(_all);
    public static IReadOnlyList<AppPermission> Root { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<AppPermission> Admin { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<AppPermission> Basic { get; } = new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record AppPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}