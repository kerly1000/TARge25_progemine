using Microsoft.AspNetCore.Mvc;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;

namespace TARge25Shop.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly IKindergartenServices _kindergartenService;
        private readonly TARge25ShopContext _context;

        public IActionResult Index()
        {
            return View();
        }
    }
}
