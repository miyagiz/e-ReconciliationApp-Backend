using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("GetCompanyList")]
        public IActionResult GetCompanyList()
        {
            var result = _companyService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetCompany")]
        public IActionResult GetCompany(int id)
        {
            var result = _companyService.GetById(id);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("UpdateCompany")]
        public IActionResult UpdateCompany(Company company)
        {
            var result = _companyService.Update(company);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("AddCompany")]
        public IActionResult AddCompany(CompanyDto companyDto)
        {
            var result = _companyService.Add(companyDto.Company);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("AddCompanyAndUserCompany")]
        public IActionResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            var result = _companyService.AddCompanyAndUserCompany(companyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
