namespace EntityFramework.Samples.DB.Tests;
#pragma warning disable CA1852

internal class TestSampleShopDbContext: SampleShopDbContext
{

    public TestSampleShopDbContext(DbContextOptions<SampleShopDbContext> options) 
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}