using DiegoMoyanoProject.Repository;
using Microsoft.AspNetCore.Mvc;
using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using AutoMapper;
using DiegoMoyanoProject.ViewModels.UserData;
using DiegoMoyanoProject.ViewModels.UserPdf;
using Microsoft.Extensions.Hosting;

namespace DiegoMoyanoProject.Controllers
{
    public class UserPdfController : Controller
    {
        private readonly IUserPdfRepository _userPdfRepoository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UserPdfController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserPdfController(IUserPdfRepository userPdfRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, ILogger<UserPdfController> logger, IMapper mapper)
        {
            _userPdfRepoository = userPdfRepository;
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
                int order = 1;
                var pdfNetworkPath = _userPdfRepoository.PdfPath(order);
                var listDates = _userPdfRepoository.GetAllDates();
                return View(new IndexUserPdfViewModel(pdfNetworkPath, listDates));
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
                var pdfNetworkPath = _userPdfRepoository.PdfPath(date);
                var listDates = _userPdfRepoository.GetAllDates();
                return View(new IndexDateUserPdfViewModel(pdfNetworkPath, listDates));
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
                int order = 1;
                var pdf = _userPdfRepoository.GetPdf(order);
                return View(new IndexOwnerUserPdfViewModel(pdf));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (BadRequest());
            }
        }
        [HttpGet]
        public IActionResult UploadPdfForm()
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UploadPdfFormViewModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult UpdatePdfForm(int order)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                return View(new UpdatePdfFormViewModel(order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult Delete(int order)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DeletePdf(order);
                return RedirectToAction("IndexOwner");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        private void DeletePdf(int order)
        {
            var pdf = _userPdfRepoository.GetPdf(order);
            if (pdf != null)
            {
                _userPdfRepoository.DeletePdf(order);
                _userPdfRepoository.ReduceOrder();
                System.IO.File.Delete(pdf.Path);
            }
        }

        [HttpPost]
        public IActionResult Upload(UploadPdfFormViewModel model)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (model.InputFile == null) return RedirectToAction("IndexOnwer");
                DateTime todayDate = DateTime.Today;
                var fileExtension = Path.GetExtension(model.InputFile.FileName);
                string fileNameWithOutExtension = "Report" + '_' + todayDate.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss");
                string fileName = fileNameWithOutExtension + fileExtension;
                string rutaGuardar = Path.Combine(_webHostEnvironment.WebRootPath, "userPdf");
                if (!Directory.Exists(rutaGuardar)) Directory.CreateDirectory(rutaGuardar);
                string filePath = Path.Combine(rutaGuardar, fileName);
                DeleteFilesOfLastOrder();
                SaveImageInCreatedFolderAndUploadInDB(model, filePath);
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

        private void SaveImageInCreatedFolderAndUploadInDB(UploadPdfFormViewModel model, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.InputFile.CopyTo(stream);
                int order = 1;
                PdfData pdfData = new PdfData(filePath, order);
                //I add an order, because, the inserted image, now should be at order 1
                _userPdfRepoository.AddOrder();
                _userPdfRepoository.UploadPdf(pdfData);
            }
        }

        private void DeleteFilesOfLastOrder()
        {
            int order = 3;
            var deletePdf = _userPdfRepoository.GetPdf(order);
            if (deletePdf != null)
            {
                //Save the path like this, to delete inicial / in the path
                System.IO.File.Delete(deletePdf.Path);
                _userPdfRepoository.DeletePdf(order);
            }
        }
        [HttpPost]
        public IActionResult Update(UpdatePdfFormViewModel model)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                if (!ModelState.IsValid) throw (new ModelStateInvalidException());
                if (model.InputFile == null) return RedirectToAction("IndexOnwer");
                DateTime todayDate = DateTime.Today;
                var fileExtension = Path.GetExtension(model.InputFile.FileName);
                string fileNameWithOutExtension = "Report" + '_' + todayDate.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss");
                string fileName = fileNameWithOutExtension + fileExtension;
                string rutaGuardar = Path.Combine(_webHostEnvironment.WebRootPath, "userPdf");
                if (!Directory.Exists(rutaGuardar)) Directory.CreateDirectory(rutaGuardar);
                string filePath = Path.Combine(rutaGuardar, fileName);
                DeleteImageFromLocalEnviorment(model);
                SaveImageInCreatedFolderAnUpdateInDB(model, filePath);
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


        private void DeleteImageFromLocalEnviorment(UpdatePdfFormViewModel model)
        {
            var deletePdf = _userPdfRepoository.GetPdf(model.Order);
            if (deletePdf != null)
            {
                System.IO.File.Delete(deletePdf.Path);
            }
            else
            {
                throw new InconsistenceInTheDBException("Existe mas de una imagen para un mismo usuario");
            }
        }

        private void SaveImageInCreatedFolderAnUpdateInDB(UpdatePdfFormViewModel model, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.InputFile.CopyTo(stream);
                PdfData pdfData = new PdfData(filePath, model.Order);
                _userPdfRepoository.UpdatePdf(pdfData, model.Order);
            }
        }
        public IActionResult OnGetDownloadFileFromFolder(string path)
        {
            try
            {
                //Read the File data into Byte Array.  
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                //Send the File to Download.  
                var partPath = path.Split(@"\");
                // Log that the request has been received
                _logger.LogInformation($"Request received for path: {path}");
                // Log the success of the operation
                _logger.LogInformation($"PDF successfully sent to client for path: {path}");
                return File(bytes, "application/octet-stream", partPath[partPath.Length - 1]);

            }
            catch (Exception ex)
            {
                // Log the exception with as much detail as possible
                _logger.LogError(ex, $"Error when trying to send PDF to client for path: {path}");
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