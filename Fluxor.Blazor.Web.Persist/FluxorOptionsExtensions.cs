namespace Fluxor.Blazor.Web.Persist;

public static class FluxorOptionsExtensions
{
    /// <summary>
    /// Configures the Fluxor options to use persistent storage for state management.
    /// </summary>
    /// <remarks>This method adds the necessary services and middleware to enable persistent storage
    /// functionality. The <paramref name="configurations"/> parameter allows customization of the persistent storage
    /// options.</remarks>
    /// <param name="options">The <see cref="FluxorOptions"/> instance to configure.</param>
    /// <param name="configurations">An action to configure the <see cref="PersistentStorageOptions"/> used for persistent storage.</param>
    /// <returns>The updated <see cref="FluxorOptions"/> instance with persistent storage enabled.</returns>
    public static FluxorOptions UsePersistentStorage(this FluxorOptions options, Action<PersistentStorageOptions> configurations)
    {
        PersistentStorageOptions persistStorageOptions = new();
        configurations?.Invoke(persistStorageOptions);

        options.Services.Add(new ServiceDescriptor( typeof(PersistentStorageOptions), persistStorageOptions ));
        options.Services.Add(new ServiceDescriptor( typeof(IBrowserStorage), typeof(BrowserStorage), ServiceLifetime.Singleton ));

        options.AddMiddleware<PersistentStorageMiddleware>();
        return options;
    }
}
