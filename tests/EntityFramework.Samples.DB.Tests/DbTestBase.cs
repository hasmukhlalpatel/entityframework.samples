namespace EntityFramework.Samples.DB.Tests;
public abstract class DbTestBase: IDisposable
{
    readonly DbContextOptions<SampleShopDbContext> _contextOptions;
    SqliteConnection _connection;
    protected DbTestBase()
    {
        //DbContext = new TestSampleShopDbContext();
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseSqlite(_connection)
            .Options;

        var dbContext = new TestSampleShopDbContext(_contextOptions);
        if (dbContext.Database.EnsureCreated())
        {
            dbContext.AddRange(
                new Customer { Id = 1, Name = "Test" },
                new Customer { Id = 2, Name = "Test2" }
            );
            dbContext.SaveChanges();
        }
        //else
        //{
        //    Assert.Fail("Database must be created");
        //}
    }

    protected SampleShopDbContext CreateDbContext() => new TestSampleShopDbContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
}