using Microsoft.AspNetCore.Antiforgery;
using MyFirstABPProject.Controllers;

namespace MyFirstABPProject.Web.Host.Controllers
{
    public class AntiForgeryController : MyFirstABPProjectControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
