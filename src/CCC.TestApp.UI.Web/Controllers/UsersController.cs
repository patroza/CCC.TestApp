using System;
using System.Web;
using System.Web.Mvc;
using CCC.TestApp.Core.Application.Usecases;
using CCC.TestApp.Core.Application.Usecases.Users;
using CCC.TestApp.UI.Web.Models;

namespace CCC.TestApp.UI.Web.Controllers
{
    public class UsersController : Controller, IResponseBoundary<ChangePasswordResponseModel>,
        IResponseBoundary<CreateUserResponseModel>,
        IResponseBoundary<DestroyUserResponseModel>, IResponseBoundary<UpdateUserResponseModel>,
        IResponseBoundary<ShowUserResponseModel>
    {
        readonly Lazy<IChangePasswordRequestBoundary> _changePassword;
        readonly Lazy<ICreateUserRequestBoundary> _create;
        readonly Lazy<IDestroyUserRequestBoundary> _destroy;
        readonly Lazy<IShowUserRequestBoundary> _show;
        readonly Lazy<IUpdateUserRequestBoundary> _update;

        ActionResult _response;

        public UsersController(Lazy<IChangePasswordRequestBoundary> changePassword,
            Lazy<ICreateUserRequestBoundary> create, Lazy<IShowUserRequestBoundary> show,
            Lazy<IDestroyUserRequestBoundary> destroy,
            Lazy<IUpdateUserRequestBoundary> update) {
            _changePassword = changePassword;
            _create = create;
            _show = show;
            _destroy = destroy;
            _update = update;
        }

        public ActionResult Create(CreateUserViewModel model) {
            if (!ModelState.IsValid)
                return View(model);

            try {
                _create.Value.Invoke(new CreateUserRequestModel {UserName = model.UserName, Password = model.Password},
                    this);
                return _response;
            } catch (UserAlreadyExistsException) {
                ModelState.AddModelError("", "Username already exists");
                return View(model);
            }
        }

        public ActionResult Destroy(Guid userId) {
            try {
                _destroy.Value.Invoke(new DestroyUserRequestModel {UserId = userId}, this);
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Show(Guid userId) {
            try {
                _show.Value.Invoke(new ShowUserRequestModel {UserId = userId}, this);
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Update(Guid userId, UpdateUserViewModel model) {
            if (!ModelState.IsValid)
                return View(model);
            try {
                _update.Value.Invoke(new UpdateUserRequestModel {Id = userId, UserName = model.UserName}, this);
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult ChangePassword(Guid userId, ChangePasswordViewModel model) {
            if (!ModelState.IsValid)
                return View(model);
            try {
                _changePassword.Value.Invoke(
                    new ChangePasswordRequestModel {
                        UserId = userId,
                        OldPassword = model.OldPassword,
                        Password = model.NewPassword,
                        PasswordConfirmation = model.PasswordConfirmation
                    }, this);
                return _response;
            } catch (OldPasswordMismatchException) {
                // We only catch OldPasswordMismatchException because other exceptions at this point should mean real error
                // (e.g ModelState.IsValid should've checked the other inputs)
                ModelState.AddModelError("", "Old password mismatch");
                return View(model);
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        #region Responders

        public void Respond(ChangePasswordResponseModel model) {
            _response = RedirectToAction("Show");
        }

        public void Respond(CreateUserResponseModel model) {
            _response = RedirectToAction("Show");
        }

        public void Respond(DestroyUserResponseModel model) {
            _response = RedirectToAction("Index", "Home");
        }

        public void Respond(ShowUserResponseModel model) {
            _response = View("Show", model);
        }

        public void Respond(UpdateUserResponseModel model) {
            _response = RedirectToAction("Show");
        }

        #endregion
    }
}