using Microsoft.Owin.Security;
using Minifutbol.BL.Extensions;
using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Minifutbol.User.Controllers
{
    public class AccountController : Controller
    {
        private UserLogic _userLogic;

        public AccountController()
        {
            _userLogic = new UserLogic();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel parameters)
        {
            var opResult = _userLogic.ValidateUser(parameters.UserName, parameters.Password);
            if (opResult.UserClaims.Any(s => s.ClaimValue.ToLower() != "user"))
            {
                return View();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, opResult.UserName),
                new Claim("userId", opResult.Id.ToString()),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(opResult.UserClaims.Select(s=>s.ClaimValue).ToList()))
            };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            var authManager = Request.GetOwinContext().Authentication;
            authManager.SignOut();

            authManager.SignIn(new AuthenticationProperties
            { IsPersistent = parameters.RememberMe }, identity);

            return RedirectToAction("Index", "home");
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserCreateModel parameters)
        {
            var addREsult = _userLogic.Add(parameters);

            if (addREsult.IsSuccess)
            {
                var opResult = _userLogic.ValidateUser(parameters.UserName, parameters.Password);
                if (opResult.UserClaims.Any(s => s.ClaimValue.ToLower() != "user"))
                {
                    return View();
                }

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, opResult.UserName),
                new Claim("userId", opResult.Id.ToString()),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(opResult.UserClaims.Select(s=>s.ClaimValue).ToList()))
            };
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                var authManager = Request.GetOwinContext().Authentication;
                authManager.SignOut();

                authManager.SignIn(new AuthenticationProperties
                { IsPersistent = false }, identity);

                return RedirectToAction("Index", "home");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            var authManager = Request.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "home");
        }

        public ActionResult UserProfile()
        {
            var userid = Request.GetOwinContext().Authentication.User.GetUserId();

            var user = _userLogic.GetById(userid);
            if (user.IsSuccess)
            {
                return View(user.Output);
            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult UserProfile(UserUpdateModel parameters)
        {
            var userid = Request.GetOwinContext().Authentication.User.GetUserId();

            var user = _userLogic.Update(parameters);
            if (user.IsSuccess)
            {
                return RedirectToAction("index", "home");
            }
            return View(user.Output);
        }
    }
}