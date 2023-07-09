using Framework.Common;
using Framework.Common.DTOs;
using Framework.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Security.Core.Interfaces;
using Services.Security.Infrastructure;
using Services.Security.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericUnitOfwork<EFDBContext> _uow;
        private readonly IGenericRepository<Users> _repoUsers;
        private readonly IGenericRepository<UserRoles> _repoUsersRoles;
        private readonly IGenericRepository<Roles> _repRoles;

        public UserService(IGenericUnitOfwork<EFDBContext> unit,
            IGenericRepository<Users> repoUsers,
            IGenericRepository<UserRoles> repoUsersRoles,
            IGenericRepository<Roles> repRoles)
        {
            _uow = unit;
            _repoUsers = repoUsers;
            _repoUsersRoles = repoUsersRoles;
            _repRoles = repRoles;
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
                    if (await _repRoles.Query(x => x.Id == roleID).AnyAsync())
                        _repoUsersRoles.Insert(new UserRoles() { Id = Guid.NewGuid().ToString(), UserId = user.Id, RoleId = roleID });
                }

                _uow.SaveChanges("adminUser");

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
