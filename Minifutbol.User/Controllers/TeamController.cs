using Minifutbol.BL.Extensions;
using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.TeamRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minifutbol.User.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private TeamLogic _teamLogic;

        public TeamController()
        {
            _teamLogic = new TeamLogic();
        }
        // GET: Team
        public ActionResult Index()
        {
            var teams = _teamLogic.GetAll(new BL.Models.Core.Filter());
            return View(teams.Output);
        }

        public ActionResult JoinTeam(int id)
        {
            var _requestLogic = new TeamRequestLogic();
            var UserId = Request.GetOwinContext().Authentication.User.GetUserId();
            var opResult = _requestLogic.Add(new TeamRequestCreateModel
            {
                TeamId = id,
                UserId = UserId
            });

            return RedirectToAction("index");

        }
        public ActionResult ExitTeam(int id)
        {
            _teamLogic.ExitTeam(null);
            return RedirectToAction("index");
        }

        public ActionResult TeamMates(int id)
        {
            var _userLogic = new UserLogic();
            var users = _userLogic.FindAll(new BL.Models.User.UserFindModel { TeamId = id });
            return View(users.Output);
        }
    }
}