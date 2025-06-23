namespace Fluxor.Blazor.Web.Persist;

/// <summary>
/// Represents configuration options for persistent storage.
/// </summary>
/// <remarks>This class provides settings to control how state data is identified and managed in persistent
/// storage. Use the <see cref="PersistenceKey"/> property to specify a unique key for storing and retrieving state
/// data.</remarks>
public sealed class PersistentStorageOptions
{
    /// <summary>
    /// Gets or sets the key used to identify persisted state data.
    /// </summary>
    public string PersistenceKey { get; set; } = "Fluxor.Blazor.Web.Persist";
}
