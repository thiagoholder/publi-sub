using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using PubliSub.Transfer.Application.Interfaces;
using PubliSub.Transfer.Domain.Models;

namespace PubliSub.Transfer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
           return Ok(_transferService.GetTransferLogs());
        }
    }
}