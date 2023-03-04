using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemo.Controllers
{
    public class SampleController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
