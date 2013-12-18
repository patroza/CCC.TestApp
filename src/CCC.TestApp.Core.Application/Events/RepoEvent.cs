using System;

namespace CCC.TestApp.Core.Application.Events
{
    public abstract class RepoEvent
    {
        protected RepoEvent(Guid id) {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    /*
    public abstract class RepoEvent<T>
    {
        public T Subject { get; protected set; }
    }
    public class RecordAdded<T> : RepoEvent<T> { }
    public class RecordRemoved<T> : RepoEvent<T> { }
    public class RecordUpdated<T> : RepoEvent<T> { }
    */
}