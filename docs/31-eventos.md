# Eventos

A arquitetura de eventos foi preparada para reduzir acoplamento entre módulos.

## Tipos

- Domain Events
- Application Events
- Integration Events

## Contratos

- `IDomainEvent`
- `IApplicationEvent`
- `IIntegrationEvent`
- `IEventPublisher`
- `IEventSubscriber<TEvent>`

A implementação inicial é in-process. Mensageria externa fica para evolução futura.

