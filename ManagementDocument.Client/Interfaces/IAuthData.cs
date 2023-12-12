using ManagementDocument.Database.Entities;
using Refit;

namespace ManagementDocument.Client.Interfaces
{
    public interface IAuthData
    {
        [Post("/Auth/Auth")]
        Task<string> Auth([Body] User user);

        [Get("/Auth/GetUserById/{id}")]
        Task<User> GetUserById(int id);
    }
}
