using PetShop.BusinessLogic;
using PetShop.BusinessLogic.Interfaces;
using System;
using Hangfire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PetShop.Domain.Entities.Response;

namespace PetShop.Web.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ISesion _session;
        public MaintenanceController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
        [HttpPost]
        public async Task<ActionResult> CleanupGuestUsers()
        {

            var cleanupResp = await _session.CleanupGuestUsersAsync();

            if (cleanupResp.Status)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, errorMessage = "An error occurred while processing the maintenance task." });
            }

        }
    }
}