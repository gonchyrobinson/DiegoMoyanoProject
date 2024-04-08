using DiegoMoyanoProject.Repository;
using Microsoft.AspNetCore.Mvc;
using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using AutoMapper;
using DiegoMoyanoProject.ViewModels.UserData;
using System.IO;

namespace DiegoMoyanoProject.Controllers
{
    public class UserDataController : Controller
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserDataController(IUserDataRepository userDataRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, ILogger<UserDataController> logger, IMapper mapper)
        {
            _userDataRepository = userDataRepository;
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
                if (IsOwner()) return RedirectToAction("IndexOwner");
                var listImages = _userDataRepository.GetUserImages();
                var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var listDates = _userDataRepository.GetAllDates();
                return View(new IndexUserDataViewModel(listImagesVm, listDates));
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
                var listImages = _userDataRepository.GetUserImages(date);
                var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var listDates = _userDataRepository.GetAllDates();
                return View(new IndexDateUserDataViewModel(listImagesVm, listDates));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult IndexOwner()
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                var vm = new IndexOwnerUserDataViewModel();
                vm.Sales = _userDataRepository.GetImage(ImageType.Sales, 1);
                vm.SpentMoney = _userDataRepository.GetImage(ImageType.SpentMoney, 1);
                vm.Campaigns = _userDataRepository.GetImage(ImageType.Campaigns, 1);
                vm.Listings = _userDataRepository.GetImage(ImageType.Listings, 1);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (BadRequest());
            }
        }
        [HttpGet]
        public IActionResult UploadImageForm(ImageType type)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UploadImageFormViewModel(type));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult UpdateImageForm(ImageType type, int order)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UpdateImageFormViewModel(type, order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult Delete(int order, ImageType type)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DeleteImage(order, type);
                return RedirectToAction("IndexOwner");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private void DeleteImage(int order, ImageType type)
        {
            var image = _userDataRepository.GetImage(type, order);
            _userDataRepository.DeleteImage(type, order);
            if (image != null)
            {
                string completePath = Path.Combine(_webHostEnvironment.WebRootPath, image.Path.Substring(1));
                System.IO.File.Delete(completePath);
            }
        }

        [HttpPost]
        public IActionResult Upload(UploadImageFormViewModel model)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (model.InputFile == null) return RedirectToAction("IndexOnwer");
                DateTime todayDate = DateTime.Today;
                var fileExtension = Path.GetExtension(model.InputFile.FileName);
                string fileNameWithOutExtension = model.ImageType.ToString() + '_' + todayDate.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss");
                string fileName = fileNameWithOutExtension + fileExtension;
                string rutaGuardar = Path.Combine(_webHostEnvironment.WebRootPath, "usersData", model.ImageType.ToString());
                string networkPath = "/usersData/" + model.ImageType.ToString();
                if (!Directory.Exists(rutaGuardar)) Directory.CreateDirectory(rutaGuardar);
                string filePath = Path.Combine(rutaGuardar, fileName);
                string fileNetworkPath = networkPath + "/" + fileName;
                DeleteFilesOfLastOrder(model, fileNameWithOutExtension, rutaGuardar);
                SaveImageInCreatedFolderAndUploadInDB(model, filePath, fileNetworkPath);
                return RedirectToAction("IndexOwner");
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

        private void SaveImageInCreatedFolderAndUploadInDB(UploadImageFormViewModel model, string filePath, string fileNetworkPath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.InputFile.CopyTo(stream);
                ImageData userData = _mapper.Map<ImageData>(model);
                userData.Order = 1;
                userData.Path = fileNetworkPath;
                //I add an order, because, the inserted image, now should be at order 1
                _userDataRepository.AddOrder(model.ImageType);
                _userDataRepository.UploadImage(userData);
            }
        }

        private void DeleteFilesOfLastOrder(UploadImageFormViewModel model, string fileNameWithOutExtension, string rutaGuardar)
        {
            var deleteImage = _userDataRepository.GetImage(model.ImageType, 3);
            if (deleteImage != null)
            {
                //Save the path like this, to delete inicial / in the path
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, deleteImage.Path.Substring(1)));
                _userDataRepository.DeleteImage(model.ImageType, 3);

            }
        }
        [HttpPost]
        public IActionResult Update(UpdateImageFormViewModel model)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (model.InputFile == null) return RedirectToAction("IndexOnwer");
                DateTime todayDate = DateTime.Today;
                var fileExtension = Path.GetExtension(model.InputFile.FileName);
                string fileNameWithOutExtension = model.ImageType.ToString() + '_' + todayDate.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss");
                string fileName = fileNameWithOutExtension + fileExtension;
                string rutaGuardar = Path.Combine(_webHostEnvironment.WebRootPath, "usersData", model.ImageType.ToString());
                string networkPath = "/usersData/" + model.ImageType.ToString();
                if (!Directory.Exists(rutaGuardar)) Directory.CreateDirectory(rutaGuardar);
                string filePath = Path.Combine(rutaGuardar, fileName);
                string fileNetworkPath = networkPath + "/" + fileName;
                DeleteImageFromLocalEnviorment(model);
                SaveImageInCreatedFolderAnUpdateInDB(model, filePath, fileNetworkPath);
                return RedirectToAction("IndexOwner");
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
                if(IsOperative())
                {
                    return RedirectToAction("ViewInversion", new { id = IdLoguedUser() });
                }
                else
                {
                    return RedirectToRoute(new{ Controller="User",action="Index"});
                }
            }catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        private void DeleteImageFromLocalEnviorment(UpdateImageFormViewModel model)
        {
            var deleteImage = _userDataRepository.GetImage(model.ImageType, model.Order);
            if (deleteImage != null)
            {

                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, deleteImage.Path.Substring(1)));
            }
            else
            {
                throw new InconsistenceInTheDBException("Existe mas de una imagen para un mismo usuario");
            }
        }

        private void SaveImageInCreatedFolderAnUpdateInDB(UpdateImageFormViewModel model, string filePath, string fileNetworkPath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.InputFile.CopyTo(stream);
                ImageData userData = _mapper.Map<ImageData>(model);
                userData.Path = fileNetworkPath;
                _userDataRepository.UpdateImage(userData, model.Order);
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