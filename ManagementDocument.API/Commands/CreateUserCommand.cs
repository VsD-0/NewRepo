using ManagementDocument.Database.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ManagementDocument.API.Commands
{
    /// <summary>
    /// Команда добавления нового пользователя
    /// </summary>
    public class CreateUserCommand : IRequest<User>
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <example>ivan.ivanov@gmail.com</example>
        [FromBody]
        [BindRequired]
        public string? Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>Ivan123#</example>
        [FromBody]
        [BindRequired]
        public string? Password { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        /// <example>Ivan123#</example>
        [FromBody]
        [BindRequired]
        public int Role { get; set; } = 1;
    }
}
