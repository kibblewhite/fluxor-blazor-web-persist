# Fluxor.Blazor.Web.Persist
This project is a library for persisting Fluxor state in Blazor applications. It provides functionality to save and restore the state of your Fluxor store, allowing you to maintain application state across page reloads or sessions.

## Features
- **State Persistence**: Automatically saves the state of your Fluxor store to local storage.
- **State Restoration**: Restores the state from storage when the application starts, ensuring a seamless user experience.
- **Customizable Storage**: Supports local storage and in the future session storage.
- **Integration with Fluxor**: Easily integrates with the Fluxor state management library for Blazor applications.
- **Type Safety**: Ensures type safety when persisting and restoring state, reducing runtime errors.
- **Event Handling**: Allows you to handle events when state is saved or restored, enabling additional feature logic or side effects.

## Getting Started

To get started with Fluxor.Blazor.Web.Persist, follow these steps:

- Install the package via NuGet:
```bash
dotnet add package Fluxor.Blazor.Web.Persist
```

- Include the namespace in your Blazor components or services where you want to use the persistence functionality:
```csharp
using Fluxor.Blazor.Web.Persist;
```

- Add the necessary services in your `Program.cs` or `Startup.cs` file:
```csharp
builder.Services.AddFluxor(options => 
{
	options
		.ScanAssemblies(typeof(Program).Assembly)
		.UsePersistentStorage(options => options.PersistenceKey = "Fluxor.Blazor.Web.Persist");
});
```

- Done! Your Fluxor store will now automatically persist its state to local storage.
