using System.Data.Common;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PetStore.Infrastructure.Persistence;
using PetStore.Infrastructure.Persistence.Mouse;
using Xunit;

namespace PetStore.Tests.Infrastructure.Persistence.Mouse;

public class MouseRepositoryTests : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<PetStoreDatabaseContext> _contextOptions;
    
    public MouseRepositoryTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<PetStoreDatabaseContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new PetStoreDatabaseContext(_contextOptions);
        context.Database.EnsureCreated();
    }

    private PetStoreDatabaseContext CreateContext() => new (_contextOptions);
    public void Dispose() => _connection.Dispose();

    [Fact] 
    public async Task AddAsync_AddsMouse()
    {
        // Arrange
        await using var writeContext = CreateContext();
        var mouse = new PetStore.Domain.MouseAggregate.Mouse("john doe");
        var sut = new MouseRepository(writeContext);

        // Act
        await sut.AddAsync(mouse);
        await writeContext.SaveChangesAsync();

        // Assert
        await using var readContext = CreateContext();
        var actual = await readContext.Mice.SingleAsync(b => b.Id == mouse.Id);

        actual.Should().BeEquivalentTo(mouse, options => options.Excluding(x => x.DomainEvents));
    }
}