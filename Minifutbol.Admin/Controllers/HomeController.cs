using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.ScoreBoard;
using Minifutbol.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minifutbol.User.Controllers
{
    public class HomeController : Controller
    {
        private PointLogic _pointLogic;
        private GameLogic _gameLogic;
        public HomeController()
        {
            _pointLogic = new PointLogic();
            _gameLogic = new GameLogic();
        }
        public ActionResult Index()
        {
            var at=_pointLogic.GetAll(new BL.Models.Core.Filter()).Output;
            var points = _pointLogic.GetAll(new BL.Models.Core.Filter()).Output.GroupBy(x => new
            {
                x.TeamId
            }).Select(s => new ScoreBoardViewModel
            {
                TeamName =s.FirstOrDefault(l=>l.TeamId==s.Key.TeamId).Team.Name,
                Win = s.Where(a => a.GamePiont == (int)GameResultEnum.Win).Count(),
                Lose = s.Where(a => a.GamePiont == (int)GameResultEnum.Lose).Count(),
                Draw = s.Where(a => a.GamePiont == (int)GameResultEnum.Draw).Count(),
                GamePiont = s.Sum(a => a.GamePiont)

            }).OrderByDescending(x=>x.GamePiont);

            return View(points.ToList());
        }

        public ActionResult Game()
        {
            var games = _gameLogic.GetAll(new BL.Models.Core.Filter()).Output.OrderByDescending(x => x.GameTime).ToList();
            return View(games);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}