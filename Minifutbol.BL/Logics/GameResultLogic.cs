using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.GameResult;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;
using Minifutbol.Utils.Enums;

namespace Minifutbol.BL.Logics
{
    public class GameResultLogic : BaseLogic<GameResultViewModel, GameResultCreateModel, GameResultFindModel, GameResultUpdateModel, GameResultDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public GameResultLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region GameResult Add

        protected override void AddAbstract(GameResultCreateModel entity)
        {
            var output = new LogicResult<GameResultViewModel>();
            var data = Mapper.Map<GameResultCreateModel, GameResult>(entity);
            this.Uow.GetRepository<GameResult>().Add(data);
            var points = new List<Point>{
                new Point
            {
                TeamId = data.Game.HostTeamId,
                GameResultId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                : (data.HostTeamGoals > data.GuestTeamGoals) ? (int)GameResultEnum.Win
                : (int)GameResultEnum.Lose
            },
            new Point
            {
                TeamId = data.Game.GuestTeamId,
                GameResultId = data.Id,
                GamePiont = data.HostTeamGoals == data.GuestTeamGoals ? (int)GameResultEnum.Draw
                           : (data.HostTeamGoals < data.GuestTeamGoals) ? (int)GameResultEnum.Win
                           : (int)GameResultEnum.Lose
            } };
            this.Uow.GetRepository<Point>().AddRange(points);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<GameResult, GameResultViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<GameResultCreateModel> entities)
        {
            var output = new LogicResult<ICollection<GameResultViewModel>>();
            var data = Mapper.Map<List<GameResultCreateModel>, List<GameResult>>(entities);
            var addedEntities = this.Uow.GetRepository<GameResult>().AddRange(data);
            output.Output = Mapper.Map<List<GameResult>, List<GameResultViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region GameResult Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<GameResultViewModel>>();
            var data = this.Uow.GetRepository<GameResult>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<GameResult>, ICollection<GameResultViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<GameResultViewModel>();
            var data = this.Uow.GetRepository<GameResult>().GetById(id);
            output.Output = Mapper.Map<GameResult, GameResultViewModel>(data);
            Result = output;
        }

        #endregion

        #region GameResult Find

        protected override void FindAbstract(GameResultFindModel parameters)
        {
            var output = new LogicResult<GameResultViewModel>();
            var predicate = PredicateBuilder.True<GameResult>();
            if (parameters == null)
            {
                parameters = new GameResultFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.HostTeamGoals != null)
            {
                predicate = predicate.And(r => r.HostTeamGoals == parameters.HostTeamGoals);
            }
            if (parameters.GuestTeamGoals != null)
            {
                predicate = predicate.And(r => r.GuestTeamGoals == parameters.GuestTeamGoals);
            }
            if (parameters.GameId != null)
            {
                predicate = predicate.And(r => r.GameId == parameters.GameId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }

            var data = this.Uow.GetRepository<GameResult>().Find(predicate);
            output.Output = Mapper.Map<GameResult, GameResultViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(GameResultFindModel parameters)
        {
            var output = new LogicResult<ICollection<GameResultViewModel>>();
            var predicate = PredicateBuilder.True<GameResult>();
            if (parameters == null)
            {
                parameters = new GameResultFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.HostTeamGoals != null)
            {
                predicate = predicate.And(r => r.HostTeamGoals == parameters.HostTeamGoals);
            }
            if (parameters.GuestTeamGoals != null)
            {
                predicate = predicate.And(r => r.GuestTeamGoals == parameters.GuestTeamGoals);
            }
            if (parameters.GameId != null)
            {
                predicate = predicate.And(r => r.GameId == parameters.GameId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<GameResult>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<GameResult>, ICollection<GameResultViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region GameResult Update

        protected override void UpdateAbstract(GameResultUpdateModel entity)
        {
            var output = new LogicResult<GameResultViewModel>();
            var curentGameResult = this.Uow.GetRepository<GameResult>().GetById(entity.Id);
            var gameResult = Mapper.Map(entity, curentGameResult);
            var data = this.Uow.GetRepository<GameResult>().Update(gameResult);
            output.Output = Mapper.Map<GameResult, GameResultViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(GameResultUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region GameResult Remove

        protected override void RemoveAbstract(GameResultDeleteModel entity)
        {
            var output = new LogicResult<GameResultViewModel>();
            var data = Uow.GetRepository<GameResult>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<GameResult>().Remove(data);
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
