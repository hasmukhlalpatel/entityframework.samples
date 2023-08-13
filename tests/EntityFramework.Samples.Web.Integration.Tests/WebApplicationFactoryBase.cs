// Copyright (c) Hasmukh Patel, 2023. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace EntityFramework.Samples.Web.Integration.Tests;

/// <summary>
/// WebApplicationFactoryBase.
/// </summary>
/// <typeparam name="TProgram"> program name.</typeparam>
public class WebApplicationFactoryBase<TProgram>
    : WebApplicationFactory<TProgram>
    where TProgram : class
{
    /// <summary>
    /// ConfigureWebHost.
    /// </summary>
    /// <param name="builder">IWebHostBuilder parameter.</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
#pragma warning disable CA1062 // Validate arguments of public methods
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<SampleShopDbContext>));

            Assert.NotNull(dbContextDescriptor);

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            Assert.NotNull(dbConnectionDescriptor);

            services.Remove(dbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<SampleShopDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });
#pragma warning restore CA1062 // Validate arguments of public methods

        builder.UseEnvironment("Development");
    }
}