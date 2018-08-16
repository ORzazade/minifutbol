using Minifutbol.BL.Extensions;
using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.Team;
using Minifutbol.BL.Models.TeamRequest;
using Minifutbol.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minifutbol.User.Controllers
{
    [Authorize(Roles = "admin")]
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TeamCreateModel parameters)
        {
            var opResult = _teamLogic.Add(parameters);
            if (opResult.IsSuccess)
            {
                return RedirectToAction("index");
            }
            return View(parameters);
        }

        public ActionResult Update(int id)
        {
            var opREsult = _teamLogic.GetById(id);
            return View(opREsult.Output);
        }

        [HttpPost]
        public ActionResult Update(TeamUpdateModel parameters)
        {
            var opResult = _teamLogic.Update(parameters);
            if (opResult.IsSuccess)
            {
                return RedirectToAction("index");
            }
            return View(parameters);
        }

        public ActionResult Details(int id)
        {
            var opREsult = _teamLogic.GetById(id);
            return View(opREsult.Output);
        }
        public ActionResult Delete(int id)
        {
            var opResult = _teamLogic.Remove(new TeamDeleteModel { Id = id });
            if (opResult.IsSuccess)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("details", new { id });
        }

        [HttpPost]
        public ActionResult AddPlayer(int teamId, int userId)
        {
            _teamLogic.AddPlayer(userId, teamId);

            return RedirectToAction("players", new { id = teamId });

        }
        public ActionResult RemovePlayer(int id, int teamId)
        {
            _teamLogic.ExitTeam(id);
            return RedirectToAction("players", new { id = teamId });
        }

        public ActionResult UpdatePlayer(int id)
        {
            ViewBag.Teams = _teamLogic.GetAll(new BL.Models.Core.Filter()).Output;
            var _userlogic = new UserLogic();
            var opResult = _userlogic.GetById(id);
            return View(opResult.Output);
        }

        [HttpPost]
        public ActionResult UpdatePlayer(UserUpdateModel parameters)
        {
            var _userlogic = new UserLogic();
            var opResult = _userlogic.Update(parameters);
            if (opResult.IsSuccess)
            {
                return RedirectToAction("players", new { id = opResult.Output.TeamId });
            }
            return View(parameters);
        }
        public ActionResult Players(int id)
        {
            var _userlogic = new UserLogic();
            @ViewBag.TeamId = id;
            ViewBag.Players = _userlogic.GetAll(new BL.Models.Core.Filter()).Output.Where(s => s.TeamId != id).ToList();
            var _userLogic = new UserLogic();
            var users = _userLogic.FindAll(new BL.Models.User.UserFindModel { TeamId = id });
            return View(users.Output);
        }
    }
}