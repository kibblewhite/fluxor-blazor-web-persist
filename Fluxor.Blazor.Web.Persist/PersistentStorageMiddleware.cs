namespace Fluxor.Blazor.Web.Persist;

/// <summary>
/// Middleware that persists feature state to browser storage and restores it during initialization.
/// </summary>
/// <remarks>This middleware interacts with browser storage to save and restore the state of features in the
/// store. During initialization, it attempts to load the state for each feature from browser storage and restore it. It
/// also listens for state changes in features and persists the updated state asynchronously.</remarks>
/// <param name="browserStorage"></param>
internal sealed class PersistentStorageMiddleware(IBrowserStorage browserStorage) : Middleware
{
    private readonly IBrowserStorage _browserStorage = browserStorage;
    private readonly Lock _syncRoot = new();

    public override async Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        foreach (IFeature feature in store.Features.Values)
        {
            feature.StateChanged += FeatureStateHasChanged;
            object? featureState = await _browserStorage.LoadAsync(feature.GetName(), feature.GetStateType());
            if (featureState is null)
            {
                continue;
            }

            feature.RestoreState(featureState);
        }

        await base.InitializeAsync(dispatcher, store);
    }

    /// <summary>
    /// Handles changes to the state of a feature and persists the updated state.
    /// </summary>
    /// <remarks>This method checks if the provided feature is an <see cref="IFeature"/> and has the name
    /// "@statepersistence"  (case-insensitive). If these conditions are met, the feature's state is saved
    /// asynchronously to browser storage.</remarks>
    /// <param name="feature">The feature whose state has changed. Must implement <see cref="IFeature"/> to be processed.</param>
    /// <param name="e">The event arguments associated with the state change.</param>
    private void FeatureStateHasChanged(object? feature, EventArgs e)
    {
        if (feature is not IFeature stateFeatureChanged || stateFeatureChanged.GetName().ToLower().Equals("@statepersistence") is false)
        {
            return;
        }

        lock (_syncRoot)
        {
            _browserStorage.SaveAsync(stateFeatureChanged.GetName(), stateFeatureChanged.GetState());
        }
    }
}
