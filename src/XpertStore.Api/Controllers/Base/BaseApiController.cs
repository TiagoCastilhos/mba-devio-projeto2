using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XpertStore.Api.Controllers.Base;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    public BaseApiController()
    {

    }
}
