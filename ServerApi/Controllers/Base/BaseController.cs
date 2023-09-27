using Microsoft.AspNetCore.Mvc;

namespace ServerApi.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
