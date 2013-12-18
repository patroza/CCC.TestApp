using System;

namespace CCC.TestApp.Core.Application.Events
{
    public class UserRecordDestroyed : RepoEvent
    {
        public UserRecordDestroyed(Guid id) : base(id) {}
    }

    public class UserRecordCreated : RepoEvent
    {
        public UserRecordCreated(Guid id) : base(id) {}
    }

    public class UserRecordUpdated : RepoEvent
    {
        public UserRecordUpdated(Guid id) : base(id) {}
    }
}