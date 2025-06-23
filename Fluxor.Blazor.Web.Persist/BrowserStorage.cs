namespace Fluxor.Blazor.Web.Persist;

public class BrowserStorage(IJSRuntime jSRuntime, PersistentStorageOptions persistentStorageOptions) : IBrowserStorage
{
    private readonly IJSRuntime _jSRuntime = jSRuntime;
    private readonly PersistentStorageOptions _persistentStorageOptions = persistentStorageOptions;

    public ValueTask ClearAsync(CancellationToken cancellationToken = default)
        => ExecuteAsync(() => _jSRuntime.InvokeVoidAsync("localStorage.clear", cancellationToken));

    public ValueTask<string> GetItemAsync(string key, CancellationToken cancellationToken = default)
        => ExecuteAsync(() => _jSRuntime.InvokeAsync<string>("localStorage.getItem", cancellationToken, $"{_persistentStorageOptions.PersistenceKey}.{key}"));

    public ValueTask SetItemAsync(string key, string data, CancellationToken cancellationToken = default)
        => ExecuteAsync(() => _jSRuntime.InvokeVoidAsync("localStorage.setItem", cancellationToken, $"{_persistentStorageOptions.PersistenceKey}.{key}", data));

    public async Task SaveAsync<T>(string key, T state)
        => await SetItemAsync($"{_persistentStorageOptions.PersistenceKey}.{key}", JsonSerializer.Serialize(state, JsonSerializerOptions.Web));

    public async Task<object?> LoadAsync(string key, Type featureType)
    {
        string featureState = await GetItemAsync($"{_persistentStorageOptions.PersistenceKey}.{key}");
        return JsonSerializer.Deserialize(string.IsNullOrWhiteSpace(featureState) ? "{}" : featureState, featureType);
    }

    private static ValueTask<T> ExecuteAsync<T>(Func<ValueTask<T>> func) => func();
    private static ValueTask ExecuteAsync(Func<ValueTask> func) => func();
}
