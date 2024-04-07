using DiegoMoyanoProject.Repository;
using Microsoft.AspNetCore.Mvc;
using DiegoMoyanoProject.ViewModels;
using DiegoMoyanoProject.ViewModels.Login;
using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;

namespace DiegoMoyanoProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginManagment;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginRepository loginManagment, ILogger<LoginController> logger)
        {
            _loginManagment = loginManagment;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Log(IndexLoginViewModel loginvm)
        {
            try
            {
                if (!ModelState.IsValid) throw new ModelStateInvalidException("Invalid data");
                var loguedUser = _loginManagment.Log(loginvm.Mail, loginvm.Pass);
                SessionStart(loguedUser);
                if (LoguedUserRole() == Role.Operative)
                {

                return RedirectToRoute(new {Controller = "UserData", Action = "ViewInversion"});
                }
                else
                {
                    return RedirectToRoute(new { Controller = "User", Action = "Index" });
                }
            }
            catch (ModelStateInvalidException ex) 
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = "Datos incorrectos";
                return RedirectToAction("Index");
            }
            catch (PassAndMailNotMatch ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = "El mail y la contraseña ingresada no coinciden";
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return RedirectToRoute(new { Controller = "Shared", Action = "Error" });
            }
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return(RedirectToAction("Index", "Home"));
        }
        private void SessionStart(User loguedUser)
        {
            HttpContext.Session.SetString("Username", loguedUser.Username);
            HttpContext.Session.SetString("Mail", loguedUser.Mail);
            HttpContext.Session.SetInt32("Role", (int)loguedUser.Role);
            HttpContext.Session.SetInt32("Id", loguedUser.Id);
        }
        private Role LoguedUserRole()
        {
            return (Role)HttpContext.Session.GetInt32("Role");
        }
    }
}