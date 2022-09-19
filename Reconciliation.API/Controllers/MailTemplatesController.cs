using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reconciliation.Business.Absctract;
using Reconciliation.Entities.Concrete;

namespace Reconciliation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailTemplatesController : ControllerBase
    {
        private readonly IMailTemplateService _mailTemplateService;

        public MailTemplatesController(IMailTemplateService mailTemplateService)
        {
            _mailTemplateService = mailTemplateService;
        }

        [HttpPost("add")]
        public IActionResult Add(MailTemplate mailTemplate)
        {
            var result = _mailTemplateService.Add(mailTemplate);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
