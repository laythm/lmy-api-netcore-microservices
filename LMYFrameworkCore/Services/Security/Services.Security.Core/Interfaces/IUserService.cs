using System.Threading.Tasks;
using Framework.Common.DTOs;
using Services.Security.Infrastructure.Entities;

namespace Services.Security.Core.Interfaces
{
    public interface IUserService
    {
        Task<DTOBase> AddUser(UserDTO userDTO);
    }
}