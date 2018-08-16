using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.Game;
using Minifutbol.BL.Models.GameResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minifutbol.Admin.Controllers
{
    public class GameController : Controller
    {
        // GET: Game

        private GameLogic _gameLogic;
        public GameController()
        {
            _gameLogic = new GameLogic();

        }
        public ActionResult Index()
        {
            var games = _gameLogic.GetAll(new BL.Models.Core.Filter()).Output.OrderByDescending(x => x.GameTime).ToList();
            return View(games);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var _teamLogic = new TeamLogic();
            ViewBag.Teams = _teamLogic.GetAll(new BL.Models.Core.Filter()).Output;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(GameCreateModel parameters)
        {
            var _teamLogic = new TeamLogic();
            ViewBag.Teams = _teamLogic.GetAll(new BL.Models.Core.Filter()).Output;
            var opResult = _gameLogic.Add(parameters);
            if (opResult.IsSuccess)
            {
                return RedirectToAction("index");
            }
            return View(parameters);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddResult(int id)
        {
            var opResult = _gameLogic.GetById(id);

            return View(new GameResultCreateModel { GameId = id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddResult(GameResultCreateModel parameters)
        {
            var opResult = _gameLogic.UpdateResult(parameters);

            return RedirectToAction("index");
        }
    }
}