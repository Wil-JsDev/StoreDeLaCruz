using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreDeLaCruz.Controllers
{
    [ApiController]
    [Route("api/v{version: apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
