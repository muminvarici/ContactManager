namespace Contacts.Domain.Events.Abstractions;

public interface IEventPublisher
{
    bool Enqueue(object eventMessage);
}
