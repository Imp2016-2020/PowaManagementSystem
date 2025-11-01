# PowaManagementSystem.Tests

This project contains comprehensive unit tests, integration tests, and performance tests for the PowaManagementSystem application.

## Test Structure

```
PowaManagementSystem.Tests/
??? Controllers/          # Controller unit tests
??? Models/     # Model unit tests  
??? Integration/         # Integration tests
??? Performance/         # Performance tests
??? Infrastructure/      # Test base classes and utilities
??? appsettings.json    # Test configuration
```

## Test Categories

### Unit Tests
- **Models**: Test model properties, validation, and business logic
- **Controllers**: Test controller actions, routing, and business logic
- **Helpers**: Test utility functions and helper methods

### Integration Tests
- **Web Application Tests**: Test the entire application pipeline
- **Database Integration**: Test database connectivity and operations

### Performance Tests
- **Hash Helper Performance**: Test hashing operations performance and memory usage

## Running Tests

### Command Line
```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test category
dotnet test --filter "Category=Unit"

# Run tests in parallel
dotnet test --parallel
```

### Visual Studio
- Open Test Explorer (Test ? Test Explorer)
- Click "Run All Tests" or run individual test methods

## Test Coverage

The project uses Coverlet for code coverage collection. Coverage reports are generated automatically in CI/CD pipeline.

To generate a local coverage report:
```bash
dotnet test --collect:"XPlat Code Coverage"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

## Continuous Integration

Tests are automatically run on:
- Every pull request to main/master/develop branches
- Every push to main/master/develop branches
- Multiple operating systems (Ubuntu, Windows, macOS)

The CI pipeline includes:
- Unit test execution
- Integration test execution
- Code coverage collection
- Security vulnerability scanning
- Cross-platform compatibility testing

## Test Dependencies

- **xUnit**: Testing framework
- **FluentAssertions**: Fluent assertion library
- **Moq**: Mocking framework
- **Microsoft.EntityFrameworkCore.InMemory**: In-memory database for testing
- **Microsoft.AspNetCore.Mvc.Testing**: Integration testing framework
- **Coverlet**: Code coverage collection

## Writing Tests

### Unit Test Example
```csharp
[Fact]
public void Method_WithValidInput_ShouldReturnExpectedResult()
{
    // Arrange
    var input = "test";
    var expected = "expected_result";

    // Act
    var result = ClassUnderTest.Method(input);

    // Assert
    result.Should().Be(expected);
}
```

### Integration Test Example
```csharp
[Fact]
public async Task Endpoint_ShouldReturnSuccessStatusCode()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/endpoint");

    // Assert
    response.EnsureSuccessStatusCode();
}
```

### Performance Test Example
```csharp
[Fact]
public void Operation_ShouldCompleteWithinTimeLimit()
{
    // Arrange
var stopwatch = Stopwatch.StartNew();

 // Act
    PerformOperation();
    stopwatch.Stop();

    // Assert
    stopwatch.ElapsedMilliseconds.Should().BeLessThan(100);
}
```

## Best Practices

1. **Follow AAA Pattern**: Arrange, Act, Assert
2. **Use Descriptive Test Names**: Method_Scenario_ExpectedBehavior
3. **One Assert Per Test**: Keep tests focused and specific
4. **Use Test Data Builders**: For complex object creation
5. **Clean Up Resources**: Implement IDisposable when needed
6. **Test Edge Cases**: Include boundary conditions and error scenarios
7. **Keep Tests Independent**: Tests should not depend on each other

## Contributing

When adding new functionality to the main application, please ensure:
1. Unit tests are added for new methods/classes
2. Integration tests cover new endpoints/workflows
3. Tests follow the established patterns and conventions
4. All tests pass before submitting a pull request

## Troubleshooting

### Common Issues

**Tests fail with database connection errors**
- Ensure SQL Server LocalDB is installed
- Check connection strings in appsettings.json

**Integration tests fail**
- Verify all required services are registered
- Check for missing configuration settings

**Performance tests are flaky**
- Run on a dedicated machine without other heavy processes
- Consider adjusting thresholds based on hardware capabilities