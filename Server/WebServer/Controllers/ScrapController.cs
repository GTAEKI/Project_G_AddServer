using Microsoft.AspNetCore.Mvc;
using WebServer.Services;

namespace WebServer.Controllers
{
    // ip:port/scrap
    [ApiController]
    [Route("scrap")]
    public class ScrapController : ControllerBase
    {
        AccountService _service;

        public ScrapController(AccountService service) 
        {
            _service = service;
        }

        //ip:port/scrap/post
        [HttpPost]
        [Route("post")]
        public ScrapPacketRes ScrapPost([FromBody] ScrapPacketReq value)
        {
            ScrapPacketRes result = new ScrapPacketRes();
            result.success = true;

            int id = _service.GenerateAccountId();

            return result;
        }
    }
}
