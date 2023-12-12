namespace ManagementDocument.Domain.Models
{
    /// <summary>
    /// Пользователь Dto
    /// </summary>
    public class UserDto
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
        /// Роль пользователя.
        /// </summary>
        public int Role { get; set; }
    }
}
