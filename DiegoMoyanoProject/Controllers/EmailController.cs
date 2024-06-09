using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DiegoMoyanoProject.Models;
using DiegoMoyanoProject.Repository;
using DiegoMoyanoProject.ViewModels.Mail;
namespace DiegoMoyanoProject.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender _IEmailSenderRepository;
        private readonly IUserRepository _IUserRepository;
        private readonly ILogger<EmailController> _logger;
        private readonly IMapper _mapper;

        public EmailController(IEmailSender iEmailSenderRepository, IUserRepository iUserRepository, ILogger<EmailController> logger, IMapper mapper)
        {
            _IEmailSenderRepository = iEmailSenderRepository;
            _IUserRepository = iUserRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
          try
            {
                return RedirectToRoute(new { Controller = "UserData", Action = "Index" });
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Invertir(decimal CapitalInvested)
        {
            try
            {
                if (IsNotLogued()) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                if (LoguedUserRole() != Role.Operative) { throw new Exception("El usuario no es operativo, por lo cual no puede acceder"); }
                var usu = _IUserRepository.GetUserById(IdLoguedUser());
                return View(new InvertirEmailViewModel(usu.Username, usu.Mail,CapitalInvested));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Invertir(InvertirEmailViewModel emailVM)
        {
            try
            {
                if (IsNotLogued()) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                if (LoguedUserRole() != Role.Operative) { throw new Exception("El usuario no es operativo, por lo cual no puede acceder"); }
                Email mail = _mapper.Map<Email>(emailVM);
                mail.Invest();
                await _IEmailSenderRepository.SendEmail(mail);
                return RedirectToAction("MailEnviado", new {enviado=true});
            }catch(Exception ex)
                {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult Retirar(decimal CapitalInvested)
        {
            try
            {
                if (IsNotLogued()) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                if (LoguedUserRole() != Role.Operative) { throw new Exception("El usuario no es operativo, por lo cual no puede acceder"); }
                var usu = _IUserRepository.GetUserById(IdLoguedUser());
                return View(new RetirarEmailViewModel(usu.Username, usu.Mail,CapitalInvested));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Retirar(RetirarEmailViewModel emailVM)
        {
            try
            {
                if (IsNotLogued()) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
                if (LoguedUserRole() != Role.Operative) { throw new Exception("El usuario no es operativo, por lo cual no puede acceder"); }
                Email mail = _mapper.Map<Email>(emailVM);
                mail.Retire();
                await _IEmailSenderRepository.SendEmail(mail);
                return RedirectToAction("MailEnviado", new { enviado = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]

        public IActionResult MailEnviado(bool enviado)
        {
            if (IsNotLogued()) { return RedirectToRoute(new { Controller = "Login", Action = "Index" }); }
            if (LoguedUserRole() != Role.Operative) { throw new Exception("El usuario no es operativo, por lo cual no puede acceder"); }
            if (enviado == false) { throw new Exception("ERROR AL ENVIAR EL MENSAJE"); }
            try
            {
                return View();
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult MoreInfo(string title, string body)
        {
            try
            {
                return View(new MoreInfoViewModel(title, body));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> MoreInfo(MoreInfoViewModel emailVM)
        {
            try
            {
                Email mail = _mapper.Map<Email>(emailVM);
                mail.MoreInfo();
                await _IEmailSenderRepository.SendEmail(mail);
                return RedirectToAction("MailEnviadoSinLoguear", new { enviado = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]

        public IActionResult MailEnviadoSinLoguear(bool enviado)
        {
            if (enviado == false) { throw new Exception("ERROR AL ENVIAR EL MENSAJE"); }
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
    }
}
