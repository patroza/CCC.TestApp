using Caliburn.Micro;
using CCC.TestApp.UI.Desktop.ViewModels.Users;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        readonly UsersViewModel _users;

        public ShellViewModel(UsersViewModel users) {
            _users = users;
            base.DisplayName = "CCC TestApp";
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            base.ActivateItem(_users);
        }

        public void Back() {
            var screen = ActiveItem;
            if (screen != null)
                screen.TryClose();
        }
    }
}