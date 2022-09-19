using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationController : ControllerBase
    {
        private readonly IAccountReconciliationService _accountReconciliationService;

        public AccountReconciliationController(IAccountReconciliationService accountReconciliationService)
        {
            _accountReconciliationService = accountReconciliationService;
        }

        [HttpPost("AddFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int companyId)
        {
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx";
                var filePath = $"{Directory.GetCurrentDirectory()}/Content/{fileName}";

                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                var result = _accountReconciliationService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya Seçimi Yapmadınız");
        }

        [HttpPost("Add")]
        public IActionResult Add(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Add(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Update(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Delete(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int companyId)
        {
            var result = _accountReconciliationService.GetListDto(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("SendReconciliationMail")]
        public IActionResult SendReconciliationMail(AccountReconciliationDto accountReconciliationDto)
        {
            var result = _accountReconciliationService.SendReconciliationMail(accountReconciliationDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetByCode")]
        public IActionResult GetByCode(string code)
        {
            var result = _accountReconciliationService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
