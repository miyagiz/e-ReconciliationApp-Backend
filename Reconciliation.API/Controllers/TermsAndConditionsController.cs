using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsAndConditionsController : ControllerBase
    {
        private readonly ITermsAndConditionService _termsAndConditionService;

        public TermsAndConditionsController(ITermsAndConditionService termsAndConditionService)
        {
            _termsAndConditionService = termsAndConditionService;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var result = _termsAndConditionService.Get();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(TermsAndCondition termsAndCondition)
        {
            var result = _termsAndConditionService.Update(termsAndCondition);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
