using EvDb.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// https://pradeepl.com/blog/dotnet/configuration-in-a-net-core-console-application/
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.Build();


var context = EvDbStorageContext.CreateWithEnvironment("auction-house");
var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging()
    .AddEvDbSqlServerStoreMigration(context)
    .BuildServiceProvider();

//configure console logging
IEvDbStorageMigration migration = serviceProvider
    .GetRequiredService<IEvDbStorageMigration>();

if (Array.Exists(args, m => m == "--destroy"))
    await migration.CreateEnvironmentAsync();
else
    await migration.DestroyEnvironmentAsync();
