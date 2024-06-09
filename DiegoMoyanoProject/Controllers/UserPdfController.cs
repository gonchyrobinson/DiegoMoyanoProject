using DiegoMoyanoProject.Repository;
using Microsoft.AspNetCore.Mvc;
using DiegoMoyanoProject.Exceptions;
using DiegoMoyanoProject.Models;
using AutoMapper;
using DiegoMoyanoProject.ViewModels.UserData;
using DiegoMoyanoProject.ViewModels.UserPdf;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var listDates = _userPdfRepoository.GetAllDates();
                DateTime maxDate = DateTime.Today;
                if (listDates.Count() > 0) maxDate = listDates.Max();
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
                var pdfNetworkPath = _userPdfRepoository.GetPdf(date);
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
        public IActionResult IndexOwner(DateTime date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                var pdf = _userPdfRepoository.GetPdfData(date);
                var dates = _userPdfRepoository.GetAllDates();
                return View(new IndexOwnerUserPdfViewModel(pdf, date, dates));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (BadRequest());
            }
        }

        [HttpGet]
        public IActionResult UpdatePdfForm(string date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DateTime dateTime;

                if (DateTime.TryParse(date, out dateTime))
                {
                    return View(new UpdatePdfFormViewModel(dateTime));
                }
                else
                {
                    _logger.LogError("Error al convertir fecha");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        public IActionResult Delete(string date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DateTime dateTime;

                if (DateTime.TryParse(date, out dateTime))
                {
                    _userPdfRepoository.DeletePdf(dateTime);
                }
                return RedirectToAction("IndexOwner",new {date = dateTime});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        public  IActionResult Upload(string date)
        {
            try
            {
                if (IsNotLogued() || !IsOwner()) return RedirectToAction("Index", "Login");
                DateTime dateTime;

                if (DateTime.TryParse(date, out dateTime))
                {
                    _userPdfRepoository.AddDate(dateTime);
                }

                return RedirectToAction("IndexOwner", new {date = dateTime});
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
        public async Task<IActionResult> Update(UpdatePdfFormViewModel model)
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
                    // Retorna los bytes del MemoryStream
                    _userPdfRepoository.UpdatePdf(new PdfData(memoryStream.ToArray()), model.Date);
                }
                return RedirectToAction("IndexOwner", new {date = model.Date});
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


        public FileResult OnGetDownloadFileFromFolder(string pdf)
        {
            try
            {
                // Decodifica la cadena Base64 a un arreglo de bytes
                byte[] pdfBytes = Convert.FromBase64String(pdf);

                // Devuelve el archivo PDF como una descarga
                return File(pdfBytes, "application/octet-stream", "reporte.pdf");
            }
            catch (Exception ex)
            {
                // Log the exception with as much detail as possible
                _logger.LogError(ex, $"Error when trying to send PDF to client");
                throw new BadHttpRequestException("Error al descargar pdf");
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