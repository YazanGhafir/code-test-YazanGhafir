using MediatR;

namespace Hedin.ChangeTires.BuildingBlocks.Domain.Abstractions.Interfaces
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
