using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Core.Utilities.Hashing;
using Reconciliation.Core.Utilities.Results.Concrete;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IForgotPasswordService _forgotPasswordService;

        public AuthController(IAuthService authService, IForgotPasswordService forgotPasswordService)
        {
            _authService = authService;
            _forgotPasswordService = forgotPasswordService;
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
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Kullanıcı Pasif Durumda. Aktif Etmek İçin Yöneticinize Danışınız.");
        }

        [HttpGet("ConfirmUser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            if (user.MailConfirm)
            {
                return BadRequest("Kullanıcı Maili Zaten Onaylanmış.");
            }
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
        public IActionResult SendConfirmEmail(string email)
        {
            var user = _authService.GetByMail(email).Data;
            if (user == null)
            {
                return BadRequest("Bu Mail Adresine Kayıtlı Bir Hesap Bulunamadı.");
            }

            if (user.MailConfirm)
            {
                return BadRequest("Hesabınızın Maili Zaten Onaylı!");
            }

            var result = _authService.SendConfirmEmailAgain(user);

            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            //var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var user = _authService.GetByMail(email).Data;

            if (user == null)
            {
                return BadRequest("Bu Mail Adresine Kayıtlı Bir Hesap Bulunamadı.");
            }

            var lists = _forgotPasswordService.GetListByUserId(user.Id).Data;

            foreach (var item in lists)
            {
                item.IsActive = false;
                _forgotPasswordService.Update(item);
            }

            var forgotPassword = _forgotPasswordService.CreateForgotPassword(user).Data;

            var result = _authService.SendForgotPasswordEmail(user, forgotPassword.Value);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("ForgotPasswordLinkCheck")]
        public IActionResult ForgotPasswordLinkCheck(string value)
        {
            var result = _forgotPasswordService.GetForgotPassword(value);

            if (result == null)
            {
                return BadRequest("Tıkladığınız Mail Sıfırlama Linki Geçersizdir.");
            }

            if (result.IsActive == true)
            {
                DateTime date1 = DateTime.Now.AddHours(-1);
                DateTime date2 = DateTime.Now;

                if (result.SendDate >= date1 && result.SendDate <= date2)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Tıkladığınız Mail Sıfırlama Linki Geçersizdir.");
                }
            }
            else
            {
                return BadRequest("Tıkladığınız Mail Sıfırlama Linki Geçersizdir.");
            }
        }

        [HttpPost("ChangePasswordToForgetPassword")]
        public IActionResult ChangePasswordToForgetPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var forgotPasswordResult = _forgotPasswordService.GetForgotPassword(forgotPasswordDto.Value);

            forgotPasswordResult.IsActive = false;
            _forgotPasswordService.Update(forgotPasswordResult);

            var userResult = _authService.GetById(forgotPasswordResult.UserId).Data;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(forgotPasswordDto.Password, out passwordHash, out passwordSalt);

            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;

            var result = _authService.ChangePassword(userResult);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
