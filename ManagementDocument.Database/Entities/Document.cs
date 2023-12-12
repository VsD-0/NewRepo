using System.Text.Json.Serialization;

namespace ManagementDocument.Database.Entities
{
    /// <summary>
    /// Документ.
    /// </summary>
    public partial class Document
    {
        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип документа.
        /// </summary>
        public int Doctype { get; set; }

        /// <summary>
        /// Номер документа.
        /// </summary>
        public string? Num { get; set; } = null!;

        /// <summary>
        /// Дата получения документа.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        public string? Codeorg { get; set; } = null!;

        /// <summary>
        /// Организация.
        /// </summary>
        public string? Org { get; set; } = null!;

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateOnly Birthdate { get; set; }

        /// <summary>
        /// Навигационное свойство для доступа к типу документа.
        /// </summary>
        [JsonIgnore]
        public virtual DocumentType DoctypeNavigation { get; set; } = null!;
    }
}
