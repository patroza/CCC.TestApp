using System;
using System.Web;
using System.Web.Mvc;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Web.Models;
using CCC.TestApp.UI.Web.Presenters;

namespace CCC.TestApp.UI.Web.Controllers
{
    public class UsersController : Controller
    {
        readonly Lazy<IChangePasswordRequestBoundary> _changePassword;
        readonly Lazy<ICreateUserRequestBoundary> _create;
        readonly Lazy<IDestroyUserRequestBoundary> _destroy;
        readonly UsersPresenter _presenter;
        readonly Lazy<IShowUserRequestBoundary> _show;
        readonly Lazy<IUpdateUserRequestBoundary> _update;

        public UsersController(UsersPresenter presenter, Lazy<IChangePasswordRequestBoundary> changePassword,
            Lazy<ICreateUserRequestBoundary> create, Lazy<IShowUserRequestBoundary> show,
            Lazy<IDestroyUserRequestBoundary> destroy, Lazy<IUpdateUserRequestBoundary> update) {
            _presenter = presenter;
            _changePassword = changePassword;
            _create = create;
            _show = show;
            _destroy = destroy;
            _update = update;
        }

        public ActionResult Create(CreateUserViewModel model) {
            _create.Value.Invoke(
                new CreateUserRequestModel {UserName = model.UserName, Password = model.Password},
                _presenter);
            return View(_presenter.Result);
        }

        public ActionResult Destroy(Guid userId) {
            try {
                _destroy.Value.Invoke(new DestroyUserRequestModel {UserId = userId}, _presenter);
                return View(_presenter.Result);
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Show(Guid userId) {
            try {
                _show.Value.Invoke(new ShowUserRequestModel {UserId = userId}, _presenter);
                return View(_presenter.Result);
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Update(Guid userId, UpdateUserViewModel model) {
            try {
                _update.Value.Invoke(new UpdateUserRequestModel {UserId = userId, UserName = model.UserName}, _presenter);
                return View(_presenter.Result);
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult ChangePassword(Guid userId, ChangePasswordViewModel model) {
            try {
                _changePassword.Value.Invoke(
                    new ChangePasswordRequestModel {
                        UserId = userId,
                        OldPassword = model.OldPassword,
                        Password = model.NewPassword,
                        PasswordConfirmation = model.PasswordConfirmation
                    }, _presenter);
                return View(_presenter.Result);
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }
    }
}