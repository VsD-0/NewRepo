using System.Text.Json.Serialization;

namespace ManagementDocument.Database.Entities
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string? Login { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string? Password { get; set; } = null!;

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Навигационное свойство для доступа к роли пользователя.
        /// </summary>
        [JsonIgnore]
        public virtual UserRole RoleNavigation { get; set; } = null!;
    }
}
