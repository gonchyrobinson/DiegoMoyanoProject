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
        private readonly IMapper _mapper;


        public UserDataController(IUserDataRepository userDataRepository, IWebHostEnvironment webHostEnvironment, ILogger<UserDataController> logger, IMapper mapper)
        {
            _userDataRepository = userDataRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            try
            {

                if (IsNotLogued()   ||  ((IsOwner()  ||   IsAdmin())  && id==null )) return RedirectToAction("Index", "Login");
                if (IsOwner() && id!=null) return RedirectToAction("IndexOwner", new { id = id });
                var currentUserId = IdLoguedUser();
                if (IsAdmin() && id!=null) currentUserId = (int)id;
                var listImages = _userDataRepository.GetUserImages(currentUserId);
                var listImagesVm = _mapper.Map<List<ImageDataViewModel>>(listImages);
                var isadminOrOwner = IsAdmin() || IsOwner();
                var isloguedUser = IsOperative();
                return View(new IndexUserDataViewModel(currentUserId, listImagesVm, isloguedUser, isadminOrOwner));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult IndexOwner(int id)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                var vm = new IndexOwnerUserDataViewModel(id);
                vm.Sales = _userDataRepository.GetImage(id, ImageType.Sales);
                vm.SpentMoney = _userDataRepository.GetImage(id, ImageType.SpentMoney);
                vm.Campaigns = _userDataRepository.GetImage(id, ImageType.Campaigns);
                vm.Listings = _userDataRepository.GetImage(id, ImageType.Listings);
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (BadRequest());
            }
        }
        [HttpGet]
        public IActionResult UploadImageForm(int userId, ImageType type)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UploadImageFormViewModel(userId, type));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult Delete(int userId, ImageType type)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DeleteImage(userId, type);
                return RedirectToAction("IndexOwner", new { id = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private void DeleteImage(int userId, ImageType type)
        {
            string? path = _userDataRepository.GetImagePath(userId, type);
            _userDataRepository.DeleteImage(userId, type);
            if (path != null)
            {
                string completePath = Path.Combine(_webHostEnvironment.WebRootPath, path.Substring(1));
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
                if (model.InputFile == null) return RedirectToAction("IndexOnwer", new { id = model.UserId });
                var fileExtension = Path.GetExtension(model.InputFile.FileName);
                string fileNameWithOutExtension = model.UserId.ToString() + '-' + model.ImageType.ToString();
                string fileName = fileNameWithOutExtension + fileExtension;
                string rutaGuardar = Path.Combine(_webHostEnvironment.WebRootPath, "usersData", model.UserId.ToString());
                string networkPath = "/usersData/" + model.UserId.ToString();
                if (!Directory.Exists(rutaGuardar)) Directory.CreateDirectory(rutaGuardar);
                string filePath = Path.Combine(rutaGuardar, fileName);
                string fileNetworkPath = networkPath + "/" + fileName;
                DeleteFilesWithTheSameName(model, fileNameWithOutExtension, rutaGuardar);
                SaveImageInCreatedFolder(model, filePath, fileNetworkPath);
                return RedirectToAction("IndexOwner", new { id = model.UserId });
            }
            catch (InconsistenceInTheDBException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private void SaveImageInCreatedFolder(UploadImageFormViewModel model, string filePath, string fileNetworkPath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.InputFile.CopyTo(stream);
                ImageData userData = _mapper.Map<ImageData>(model);
                userData.Path = fileNetworkPath;
                _userDataRepository.UploadImage(userData);
            }
        }

        private void DeleteFilesWithTheSameName(UploadImageFormViewModel model, string fileNameWithOutExtension, string rutaGuardar)
        {
            var filesWithFileName = Directory.GetFiles(rutaGuardar, $"{fileNameWithOutExtension}.*");
            if (filesWithFileName.Count() > 0)
            {
                if (_userDataRepository.ExistsImage(model.UserId, model.ImageType))
                {
                    System.IO.File.Delete(Path.Combine(rutaGuardar, filesWithFileName[0]));
                    _userDataRepository.DeleteImage(model.UserId, model.ImageType);
                }
                else
                {
                    throw new InconsistenceInTheDBException("Existe mas de una imagen para un mismo usuario");
                }
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
        }private bool IsOperative()
        {
            return LoguedUserRole() == Role.Operative;
        }
    }
}