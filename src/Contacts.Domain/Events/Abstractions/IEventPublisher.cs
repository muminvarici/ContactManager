namespace Contacts.Domain.Events.Abstractions;

public interface IEventPublisher
{
    bool Enqueue(string route, object eventMessage);
}
