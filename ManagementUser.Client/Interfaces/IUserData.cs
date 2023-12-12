using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using Refit;

namespace ManagementUser.Client.Interfaces
{
    public interface IUserData
    {
        [Get("/Users/GetUsers")]
        Task<PaginatedResult<UserDto>> GetUsers(int role, int pageNumber, int pageSize);

        [Post("/Users/CreateUser")]
        Task<UserDto> CreateUser([Body] User user);


        [Put("/Users/UpdateUser")]
        Task UpdateUser([Body] UserDto user);


        [Delete("/Users/DeleteUser/{id}")]
        Task DeleteUser(int id);
    }
}
