using Microsoft.AspNetCore.Mvc;
using PubliSub.Banking.Application.Interfaces;
using PubliSub.Banking.Application.Models;
using PubliSub.Banking.Domain.Models;

namespace PubliSub.Banking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get() 
        {
            return Ok(_accountService.GetAccounts());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer) 
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }
    }
}