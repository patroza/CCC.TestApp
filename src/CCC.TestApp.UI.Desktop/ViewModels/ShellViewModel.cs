using System;
using Caliburn.Micro;
using CCC.TestApp.UI.Desktop.ViewModels.Users;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        readonly Lazy<UsersController> _usersController;

        public ShellViewModel(Lazy<UsersController> usersController) {
            _usersController = usersController;
            base.DisplayName = "CCC TestApp";
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            _usersController.Value.ListUsers();
        }

        public void Back() {
            var screen = ActiveItem;
            if (screen != null)
                screen.TryClose();
        }
    }
}