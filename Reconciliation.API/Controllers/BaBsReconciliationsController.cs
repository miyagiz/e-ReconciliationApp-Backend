﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationsController : ControllerBase
    {
        private readonly IBaBsReconciliationService _baBsReconciliationService;

        public BaBsReconciliationsController(IBaBsReconciliationService baBsReconciliationService)
        {
            _baBsReconciliationService = baBsReconciliationService;
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

                var result = _baBsReconciliationService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya Seçimi Yapmadınız");
        }

        [HttpPost("Add")]
        public IActionResult Add(BaBsReconciliation baBsReconciliation)
        {
            var result = _baBsReconciliationService.Add(baBsReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(BaBsReconciliation baBsReconciliation)
        {
            var result = _baBsReconciliationService.Update(baBsReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(BaBsReconciliation baBsReconciliation)
        {
            var result = _baBsReconciliationService.Delete(baBsReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _baBsReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int companyId)
        {
            var result = _baBsReconciliationService.GetListDto(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("SendReconciliationMail")]
        public IActionResult SendReconciliationMail(BaBsReconciliationDto baBsReconciliationDto)
        {
            var result = _baBsReconciliationService.SendReconciliationMail(baBsReconciliationDto);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetByCode")]
        public IActionResult GetByCode(string code)
        {
            var result = _baBsReconciliationService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}