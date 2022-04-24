using MassTransit;

namespace Order.Application;

// Integration events REMOVE LATER

public interface IOrderStockConfirmedIntegrationEvent : CorrelatedBy<Guid>
{
}

public class OrdefrStockConfirmedIntegrationEvent : IOrderStockConfirmedIntegrationEvent
{
    public Guid CorrelationId { get; }

    public OrdefrStockConfirmedIntegrationEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
}

/// <summary>
/// Contract for order paid integration event. Correlation Id will identify event to correct saga instance
/// which is persisted to DB or in memory.
/// </summary>
public interface IOrderAcceptedIntegrationEvent : CorrelatedBy<Guid>
{
    public DateTime? AcceptDate { get; set; }
}


public interface IOrderNotificationSendedIntegrationEvent
{
    public Guid Id { get; set; } // Another way to add correlation ID
}

public interface IOrderCancellationRequestIntegrationEvent : CorrelatedBy<Guid>
{
    public DateTime? Date { get; set; }
    public int OrderId { get; set; }
}

// ---

/// <summary>
/// Instance of state machine which contains state machine data. Persisted to repository. Correlation Id
/// is way to identify instances in database.
/// </summary>
public class CreateOrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public int CurrentState { get; set; }
    public DateTime? AcceptDate { get; set;}
    public DateTime? CancelDate { get; set;}
}

/// <summary>
/// Define states, events and behavior of a finite state machine. Create once and apply event triggered behavior to instances.
/// </summary>
public class CreateOrderStateMachine : MassTransitStateMachine<CreateOrderState>
{
    // States

    public State StockConfirmed { get; private set; }
    public State Accepted { get; private set; }
    public State NotficationSended { get; private set; }
    public State Cancelled { get; private set; }
    public State Ready { get; private set; }

    // Events
    public Event<IOrderStockConfirmedIntegrationEvent> OrderStockConfirmed { get; private set; }
    public Event<IOrderAcceptedIntegrationEvent> OrderAccepted { get; private set; }
    public Event<IOrderNotificationSendedIntegrationEvent> OrderNotificationSended { get; private set; }
    public Event<IOrderCancellationRequestIntegrationEvent> OrderCancellationRequested { get; private set; }
    public Event OrderReady { get; private set; }

    public CreateOrderStateMachine()
    {   
        // Specify int state values: 0 - None, 1 - Initial, 2 - Final, 3 - StockConfirmed, 4 - Accepted, 5 - NotficationSended, 6 - Cancelled, 7 - Ready
        InstanceState(x => x.CurrentState, StockConfirmed, Accepted, NotficationSended, Cancelled, Ready);

        Event(() => OrderStockConfirmed);
        Event(() => OrderAccepted);
        Event(() => OrderNotificationSended, x => x.CorrelateById(c => c.Message.Id)); // Another way to add correlation id
        Event(() => OrderCancellationRequested);

        // State machine
        Initially(
            When(OrderStockConfirmed)
                .Publish(context => new OrdefrStockConfirmedIntegrationEvent(context.Saga.CorrelationId))
                .TransitionTo(StockConfirmed),
            When(OrderAccepted)
                .Then(x => x.Saga.AcceptDate = x.Message.AcceptDate)
                .TransitionTo(Accepted),
            When(OrderNotificationSended)
                .TransitionTo(NotficationSended)
        );

        During(Cancelled, Ignore(OrderStockConfirmed));
        During(Cancelled, Ignore(OrderAccepted));
        During(Cancelled, Ignore(OrderNotificationSended));

        // During any state when OrderCancellationRequested was received
        DuringAny( 
            When(OrderCancellationRequested)
                .RespondAsync(context => context.Init<OrdefrStockConfirmedIntegrationEvent>(new { OrderId = context.Saga.CorrelationId }))
                .TransitionTo(Cancelled)
        );
    }
}
