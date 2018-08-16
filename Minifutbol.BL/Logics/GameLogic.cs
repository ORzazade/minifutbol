using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.Game;
using Minifutbol.BL.Models.GameResult;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;
using Minifutbol.Utils.Enums;

namespace Minifutbol.BL.Logics
{
    public class GameLogic : BaseLogic<GameViewModel, GameCreateModel, GameFindModel, GameUpdateModel, GameDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public GameLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region Game Add

        protected override void AddAbstract(GameCreateModel entity)
        {
            var output = new LogicResult<GameViewModel>();
            var data = Mapper.Map<GameCreateModel, Game>(entity);
            this.Uow.GetRepository<Game>().Add(data);
            this.Uow.SaveChanges();
            if (data.GuestTeamGoals != null && data.GuestTeamGoals != null)
            {
                var points = new List<Point>{
                new Point
            {
                TeamId = data.HostTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                : (data.HostTeamGoals > data.GuestTeamGoals) ? (int)GameResultEnum.Win
                : (int)GameResultEnum.Lose
            },
            new Point
            {
                TeamId = data.GuestTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                           : (data.HostTeamGoals < data.GuestTeamGoals) ? (int)GameResultEnum.Win
                           : (int)GameResultEnum.Lose
            }};
                this.Uow.GetRepository<Point>().AddRange(points);
            }
            output.Output = Mapper.Map<Game, GameViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<GameCreateModel> entities)
        {
            var output = new LogicResult<ICollection<GameViewModel>>();
            var data = Mapper.Map<List<GameCreateModel>, List<Game>>(entities);
            var addedEntities = this.Uow.GetRepository<Game>().AddRange(data);
            output.Output = Mapper.Map<List<Game>, List<GameViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Game Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<GameViewModel>>();
            var data = this.Uow.GetRepository<Game>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Game>, ICollection<GameViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<GameViewModel>();
            var data = this.Uow.GetRepository<Game>().GetById(id);
            output.Output = Mapper.Map<Game, GameViewModel>(data);
            Result = output;
        }

        #endregion

        #region Game Find

        protected override void FindAbstract(GameFindModel parameters)
        {
            var output = new LogicResult<GameViewModel>();
            var predicate = PredicateBuilder.True<Game>();
            if (parameters == null)
            {
                parameters = new GameFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.HostTeamId != null)
            {
                predicate = predicate.And(r => r.HostTeamId == parameters.HostTeamId);
            }
            if (parameters.GuestTeamId != null)
            {
                predicate = predicate.And(r => r.GuestTeamId == parameters.GuestTeamId);
            }
            if (parameters.HostTeamId != null)
            {
                predicate = predicate.And(r => r.HostTeamId == parameters.HostTeamId);
            }
            if (parameters.RefereeName != null)
            {
                predicate = predicate.And(r => r.RefereeName.Contains(parameters.RefereeName));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }

            var data = this.Uow.GetRepository<Game>().Find(predicate);
            output.Output = Mapper.Map<Game, GameViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(GameFindModel parameters)
        {
            var output = new LogicResult<ICollection<GameViewModel>>();
            var predicate = PredicateBuilder.True<Game>();
            if (parameters == null)
            {
                parameters = new GameFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.HostTeamId != null)
            {
                predicate = predicate.And(r => r.HostTeamId == parameters.HostTeamId);
            }
            if (parameters.GuestTeamId != null)
            {
                predicate = predicate.And(r => r.GuestTeamId == parameters.GuestTeamId);
            }
            if (parameters.HostTeamId != null)
            {
                predicate = predicate.And(r => r.HostTeamId == parameters.HostTeamId);
            }
            if (parameters.RefereeName != null)
            {
                predicate = predicate.And(r => r.RefereeName.Contains(parameters.RefereeName));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<Game>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Game>, ICollection<GameViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Game Update

        protected override void UpdateAbstract(GameUpdateModel entity)
        {
            var output = new LogicResult<GameViewModel>();
            var curentGame = this.Uow.GetRepository<Game>().GetById(entity.Id);
            var game = Mapper.Map(entity, curentGame);
            var data = this.Uow.GetRepository<Game>().Update(game);
            var points = this.Uow.GetRepository<Point>().FindAll(a => a.GameId == data.Id).ToList();
            foreach (var item in points)
            {
                Uow.GetRepository<Point>().Remove(item);
            }
            if (data.GuestTeamGoals != null && data.GuestTeamGoals != null)
            {
                var newPoints = new List<Point>{
                new Point
            {
                TeamId = data.HostTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                : (data.HostTeamGoals > data.GuestTeamGoals) ? (int)GameResultEnum.Win
                : (int)GameResultEnum.Lose
            },
            new Point
            {
                TeamId = data.GuestTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                           : (data.HostTeamGoals < data.GuestTeamGoals) ? (int)GameResultEnum.Win
                           : (int)GameResultEnum.Lose
            }};
                this.Uow.GetRepository<Point>().AddRange(points);
            }
            output.Output = Mapper.Map<Game, GameViewModel>(data);
            Result = output;
        }

        public bool UpdateResult(GameResultCreateModel entity)
        {
            var curentGame = this.Uow.GetRepository<Game>().GetById(entity.GameId);
            curentGame.GuestTeamGoals = entity.GuestTeamGoals;
            curentGame.HostTeamGoals = entity.HostTeamGoals;
            var data = this.Uow.GetRepository<Game>().Update(curentGame);
            var points = this.Uow.GetRepository<Point>().FindAll(a => a.GameId == data.Id).ToList();
            foreach (var item in points)
            {
                Uow.GetRepository<Point>().Remove(item);
            }
            if (data.GuestTeamGoals != null && data.GuestTeamGoals != null)
            {
                var newPoints = new List<Point>{
                new Point
            {
                TeamId = data.HostTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                : (data.HostTeamGoals > data.GuestTeamGoals) ? (int)GameResultEnum.Win
                : (int)GameResultEnum.Lose
            },
            new Point
            {
                TeamId = data.GuestTeamId,
                GameId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                           : (data.HostTeamGoals < data.GuestTeamGoals) ? (int)GameResultEnum.Win
                           : (int)GameResultEnum.Lose
            }};
                this.Uow.GetRepository<Point>().AddRange(newPoints);
            }
            return true;
        }
        protected override void UpdateRangeAbstract(GameUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Game Remove

        protected override void RemoveAbstract(GameDeleteModel entity)
        {
            var output = new LogicResult<GameViewModel>();
            var data = Uow.GetRepository<Game>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Game>().Remove(data);
            }
            else
            {
                output.ErrorList.Add(new Error
                {
                    Code = "400",
                    Text = "id is incorrect",
                    Type = Utils.Enums.OperationResultCode.Information
                });
            }
            Result = output;
        }



        #endregion
    }
}
