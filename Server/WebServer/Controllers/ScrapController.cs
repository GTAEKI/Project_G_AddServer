using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    // ip:port/scrap
    [ApiController]
    [Route("scrap")]
    public class ScrapController : ControllerBase
    {
        //ip:port/scrap/post
        [HttpPost]
        [Route("post")]
        public ScrapPacketRes ScrapPost([FromBody] ScrapPacketReq value)
        {
            ScrapPacketRes result = new ScrapPacketRes();
            result.success = true;

            return result;
        }
    }
}
