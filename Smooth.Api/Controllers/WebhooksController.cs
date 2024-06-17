using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers;

[Route(Ctrls.WEBHOOKS)]
public class WebhooksController : ControllerBase
{
    [HttpPost]
    [Route(Routes.POST_UPLOADED)]
    public void UploadedAsync()
    {
        var x = 5;
    }
}
