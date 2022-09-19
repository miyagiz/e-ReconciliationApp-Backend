using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Business.ValidationRules.FluentValidation;
using Reconciliation.Core.Aspects.Autofac.Validation;
using Reconciliation.Core.Utilities.Results.Concrete;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyAccountController : ControllerBase
    {
        private readonly ICurrencyAccountService _currencyAccountService;

        public CurrencyAccountController(ICurrencyAccountService currencyAccountService)
        {
            _currencyAccountService = currencyAccountService;
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

                var result = _currencyAccountService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya Seçimi Yapmadınız");
        }


        [ValidationAspect(typeof(CurrencyAccountValidator))]
        [HttpPost("Add")]
        public IActionResult Add(CurrencyAccount currencyAccount)
        {
            var result = _currencyAccountService.Add(currencyAccount);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [ValidationAspect(typeof(CurrencyAccountValidator))]
        [HttpPost("Update")]
        public IActionResult Update(CurrencyAccount currencyAccount)
        {
            var result = _currencyAccountService.Update(currencyAccount);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(CurrencyAccount currencyAccount)
        {
            var result = _currencyAccountService.Delete(currencyAccount);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _currencyAccountService.Get(id);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int companyId)
        {
            var result = _currencyAccountService.GetList(companyId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
