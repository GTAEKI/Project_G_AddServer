using Microsoft.AspNetCore.Mvc;
using WebServer.Services;

namespace WebServer.Controllers
{
    // ip:port/scrap
    [ApiController]
    [Route("scrap")]
    public class ScrapController : ControllerBase
    {
        ScrapService _scrapService;

        public ScrapController(ScrapService scrapService) 
        {
            _scrapService = scrapService;
        }

        [HttpPost]
        [Route("input")]
        public ScrapPacketRes InputScrap([FromBody] ScrapPacketReq value)
        {
            ScrapPacketRes result = new ScrapPacketRes();

            _scrapService.InputScrapByPlayerId(value.userId, value.scrap);

            result.success = true;
            result.scrap = value.scrap;

            return result;
        }

        [HttpPost]
        [Route("get")]
        public ScrapPacketRes GetScrap([FromBody] ScrapPacketReq value)
        {
            ScrapPacketRes result = new ScrapPacketRes();

            result.scrap = _scrapService.GetScrapByPlayerId(value.userId);
            result.success = true;

            return result;
        }


    }
}
