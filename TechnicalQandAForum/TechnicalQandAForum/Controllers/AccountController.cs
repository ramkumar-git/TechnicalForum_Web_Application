using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalQandAForum.CustomFilters;
using TechnicalQandAForum.ServiceLayer;
using TechnicalQandAForum.ViewModels;

namespace TechnicalQandAForum.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                int uid = _userService.InsertUser(registerViewModel);
                Session["CurrentUserId"] = uid;
                Session["CurrentUserName"] = registerViewModel.Name;
                Session["CurrentUserEmail"] = registerViewModel.Password;
                Session["CurrentUserPassword"] = uid;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("", "home");
            }
            else
            {
                ModelState.AddModelError("error", "Invalid data");
                return View(registerViewModel);
            }
        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                UserViewModel uvm = _userService.GetUserByEmailAndPassword(loginViewModel.Email, loginViewModel.Password);
                if (uvm != null)
                {
                    Session["CurrentUserId"] = uvm.UserId;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.IsAdmin;
                    if (uvm.IsAdmin)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "" });
                    }
                    else
                    {
                        return RedirectToAction("", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid Login Credentials");
                    return View(loginViewModel);
                }
            }
            else
            {
                ModelState.AddModelError("error", "Invalid Login Credentials");
                return View(loginViewModel);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("", "Home");
        }

        [UserAuthorization]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel uvm = _userService.GetUserByUserId(uid);
            EditUserDetailsViewModel editUserDetailsViewModel = new EditUserDetailsViewModel();
            if (uvm != null)
            {
                editUserDetailsViewModel = new EditUserDetailsViewModel()
                { UserId = uvm.UserId, Email = uvm.Email, Mobile = uvm.Mobile, Name = uvm.Name };
                return View(editUserDetailsViewModel);
            }
            else
            {
                return View(editUserDetailsViewModel);
            }
        }

        [UserAuthorization]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangeProfile(EditUserDetailsViewModel evm)
        {
            evm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
            if (!string.IsNullOrEmpty(evm.Email))
            {
                _userService.UpdateUserDetails(evm);
                Session["CurrentUserName"] = evm.Name;
                return RedirectToAction("", "Home");
            }
            return View(evm);
        }

        [UserAuthorization]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UserViewModel uvm = _userService.GetUserByUserId(uid);
            if (uvm != null)
            {
                EditUserPasswordViewModel editUserPasswordViewModel =
                    new EditUserPasswordViewModel() { Password = "", ConfirmPassword = "", Email = uvm.Email };
                return View(editUserPasswordViewModel);
            }
            else
            {
                return View();
            }
        }

        [UserAuthorization]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(EditUserPasswordViewModel epvm)
        {
            if (ModelState.IsValid)
            {
                epvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
                _userService.UpdateUserpassword(epvm);
                return RedirectToAction("", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "Something went wrong, Please try again");
                return View(epvm);
            }
        }

    }
}