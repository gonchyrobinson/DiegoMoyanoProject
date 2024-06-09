using AutoMapper;
using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using DiegoMoyanoProject.Repository;
using DiegoMoyanoProject.ViewModels.UserData;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiegoMoyanoProject.Controllers
{
    public class UserDataController : Controller
    {
        private readonly IImagesRepository _imagesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserDataController(IImagesRepository imagesRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, ILogger<UserDataController> logger, IMapper mapper)
        {
            _imagesRepository = imagesRepository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");

                //Si quiero modificar para que se vea el último registro cargado, descomentar y retornar View(IndexUserDataVm)
                //var listImages = _userDataRepository.GetUserImages();
                //var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var listDates = _imagesRepository.GetAllDates();
                var maxDate = (listDates.Count() > 0) ? listDates.Max() : DateTime.Today;
                if (IsOwner()) return RedirectToAction("IndexOwner", new { date = maxDate });
                return RedirectToAction("IndexDate", new { date = maxDate });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult IndexDate(DateTime date)
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                var listImages = new List<ImageFile>();
                foreach (var type in ImageData.AllTypes)
                {
                    var img = _imagesRepository.getImage(date, type);
                    if (img != null) listImages.Add(img);
                }
                var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var listDates = _imagesRepository.GetAllDates();
                return View(new IndexDateUserDataViewModel(listImagesVm, listDates, date));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult IndexOwner(DateTime date)
        {
            try
            {
                if (IsNotLogued()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (!IsOwner()) return RedirectToAction("IndexDate", new { date = date });
                var listImages = new List<ImageFile>();
                foreach (var type in ImageData.AllTypes)
                {
                    var img = _imagesRepository.getImage(date, type);
                    if (img == null) img = new ImageFile(type);
                    listImages.Add(img);
                }
                var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var listDates = _imagesRepository.GetAllDates();
                return View(new IndexOwnerUserDataViewModel(listImagesVm, listDates, date));
            }
            catch (noDateException ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Upload", new { date = DateTime.Today });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (BadRequest());
            }
        }
        [HttpGet]
        public IActionResult UpdateImageForm(ImageType type, DateTime date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UpdateImageFormViewModel(type, date));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Delete(string date, ImageType type)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DateTime dateTime;

                if (DateTime.TryParse(date,out dateTime))
                {
                    _imagesRepository.Delete(dateTime, type);

                }
                    return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Upload(string date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                DateTime dateTime;

                if (DateTime.TryParse(date, out dateTime))
                {
                    _imagesRepository.AddDate(dateTime);
                }
                    return RedirectToAction("Index");
            }
            catch (InconsistenceInTheDBException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateImageFormViewModel model)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (model.InputFile == null) return RedirectToAction("Index");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    IFormFile file = (IFormFile)model.InputFile;
                    await (file.CopyToAsync(memoryStream));
                    var img = Convert.ToBase64String(memoryStream.ToArray());
                    var type = Path.GetExtension(model.InputFile.FileName).Split('.').Last();
                    _imagesRepository.Update(new ImageFile(type, img, model.ImageType), model.Date);

                }
                return RedirectToAction("IndexOwner",new {date = model.Date});
            }
            catch (InconsistenceInTheDBException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult ViewInversion(int id)
        {
            try
            {

                if (IsNotLogued() || ((IsOwner() || IsAdmin()) && id == null)) return RedirectToAction("Index", "Login");
                if (IsOwner() && id != null) return RedirectToAction("IndexOwner", new { id = id });
                var currentUserId = IdLoguedUser();
                var isLoguedUser = currentUserId == IdLoguedUser();
                var usu = _userRepository.GetUserById(id);
                return View(new ViewInversionViewModel(usu, isLoguedUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        public IActionResult RedirectionOfCapitalAndRentability()
        {
            try
            {
                if (IsOperative())
                {
                    return RedirectToAction("ViewInversion", new { id = IdLoguedUser() });
                }
                else
                {
                    return RedirectToRoute(new { Controller = "User", action = "Index" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
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
        private bool IsOwner()
        {
            return LoguedUserRole() == Role.Owner;
        }
        private bool IsAdmin()
        {
            return LoguedUserRole() == Role.Admin;
        }
        private bool IsOperative()
        {
            return LoguedUserRole() == Role.Operative;
        }
    }
}

