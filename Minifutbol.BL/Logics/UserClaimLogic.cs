using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Minifutbol.BL.Base;
using Minifutbol.BL.Mapping;
using Minifutbol.BL.Models.Core;
using Minifutbol.BL.Models.UserClaim;
using Minifutbol.DAL.Context;
using Minifutbol.Utils;

namespace Minifutbol.BL.Logics
{
    public class UserClaimLogic : BaseLogic<UserClaimViewModel, UserClaimCreateModel, UserClaimFindModel, UserClaimUpdateModel, UserClaimDeleteModel>
    {
        #region Properties

        public MinifutbolContext MinifutbolContext;
        private IMapper _mapper;
        public IMapper Mapper => _mapper ?? (_mapper = AutoMapperConfig.CreateMapper());

        #endregion

        #region Constructor

        public UserClaimLogic()
        {
            MinifutbolContext = new MinifutbolContext();
        }

        #endregion


        #region UserClaim Add

        protected override void AddAbstract(UserClaimCreateModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = Mapper.Map<UserClaimCreateModel, UserClaim>(entity);
            this.Uow.GetRepository<UserClaim>().Add(data);
            this.Uow.SaveChanges();
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void AddRangeAbstract(List<UserClaimCreateModel> entities)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var data = Mapper.Map<List<UserClaimCreateModel>, List<UserClaim>>(entities);
            var addedEntities = this.Uow.GetRepository<UserClaim>().AddRange(data);
            output.Output = Mapper.Map<List<UserClaim>, List<UserClaimViewModel>>(addedEntities);
            ResultAll = output;
        }

        #endregion

        #region UserClaim Get

        protected override void GetAllAbstract(Filter filter)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var data = this.Uow.GetRepository<UserClaim>().GetAll();
            if (filter.PageSize > 0 && filter.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserClaim>, ICollection<UserClaimViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        protected override void GetByIdAbstract(int id)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = this.Uow.GetRepository<UserClaim>().GetById(id);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        #endregion

        #region UserClaim Find

        protected override void FindAbstract(UserClaimFindModel parameters)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var predicate = PredicateBuilder.True<UserClaim>();
            if (parameters == null)
            {
                parameters = new UserClaimFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ClaimType != null)
            {
                predicate = predicate.And(r => r.ClaimType.Contains(parameters.ClaimType));
            }
            if (parameters.ClaimValue != null)
            {
                predicate = predicate.And(r => r.ClaimValue.Contains(parameters.ClaimValue));
            }

            var data = this.Uow.GetRepository<UserClaim>().Find(predicate);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void FindAllAbstract(UserClaimFindModel parameters)
        {
            var output = new LogicResult<ICollection<UserClaimViewModel>>();
            var predicate = PredicateBuilder.True<UserClaim>();
            if (parameters == null)
            {
                parameters = new UserClaimFindModel();
            }
            if (parameters.Id != null)
            {
                predicate = predicate.And(r => r.Id == parameters.Id);
            }
            if (parameters.UserId != null)
            {
                predicate = predicate.And(r => r.UserId == parameters.UserId);
            }
            if (parameters.ClaimType != null)
            {
                predicate = predicate.And(r => r.ClaimType.Contains(parameters.ClaimType));
            }
            if (parameters.ClaimValue != null)
            {
                predicate = predicate.And(r => r.ClaimValue.Contains(parameters.ClaimValue));
            }
            var data = this.Uow.GetRepository<UserClaim>().FindAll(predicate);
            if (parameters.PageSize > 0 && parameters.PageNumber > 0)
            {
                data = data.OrderBy(x => x.Id).Skip((int)((parameters.PageNumber - 1) * parameters.PageSize)).Take((int)parameters.PageSize);
            }
            output.Output = Mapper.Map<ICollection<UserClaim>, ICollection<UserClaimViewModel>>(data.ToList()).OrderByDescending(x => x.Id).ToList();
            ResultAll = output;
            this.Uow.Dispose();

        }

        #endregion

        #region UserClaim Update

        protected override void UpdateAbstract(UserClaimUpdateModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var curentUserClaim = this.Uow.GetRepository<UserClaim>().GetById(entity.Id);
            var userClaim = Mapper.Map(entity, curentUserClaim);
            var data = this.Uow.GetRepository<UserClaim>().Update(userClaim);
            output.Output = Mapper.Map<UserClaim, UserClaimViewModel>(data);
            Result = output;
        }

        protected override void UpdateRangeAbstract(UserClaimUpdateModel parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region UserClaim Remove

        protected override void RemoveAbstract(UserClaimDeleteModel entity)
        {
            var output = new LogicResult<UserClaimViewModel>();
            var data = Uow.GetRepository<UserClaim>().GetById(entity.Id);
            if (data != null)
            {
                Uow.GetRepository<UserClaim>().Remove(data);
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
