using BackeryShop.Web.Models.ViewModels;
using BackeryShopDomain.DataModel;
using BackeryShopDomain.DataModel.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace BackeryShop.Web.Controllers
{
    public class HomeController : Controller
	{
		public ActionResult Index()
		{

            var model = new HomeViewModel
            {
                BackeriesList = TurnoverRepository.GetAllBackeries()
            };
            return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}