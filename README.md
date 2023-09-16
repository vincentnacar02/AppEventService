# AppEventService
Simple blazor server app event service

![design](https://github.com/vincentnacar02/AppEventService/blob/master/design.png?raw=true)

### Basic usage

```csharp
// register service
Services.AddSingleton<AppEventService<TodoUpdatedEvent>>();

// inject to component
@inject AppEventService<TodoUpdatedEvent> _appEventService;

// create dispatcher
var todo = ...;
_dispatcher = _appEventService.GetOrCreateDispatcher(todo.Id);

// create action
public void OnTodoUpdated(TodoUpdatedEvent ev) {
  // do something
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
