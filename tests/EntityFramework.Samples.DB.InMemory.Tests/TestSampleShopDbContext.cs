namespace EntityFramework.Samples.DB.InMemory.Tests;
#pragma warning disable CA1852
internal class TestSampleShopDbContext : SampleShopDbContext
#pragma warning restore CA1852
{

    public TestSampleShopDbContext(DbContextOptions<SampleShopDbContext> options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}