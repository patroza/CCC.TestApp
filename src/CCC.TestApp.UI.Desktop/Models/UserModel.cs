using System;

namespace CCC.TestApp.UI.Desktop.Models
{
    public class UserModel : ModelBase
    {
        string _userName;

        public UserModel(Guid id) {
            Id = id;
        }

        public Guid Id { get; private set; }

        public string UserName {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
    }
}