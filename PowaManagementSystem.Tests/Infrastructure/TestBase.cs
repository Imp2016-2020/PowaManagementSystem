using Microsoft.EntityFrameworkCore;
using PowaManagementSystem.Data;

namespace PowaManagementSystem.Tests.Infrastructure;

public abstract class TestBase : IDisposable
{
    protected PowaDbContext Context { get; private set; }

    protected TestBase()
    {
    var options = new DbContextOptionsBuilder<PowaDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new PowaDbContext(options);
        Context.Database.EnsureCreated();
    }

    protected virtual void SeedDatabase()
  {
        // Override this method in derived classes to seed test data
    }

    public virtual void Dispose()
    {
        Context?.Dispose();
    }
}