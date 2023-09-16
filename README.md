# AppEventService
Simple Blazor server app event service that handles real-time events for multiple users

![design](https://github.com/vincentnacar02/AppEventService/blob/master/design.png?raw=true)

### Basic usage

```csharp
// register service
Services.AddAppEventService<TodoUpdatedEvent>();

// inject to component
@inject IAppEventService<TodoUpdatedEvent> _appEventService;

// create dispatcher
var todo = ...;
_dispatcher = _appEventService.GetOrCreateDispatcher(todo.Id);

// create action
public void OnTodoUpdated(TodoUpdatedEvent ev)
{
  InvokeAsync(() =>
  {
      // do something with event
      ...
      StateHasChanged();
  });
}

...

_dispatcher.AddAction(OnTodoUpdated);

...

// publish event
await _dispatcher.PublishEventAsync(new TodoUpdatedEvent());

// cleanup
_dispatcher.RemoveAction(OnTodoUpdated);
_appEventService.DestroyDispatcher(_dispatcher.Key);
```

<a href="https://www.buymeacoffee.com/vincentnacar" target="_blank"><img src="https://img.buymeacoffee.com/button-api/?text=Buy me a coffee&emoji=&slug=vincentnacar&button_colour=5F7FFF&font_colour=ffffff&font_family=Cookie&outline_colour=000000&coffee_colour=FFDD00" /></a>

### Check the Samples folder for more info

### Installation
```cmd
NuGet\Install-Package AppEventService -Version 1.0.0.1
```
