using System;
using System.ComponentModel.Composition;

namespace CCC.TestApp.UI.Desktop.ViewModels.Users
{
    public class UserViewModelsFactory
    {
        readonly ExportFactory<EditUserViewModel> _editUserFactory;
        readonly ExportFactory<ShowUserViewModel> _showUserFactory;

        public UserViewModelsFactory(ExportFactory<EditUserViewModel> editUserFactory,
            ExportFactory<ShowUserViewModel> showUserFactory) {
            _editUserFactory = editUserFactory;
            _showUserFactory = showUserFactory;
        }

        public Func<Guid, ExportLifetimeContext<EditUserViewModel>> EditUser {
            get { return _editUser; }
        }

        public Func<Guid, ExportLifetimeContext<ShowUserViewModel>> ShowUser {
            get { return _showUser; }
        }

        ExportLifetimeContext<EditUserViewModel> _editUser(Guid userId) {
            var editUser = _editUserFactory.CreateExport();
            editUser.Value.LoadUser(userId);
            return editUser;
        }

        ExportLifetimeContext<ShowUserViewModel> _showUser(Guid userId) {
            var showUser = _showUserFactory.CreateExport();
            showUser.Value.SetUserId(userId);
            return showUser;
        }
    }
}