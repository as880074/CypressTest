using LoginDemo.Models;
using LoginDemo.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email !="handsome@gmail.com")
                {
                    TempDataMessage("message", "danger", $"We have {model.Email},try again.");
                    return RedirectToAction("Index", "Home");
                }
                //不開放註冊
                //CryptoBox.Data.Models.Users user = new Users
                //{
                //    Password = new Helpers.PasswordEncode().Encoder(model.Password), // SHA256
                //    Email = model.Email,
                //    Name = model.Name,
                //    Surname = model.Surname
                //};
                //_userService.InsertUser(user);
                //if (new EmailSender(_activationService).SendEmailActivation(model.Email).Result)
                //{
                //    TempDataMessage("message", "Success", $"Register is successfully,you can login now");
                //}
                //else
                //{
                //    TempDataMessage("message", "info", $"Register is successfully but activation mail did not send.");
                //}

            }
            else
            {
                TempDataMessage("message", "danger", $"Register form datas is not valid");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                if (model.Email != "admin@gmail.com" || model.Password != "123")
                {
                    TempDataMessage("message", "danger", $"Incorrect Password or Email.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (model.Email != "admin@gmail.com")
                    {
                        TempDataMessage("message", "danger", $"Account is not valid ({model.Email}),please active it");
                        return RedirectToAction("Index", "Home");
                    }
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "123456"), new Claim("Email", model.Email) }, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignOutAsync();
                    await HttpContext.SignInAsync(new ClaimsPrincipal(identity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.MaxValue,
                            AllowRefresh = true
                        });

                    return RedirectToAction("Index", "Sample");
                }

            }
            else
            {
                TempDataMessage("message", "danger", $"Login form datas is not valid");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public void TempDataMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}