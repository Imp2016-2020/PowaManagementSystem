using System.Diagnostics;
using FluentAssertions;
using PowaManagementSystem.Helpers;

namespace PowaManagementSystem.Tests.Performance;

public class HashHelperPerformanceTests
{
    [Fact]
    public void Md5_Performance_ShouldCompleteWithin100Milliseconds()
 {
        // Arrange
        const string input = "performance_test_string_with_reasonable_length";
    const int iterations = 1000;
 var stopwatch = new Stopwatch();

     // Act
     stopwatch.Start();
        for (var i = 0; i < iterations; i++)
        {
       HashHelper.Md5(input);
     }
        stopwatch.Stop();

     // Assert
 var averageTimePerOperation = stopwatch.ElapsedMilliseconds / (double)iterations;
averageTimePerOperation.Should().BeLessThan(0.1); // Less than 0.1ms per operation
    }

    [Fact]
    public void Md5_MemoryUsage_ShouldNotLeakMemory()
 {
        // Arrange
        const string input = "memory_test_string";
     const int iterations = 10000;
        
// Force garbage collection before test
    GC.Collect();
  GC.WaitForPendingFinalizers();
    GC.Collect();
        
  var initialMemory = GC.GetTotalMemory(false);

        // Act
   for (var i = 0; i < iterations; i++)
        {
      HashHelper.Md5(input);
        }

        // Force garbage collection after test
    GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        
        var finalMemory = GC.GetTotalMemory(false);

        // Assert
     var memoryIncrease = finalMemory - initialMemory;
  memoryIncrease.Should().BeLessThan(1024 * 1024); // Less than 1MB increase
    }
}