using System;

namespace CCC.TestApp.Core.Application.Services
{
    public interface IEventBus
    {
        Action<Action> PublicationThreadMarshaller { get; set; }
        bool HandlerExistsFor(Type messageType);
        void Publish(object message);
        void Publish(object message, Action<Action> marshal);
        void Subscribe(object subscriber);
        void Unsubscribe(object subscriber);
    }
}