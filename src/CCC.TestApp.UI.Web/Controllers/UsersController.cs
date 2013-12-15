using System;
using System.Collections.Generic;
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
        readonly Lazy<IRequestBoundary<ChangePasswordRequestModel>> _changePassword;
        readonly Lazy<IRequestBoundary<CreateUserRequestModel>> _create;
        readonly Lazy<IRequestBoundary<DestroyUserRequestModel>> _destroy;
        readonly Lazy<IRequestBoundary<ShowUserRequestModel>> _show;
        readonly Lazy<IRequestBoundary<UpdateUserRequestModel>> _update;

        ActionResult _response;

        public UsersController(Lazy<IRequestBoundary<ChangePasswordRequestModel>> changePassword,
            Lazy<IRequestBoundary<CreateUserRequestModel>> create, Lazy<IRequestBoundary<ShowUserRequestModel>> show,
            Lazy<IRequestBoundary<DestroyUserRequestModel>> destroy,
            Lazy<IRequestBoundary<UpdateUserRequestModel>> update) {
            _changePassword = changePassword;
            _create = create;
            _show = show;
            _destroy = destroy;
            _update = update;
        }

        public ActionResult Create(CreateUserViewModel model) {
            if (!ModelState.IsValid)
                return View(model);
            _create.Value.Invoke(new CreateUserRequestModel {UserName = model.UserName, Password = model.Password});
            return _response;
        }

        public ActionResult Destroy(Guid userId) {
            try {
                _destroy.Value.Invoke(new DestroyUserRequestModel {UserId = userId});
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Show(Guid userId) {
            try {
                _show.Value.Invoke(new ShowUserRequestModel {UserId = userId});
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        public ActionResult Update(Guid userId, UpdateUserViewModel model) {
            if (!ModelState.IsValid)
                return View(model);
            try {
                _update.Value.Invoke(new UpdateUserRequestModel {UserId = userId, UserName = model.UserName});
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
                    });
                return _response;
            } catch (UserDoesntExistException e) {
                throw new HttpException(404, "user not found", e);
            }
        }

        #region Responders

        public void Respond(ChangePasswordResponseModel model) {
            if (model.Succeeded)
                return;
            AddErrors(model.Errors);
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
            if (model.Succeeded)
                return;
            AddErrors(model.Errors);
            _response = RedirectToAction("Show");
        }

        void AddErrors(IEnumerable<string> errors) {
            foreach (var error in errors)
                ModelState.AddModelError("", error);
        }

        #endregion
    }
}