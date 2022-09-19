using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationDetailsController : ControllerBase
    {
        private readonly IBaBsReconciliationDetailsService _baBsReconciliationDetailsService;

        public BaBsReconciliationDetailsController(IBaBsReconciliationDetailsService baBsReconciliationDetailsService)
        {
            _baBsReconciliationDetailsService = baBsReconciliationDetailsService;
        }

        [HttpPost("AddFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int babsReconciliationId)
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

                var result = _baBsReconciliationDetailsService.AddToExcel(filePath, babsReconciliationId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya Seçimi Yapmadınız");
        }

        [HttpPost("Add")]
        public IActionResult Add(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailsService.Add(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailsService.Update(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailsService.Delete(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _baBsReconciliationDetailsService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int babsReconciliationId)
        {
            var result = _baBsReconciliationDetailsService.GetList(babsReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
