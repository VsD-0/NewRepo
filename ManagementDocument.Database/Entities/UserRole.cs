using System.Text.Json.Serialization;

namespace ManagementDocument.Database.Entities
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public partial class UserRole
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        public string Role { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство для доступа к пользователям с данной ролью.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
