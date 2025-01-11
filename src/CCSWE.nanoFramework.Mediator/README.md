[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.Mediator/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.Mediator/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Mediator.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Mediator/) 

# CCSWE.nanoFramework.Mediator

A simple asynchronous mediator implementation for nanoFramework. Provides in-process publisher-subscriber communication while keeping all parties decoupled.

Based on [Mako-IoT.Device.Services.Mediator](https://github.com/CShark-Hub/Mako-IoT.Device.Services.Mediator)

## Usage

Create classes for your events
```c#
public class Event1 : IMediatorEvent
{
    public string Data { get; set; }
}

public class Event2 : IMediatorEvent
{
    public string Text { get; set; }
}
```
Your event subscriber must implement `IMediatorEventHandler` interface
```c#
public class EventHandlerService : IMediatorEventHandler
{
    public void HandleEvent(IMediatorEvent mediatorEvent)
    {
        switch (mediatorEvent)
        {
            case Event1 event1:
                Debug.WriteLine($"[{nameof(EventHandlerService)}] Event1 received. The data is: {event1.Data}");
                break;
            case Event2 event2:
                Debug.WriteLine($"[{nameof(EventHandlerService)}] Event2 received The text is: {event2.Text}");
                break;
        }
    }
}
```
Use `IMediator` to publish events
```c#
public class EventPublisherService : IEventPublisherService
{
    private readonly IMediator _mediator;

    public EventPublisherService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void DoSomething()
    {
        _mediator.Publish(new Event2 { Text = "Hello from EventPublisherService" });
    }
}
```
Register `AsyncMediator` and singleton subscribers to an `IHostBuilder` ...
```c#
    var hostBuilder = new HostBuilder();
    hostBuilder.UseMediator(options =>
    {
        options.AddSubscriber(typeof(Event1), typeof(Service2));
        options.AddSubscriber(typeof(Event2), typeof(Service2));
    });
```
... or directly to an `IServiceCollection`
```c#
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddMediator(options =>
    {
        options.AddSubscriber(typeof(Event1), typeof(Service2));
        options.AddSubscriber(typeof(Event2), typeof(Service2));
    });
```

For transient and scoped services you can use the `Subscribe` and `Unsubscribe` overloads that take a specific instance.
```c#
public class TransientService : IDisposable
{
    private readonly IMediator _mediator;

    public TransientService(IMediator mediator)
    {
        _mediator = mediator;
        _mediator.Subscribe(typeof(Event1), this);
        _mediator.Subscribe(typeof(Event2), this);
    }

    public void Dispose()
    {
        _mediator.Unsubscribe(typeof(Event1), this);
        _mediator.Unsubscribe(typeof(Event2), this);
    }
}
```