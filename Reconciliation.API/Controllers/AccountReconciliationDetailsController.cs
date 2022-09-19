using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationDetailsController : ControllerBase
    {
        private readonly IAccountReconciliationDetailService _accountReconciliationDetailService;

        public AccountReconciliationDetailsController(IAccountReconciliationDetailService accountReconciliationDetailService)
        {
            _accountReconciliationDetailService = accountReconciliationDetailService;
        }

        [HttpPost("AddFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int accountReconciliationId)
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

                var result = _accountReconciliationDetailService.AddToExcel(filePath, accountReconciliationId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya Seçimi Yapmadınız");
        }

        [HttpPost("Add")]
        public IActionResult Add(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = _accountReconciliationDetailService.Add(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = _accountReconciliationDetailService.Update(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = _accountReconciliationDetailService.Delete(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconciliationDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int accountReconciliationId)
        {
            var result = _accountReconciliationDetailService.GetList(accountReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
