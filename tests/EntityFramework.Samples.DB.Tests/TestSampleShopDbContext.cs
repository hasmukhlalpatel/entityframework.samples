namespace EntityFramework.Samples.DB.Tests;

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