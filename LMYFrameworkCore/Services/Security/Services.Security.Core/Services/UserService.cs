using Framework.Common;
using Framework.Common.DTOs;
using Framework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Security.Common.Interfaces;
using Services.Security.Core.Interfaces;
using Services.Security.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Users> _repoUsers;
        private readonly IGenericRepository<UserRoles> _repoUsersRoles;

        public UserService(IUnitOfWork unit,
            IGenericRepository<Users> repoUsers,
            IGenericRepository<UserRoles> repoUsersRoles)
        {
            _uow = unit;
            _repoUsers = repoUsers;
            _repoUsersRoles = repoUsersRoles;
        }

        public async Task<DTOBase> AddUser(UserDTO userDTO)
        {
            DTOBase dtoBase = new DTOBase();

            //if (validate(userModel, model))
            // {
            //using (var transaction = dbContext.Database.BeginTransaction())
            //  {
            try
            {
                var user = new Users();
                user.Id = Guid.NewGuid().ToString();
                user.CopyPropertyValues(userDTO, nameof(user.Id));
                user.PasswordHash = Hasher.HashString(userDTO.Password);

                _repoUsers.Insert(user);

                foreach (var roleID in userDTO.UserRoleIDs)
                {
                    _repoUsersRoles.Insert(new UserRoles() { Id = Guid.NewGuid().ToString(), UserId = user.Id, RoleId = roleID });
                }

                _uow.SaveChanges();

                dtoBase.AddSuccess("done");
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                dtoBase.AddError(ex.Message);
            }
            // }

            return null;
        }
    }
}
