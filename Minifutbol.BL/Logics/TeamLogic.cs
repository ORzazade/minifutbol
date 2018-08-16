using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Extensions;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.Team;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;

namespace Minifutbol.BL.Logics
{
    public class TeamLogic : BaseLogic<TeamViewModel, TeamCreateModel, TeamFindModel, TeamUpdateModel, TeamDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public TeamLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region Team Add

        protected override void AddAbstract(TeamCreateModel entity)
        {
            var output = new LogicResult<TeamViewModel>();
            var data = Mapper.Map<TeamCreateModel, Team>(entity);
            this.Uow.GetRepository<Team>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<Team, TeamViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<TeamCreateModel> entities)
        {
            var output = new LogicResult<ICollection<TeamViewModel>>();
            var data = Mapper.Map<List<TeamCreateModel>, List<Team>>(entities);
            var addedEntities = this.Uow.GetRepository<Team>().AddRange(data);
            output.Output = Mapper.Map<List<Team>, List<TeamViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region Team Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<TeamViewModel>>();
            var data = this.Uow.GetRepository<Team>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Team>, ICollection<TeamViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<TeamViewModel>();
            var data = this.Uow.GetRepository<Team>().GetById(id);
            output.Output = Mapper.Map<Team, TeamViewModel>(data);
            Result = output;
        }

        #endregion

        #region Team Find

        protected override void FindAbstract(TeamFindModel parameters)
        {
            var output = new LogicResult<TeamViewModel>();
            var predicate = PredicateBuilder.True<Team>();
            if (parameters == null)
            {
                parameters = new TeamFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }

            var data = this.Uow.GetRepository<Team>().Find(predicate);
            output.Output = Mapper.Map<Team, TeamViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(TeamFindModel parameters)
        {
            var output = new LogicResult<ICollection<TeamViewModel>>();
            var predicate = PredicateBuilder.True<Team>();
            if (parameters == null)
            {
                parameters = new TeamFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.Name != null)
            {
                predicate = predicate.And(r => r.Name.Contains(parameters.Name));
            }
            if (parameters.Description != null)
            {
                predicate = predicate.And(r => r.Description.Contains(parameters.Description));
            }
            var data = this.Uow.GetRepository<Team>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<Team>, ICollection<TeamViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region Team Update

        protected override void UpdateAbstract(TeamUpdateModel entity)
        {
            var output = new LogicResult<TeamViewModel>();
            var curentTeam = this.Uow.GetRepository<Team>().GetById(entity.Id);
            var team = Mapper.Map(entity, curentTeam);
            var data = this.Uow.GetRepository<Team>().Update(team);
            output.Output = Mapper.Map<Team, TeamViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(TeamUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Team Remove

        protected override void RemoveAbstract(TeamDeleteModel entity)
        {
            var output = new LogicResult<TeamViewModel>();
            var data = Uow.GetRepository<Team>().GetById(entity.Id);
            if (data != null)
            {

                while (data.Users.Count()>0)
                {
                    data.Users.FirstOrDefault().TeamId = null;
                    Uow.GetRepository<User>().Update(data.Users.FirstOrDefault());
                }
               
                Uow.GetRepository<Team>().Remove(data);
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

        public bool ExitTeam(int? id)
        {
            var UserId = id ?? HttpContext.Current.GetOwinContext().Authentication.User.GetUserId();
            var user = Uow.GetRepository<User>().GetById(UserId);
            if (user != null)
            {
                user.TeamId = null;
                this.Uow.GetRepository<User>().Update(user);
                return true;
            }
            return false;
        }
        public bool AddPlayer(int userId, int teamId)
        {
            var user = Uow.GetRepository<User>().GetById(userId);
            if (user != null)
            {
                user.TeamId = teamId;
                this.Uow.GetRepository<User>().Update(user);
                return true;
            }
            return false;
        }

        #endregion
    }
}
