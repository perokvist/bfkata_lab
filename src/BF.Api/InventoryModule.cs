using Fiffi;
using Fiffi.Modularization;
using Protos;
using System.Reflection.Metadata;

namespace BF.Api;

public class InventoryModule : Module
{
    public InventoryModule(Func<ICommand, Task> dispatcher, Func<IEvent[], Task> publish, QueryDispatcher queryDispatcher, Func<IEvent[], Task> onStart) 
        : base(dispatcher, publish, queryDispatcher, onStart)
    {}

    public static InventoryModule Initialize(IAdvancedEventStore store, Func<IEvent[], Task> pub)
        => new Configuration<InventoryModule>((dispatch, pub, query, start) => new(dispatch, pub, query, start))
        .Commands(cmd => 
            cmd switch {
                AddLocation c => ApplicationService.ExecuteAsync(c, () => Handlers.Handle(c), pub),
                _ => throw new NotImplementedException()
        })
        .Create(store);
}

public record Command(Guid Id) : ICommand
{
    public IAggregateId AggregateId => new AggregateId(Id);
    public Guid CorrelationId { get; set; }
    public Guid CausationId { get; set; }
}

public record AddLocation(Guid Id, Location[] Locations) : Command(Id);
public record Location(Guid Id, string Name, Guid ParentId, Location[] Locations);

public record LocationAdded(Guid Id, string Name, Guid Parent) : EventRecord;

public static class Handlers
{
    public static EventRecord[] Handle(AddLocation command)
        => command.Locations
        .Select(x => new LocationAdded(x.Id, x.Name, x.ParentId))
        .ToArray();
}



