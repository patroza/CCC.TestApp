using Caliburn.Micro;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public ShellViewModel(UsersViewModel users) {
            base.DisplayName = "CCC TestApp";
            Items.Add(users);
            base.ActivateItem(users);
        }

        public void Back() {
            var screen = ActiveItem;
            if (screen != null)
                screen.TryClose();
        }
    }
}