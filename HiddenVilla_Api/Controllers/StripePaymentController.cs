using Microsoft.AspNetCore.Mvc;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
