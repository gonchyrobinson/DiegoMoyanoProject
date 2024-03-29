using AutoMapper;
using DiegoMoyanoProject.Models;
using DiegoMoyanoProject.Repository;
using DiegoMoyanoProject.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace DiegoMoyanoProject.Controllers
{

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            try
            {

                if (IsNotLogued()) return RedirectToAction("Index", "Login");
                List<User> listOfUsers = _userRepository.ListUsers();
                if (LoguedUserRole() == Role.Owner)
                {
                    listOfUsers = _userRepository.ListUsers();
                }
                else
                {
                    listOfUsers = _userRepository.ListOperativeUsers();
                }
                var listOfUser = _mapper.Map<List<UserOfIndexUserViewModel>>(listOfUsers);
                var indexVM = new IndexUserViewModel(listOfUser, LoguedUserRole(), IdLoguedUser());

                return View(indexVM);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in UserController.Index");
                return BadRequest();
            }
        }

        private int IdLoguedUser()
        {
            return (int)HttpContext.Session.GetInt32("Id");
        }

        private bool IsNotLogued()
        {
            return !HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null;
        }

        private Role LoguedUserRole()
        {
            return (Role)HttpContext.Session.GetInt32("Role");
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");

                //if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                return View(new CreateUserViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
        }

        [HttpPost]

        public IActionResult CreateUser(CreateUserViewModel usu)
        {

            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");

                //if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null || HttpContext.Session.GetString("Role")=="Operative") { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }

                if (ModelState.IsValid)
                {
                    var Usu = _mapper.Map<User>(usu);
                    _userRepository.CreateUser(Usu);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Algun parametro no cumple los estandares");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");

                if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null || (HttpContext.Session.GetString("Role") == "Operative" && HttpContext.Session.GetInt32("Id") != id)) { RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                var usu = _userRepository.GetUserById(id);
                var usuvm = _mapper.Map<UpdateUserViewModel>(usu);
                return View(usuvm);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserViewModel UsuVM)
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");

                if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null || (HttpContext.Session.GetString("Role") == "Operative" && HttpContext.Session.GetInt32("Id") != UsuVM.Id)) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                if (ModelState.IsValid)
                {
                    var usu = _mapper.Map<User>(UsuVM);
                    _userRepository.UpdateUser(UsuVM.Id, usu);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Algun modelo no es valido");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        public IActionResult DeleteUser(int id)
        {
            try
            {

                if (!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null || (HttpContext.Session.GetString("Role") == "Operative" && HttpContext.Session.GetInt32("Id") != id)) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "usersData", id.ToString());
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
                _userRepository.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

    }

}
