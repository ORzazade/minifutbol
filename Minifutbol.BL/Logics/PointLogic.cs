using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.Point;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;

namespace Minifutbol.BL.Logics
{
    public class PointLogic : BaseLogic<PointViewModel, PointCreateModel, PointFindModel, PointUpdateModel, PointDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public PointLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region Point Add

        protected override void AddAbstract(PointCreateModel entity)
        {
            var output = new LogicResult<PointViewModel>();
            var data = Mapper.Map<PointCreateModel, Point>(entity);
            this.Uow.GetRepository<Point>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Point, PointViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<PointCreateModel> entities)
        {
            var output = new LogicResult<ICollection<PointViewModel>>();
            var data = Mapper.Map<List<PointCreateModel>, List<Point>>(entities);
            var addedEntities = this.Uow.GetRepository<Point>().AddRange(data);
            output.Output = Mapper.Map<List<Point>, List<PointViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Point Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<PointViewModel>>();
            var data = this.Uow.GetRepository<Point>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Point>, ICollection<PointViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<PointViewModel>();
            var data = this.Uow.GetRepository<Point>().GetById(id);
            output.Output = Mapper.Map<Point, PointViewModel>(data);
            Result = output;
        }

        #endregion

        #region Point Find

        protected override void FindAbstract(PointFindModel parameters)
        {
            var output = new LogicResult<PointViewModel>();
            var predicate = PredicateBuilder.True<Point>();
            if (parameters == null)
            {
                parameters = new PointFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.TeamId != null)
            {
                predicate = predicate.And(r => r.TeamId == parameters.TeamId);
            }
            if (parameters.GamePiont != null)
            {
                predicate = predicate.And(r => r.GamePiont == parameters.GamePiont);
            }
            if (parameters.GameId != null)
            {
                predicate = predicate.And(r => r.GameId == parameters.GameId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }

            var data = this.Uow.GetRepository<Point>().Find(predicate);
            output.Output = Mapper.Map<Point, PointViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(PointFindModel parameters)
        {
            var output = new LogicResult<ICollection<PointViewModel>>();
            var predicate = PredicateBuilder.True<Point>();
            if (parameters == null)
            {
                parameters = new PointFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.TeamId != null)
            {
                predicate = predicate.And(r => r.TeamId == parameters.TeamId);
            }
            if (parameters.GamePiont != null)
            {
                predicate = predicate.And(r => r.GamePiont == parameters.GamePiont);
            }
            if (parameters.GameId != null)
            {
                predicate = predicate.And(r => r.GameId == parameters.GameId);
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<Point>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Point>, ICollection<PointViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Point Update

        protected override void UpdateAbstract(PointUpdateModel entity)
        {
            var output = new LogicResult<PointViewModel>();
            var curentPoint = this.Uow.GetRepository<Point>().GetById(entity.Id);
            var point = Mapper.Map(entity, curentPoint);
            var data = this.Uow.GetRepository<Point>().Update(point);
            output.Output = Mapper.Map<Point, PointViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(PointUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Point Remove

        protected override void RemoveAbstract(PointDeleteModel entity)
        {
            var output = new LogicResult<PointViewModel>();
            var data = Uow.GetRepository<Point>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<Point>().Remove(data);
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
