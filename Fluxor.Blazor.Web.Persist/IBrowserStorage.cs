namespace Fluxor.Blazor.Web.Persist;

/// <summary>
/// Defines methods for interacting with browser-based storage, allowing data to be persisted and retrieved.
/// </summary>
/// <remarks>This interface provides asynchronous methods for storing, retrieving, and clearing data in browser
/// storage. It supports both string-based storage and serialization/deserialization of complex objects.</remarks>
internal interface IBrowserStorage
{
    ValueTask ClearAsync(CancellationToken cancellationToken = default);
    ValueTask<string> GetItemAsync(string key, CancellationToken cancellationToken = default);
    ValueTask SetItemAsync(string key, string data, CancellationToken cancellationToken = default);
    Task SaveAsync<T>(string key, T state);
    Task<object?> LoadAsync(string key, Type featureType);
}