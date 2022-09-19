using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegister)
        {
            var userExist = _authService.UserExist(userAndCompanyRegister.UserForRegister.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }

            var companyExist = _authService.CompanyExists(userAndCompanyRegister.Company);
            if (!companyExist.Success)
            {
                return BadRequest(userExist.Message);
            }

            var registerResult = _authService.Register(userAndCompanyRegister.UserForRegister, userAndCompanyRegister.UserForRegister.Password, userAndCompanyRegister.Company);

            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }


        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegisterToSecondAccountDto userForRegister)
        {
            var userExist = _authService.UserExist(userForRegister.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }

            var registerResult = _authService.RegisterSecondAccount(userForRegister, userForRegister.Password, userForRegister.CompanyId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegister.CompanyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            if (userToLogin.Data.IsActive)
            {
                var userCompany = _authService.GetUserCompany(userToLogin.Data.Id).Data;

                var result = _authService.CreateAccessToken(userToLogin.Data, userCompany.CompanyId);

                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Kullanıcı Pasif Durumda. Aktif Etmek İçin Yöneticinize Danışınız.");
        }

        [HttpGet("ConfirmUser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("SendConfirmEmail")]
        public IActionResult SendConfirmEmail(int id)
        {
            var user = _authService.GetById(id).Data;
            var result = _authService.SendConfirmEmail(user);

            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            //var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
