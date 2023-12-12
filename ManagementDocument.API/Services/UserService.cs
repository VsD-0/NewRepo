using ManagementDocument.Database.Context;
using ManagementDocument.Database.Entities;
using ManagementDocument.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementDocument.API.Services
{
    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields
        private readonly ManagementDocumentDbContext _context;
        #endregion Fields

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserService"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public UserService(ManagementDocumentDbContext context) => _context = context;

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public async Task<List<User>> GetUsers() => await _context.Users.ToListAsync();

        /// <summary>
        /// Получает список всех пользователей - Dto.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public async Task<List<UserDto>> GetUsersDto()
        {
            List<UserDto> usersDto = await _context.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Role = user.Role
                })
                .ToListAsync();

            return usersDto;
        }

        /// <summary>
        /// Получает пользователя по его логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Пользователь с указанным логином.</returns>
        public async Task<User> GetUserByLogin(string login) => await _context.Users.FirstOrDefaultAsync(u => u.Login == login) ?? throw new ArgumentNullException(nameof(login), "Пользователь не найден");

        /// <summary>
        /// Получает пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь с указанным идентификатором.</returns>
        public async Task<User> GetUserById(int id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ArgumentNullException(nameof(id), "Пользователь не найден");

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь для добавления.</param>
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет информацию о пользователе в базе данных.
        /// </summary>
        /// <param name="user">Пользователь для обновления.</param>

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет информацию о пользователе в базе данных.
        /// </summary>
        /// <param name="user">Документ для обновления.</param>

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным логином.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>True, если пользователь существует, иначе false.</returns>
        public bool IsExistLogin(string login) => _context.Users.Any(u => u.Login == login);

        /// <summary>
        /// Проверяет, является ли указанный пароль действительным для указанного пользователя.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>True, если пароль действителен, иначе false.</returns>
        public bool IsValidPassword(string login, string password) => _context.Users.Any(u => u.Login == login && u.Password == password);
    }
}
