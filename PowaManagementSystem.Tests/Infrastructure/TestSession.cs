using Microsoft.AspNetCore.Http;

namespace PowaManagementSystem.Tests.Infrastructure;

// Test session implementation for testing
public class TestSession : ISession
{
 private readonly Dictionary<string, byte[]> _sessionStorage = new();

    public string Id => "test-session-id";
    public bool IsAvailable => true;
    public IEnumerable<string> Keys => _sessionStorage.Keys;

    public void Clear() => _sessionStorage.Clear();

    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

public void Remove(string key) => _sessionStorage.Remove(key);

 public void Set(string key, byte[] value) => _sessionStorage[key] = value;

    public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value!);
    
    // Extension method implementations for string values
    public void SetString(string key, string value)
{
     if (value == null)
    {
         Remove(key);
        }
   else
    {
            Set(key, System.Text.Encoding.UTF8.GetBytes(value));
        }
    }
    
    public string? GetString(string key)
    {
        if (TryGetValue(key, out var bytes))
  {
   return System.Text.Encoding.UTF8.GetString(bytes);
    }
   return null;
    }
}