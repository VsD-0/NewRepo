using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Интерфейс для работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// Получает список всех пользователей - Dto.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        Task<List<UserDto>> GetUsersDto();

        /// <summary>
        /// Получает пользователя по его логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Пользователь с указанным логином.</returns>
        Task<User> GetUserByLogin(string login);

        /// <summary>
        /// Получает пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь с указанным идентификатором.</returns>
        Task<User> GetUserById(int id);

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        Task AddUser(User user);

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="user">Пользователь для удаления.</param>
        Task DeleteUser(User user);

        /// <summary>
        /// Обновляет информацию о пользователе.
        /// </summary>
        /// <param name="user">Пользователь для обновления.</param>
        Task UpdateUser(User user);

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным логином.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>True, если пользователь существует, иначе false.</returns>
        bool IsExistLogin(string login);

        /// <summary>
        /// Проверяет, является ли указанный пароль действительным для указанного пользователя.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>True, если пароль действителен, иначе false.</returns>
        bool IsValidPassword(string login, string password);
    }
}