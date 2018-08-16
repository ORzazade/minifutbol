using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.TeamRequest;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;

namespace Minifutbol.BL.Logics
{
    public class TeamRequestLogic : BaseLogic<TeamRequestViewModel, TeamRequestCreateModel, TeamRequestFindModel, TeamRequestUpdateModel, TeamRequestDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TeamRequestLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region TeamRequest Add

        protected override void AddAbstract(TeamRequestCreateModel entity)
        {
            var output = new LogicResult<TeamRequestViewModel>();
            var data = Mapper.Map<TeamRequestCreateModel, TeamRequest>(entity);
            this.Uow.GetRepository<TeamRequest>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<TeamRequest, TeamRequestViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TeamRequestCreateModel> entities)
        {
            var output = new LogicResult<ICollection<TeamRequestViewModel>>();
            var data = Mapper.Map<List<TeamRequestCreateModel>, List<TeamRequest>>(entities);
            var addedEntities = this.Uow.GetRepository<TeamRequest>().AddRange(data);
            output.Output = Mapper.Map<List<TeamRequest>, List<TeamRequestViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region TeamRequest Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TeamRequestViewModel>>();
            var data = this.Uow.GetRepository<TeamRequest>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TeamRequest>, ICollection<TeamRequestViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TeamRequestViewModel>();
            var data = this.Uow.GetRepository<TeamRequest>().GetById(id);
            output.Output = Mapper.Map<TeamRequest, TeamRequestViewModel>(data);
            Result = output;
        }

        #endregion

        #region TeamRequest Find

        protected override void FindAbstract(TeamRequestFindModel parameters)
        {
            var output = new LogicResult<TeamRequestViewModel>();
            var predicate = PredicateBuilder.True<TeamRequest>();
            if (parameters == null)
            {
                parameters = new TeamRequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.TeamId != null)
            {
                predicate = predicate.And(r => r.TeamId == parameters.TeamId);
            }
           
            var data = this.Uow.GetRepository<TeamRequest>().Find(predicate);
            output.Output = Mapper.Map<TeamRequest, TeamRequestViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(TeamRequestFindModel parameters)
        {
            var output = new LogicResult<ICollection<TeamRequestViewModel>>();
            var predicate = PredicateBuilder.True<TeamRequest>();
            if (parameters == null)
            {
                parameters = new TeamRequestFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.TeamId != null)
            {
                predicate = predicate.And(r => r.TeamId == parameters.TeamId);
            }

            var data = this.Uow.GetRepository<TeamRequest>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<TeamRequest>, ICollection<TeamRequestViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region TeamRequest Update

        protected override void UpdateAbstract(TeamRequestUpdateModel entity)
        {
            var output = new LogicResult<TeamRequestViewModel>();
            var curentTeamRequest = this.Uow.GetRepository<TeamRequest>().GetById(entity.Id);
            var teamRequest = Mapper.Map(entity, curentTeamRequest);
            var data = this.Uow.GetRepository<TeamRequest>().Update(teamRequest);
            output.Output = Mapper.Map<TeamRequest, TeamRequestViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TeamRequestUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region TeamRequest Remove

        protected override void RemoveAbstract(TeamRequestDeleteModel entity)
        {
            var output = new LogicResult<TeamRequestViewModel>();
            var data = Uow.GetRepository<TeamRequest>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<TeamRequest>().Remove(data);
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
