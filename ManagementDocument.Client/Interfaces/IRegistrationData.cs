using ManagementDocument.Database.Entities;
using Refit;

namespace ManagementDocument.Client.Interfaces
{
    public interface IRegistrationData
    {
        [Post("/Registration/CreateUser")]
        Task<User> CreateUser([Body] User user);
    }
}
