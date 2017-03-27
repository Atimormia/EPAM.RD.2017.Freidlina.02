using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoGallery.ViewModels;
using PhotoGallery.Infrastructure.Core;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using PhotoGallery.DB.Infrastructure.Core;
using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Infrastructure.Services.Abstract;
using PhotoGallery.DB.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserRepository _userRepository;
        private readonly ILoggingRepository _loggingRepository;

        public AccountController(IMembershipService membershipService,
            IUserRepository userRepository,
            ILoggingRepository _errorRepository)
        {
            _membershipService = membershipService;
            _userRepository = userRepository;
            _loggingRepository = _errorRepository;
        }


        [HttpPost]
        public JsonResult Login(LoginViewModel user)
        {
            try
            {
                MembershipContext userContext = _membershipService.ValidateUser(user.Username, user.Password);

                if (userContext.User != null)
                {
                    IEnumerable<Role> roles = _userRepository.GetUserRoles(user.Username);
                    List<Claim> claims = new List<Claim>();
                    foreach (Role role in roles)
                    {
                        Claim claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Username);
                        claims.Add(claim);
                    }
                    FormsAuthentication.SetAuthCookie(userContext.User.Username, false);
                    return Json(new { status = true, rights = userContext.User.UserRoles.First(), username = userContext.User.Username });
                    //await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    //    new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                    //    new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties {IsPersistent = user.RememberMe });
                }
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                return Json(new { status = false, message = "invalid login or password format" });
            }

            return Json(new { status = false, message = "invalid login or password" });
        }

        [HttpPost]
        public JsonResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();

                return Json(new { status = false });
            }

        }

        [Route("register")]
        [HttpPost]
        public JsonResult Register(RegistrationViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User _user = _membershipService.CreateUser(user.Username, user.Email, user.Password, new int[] { 1 });

                    if (_user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return Json(new { status = true, rights = _user.UserRoles.First(), username = _user.Username });
                    }
                    else
                    {
                        return Json(new { status = false, message = "user already exists" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "invalid login or password format" });
                }
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                return Json(new { status = false, message = "invalid login or password" });
            }

            
        }
    }
}
