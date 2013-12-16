using CCC.TestApp.Core.Domain.Entities;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public class UserViewModel : ScreenBase
    {
        public UserViewModel(User user) {
            base.DisplayName = "Show User";
        }
    }
}