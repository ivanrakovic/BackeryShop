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

		public ActionResult Settings()
		{
			return View();
		}

		public ActionResult Contact()
		{
			

			return View();
		}
	}
}