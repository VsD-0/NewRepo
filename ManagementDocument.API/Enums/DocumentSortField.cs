namespace ManagementDocument.API.Enums
{
    /// <summary>
    /// Свойства документа
    /// </summary>
    public enum DocumentSortField
    {
        /// <summary>
        /// Id
        /// </summary>
        Id,
        /// <summary>
        /// Тип
        /// </summary>
        Doctype,
        /// <summary>
        /// Номер
        /// </summary>
        Num,
        /// <summary>
        /// Дата получения
        /// </summary>
        Date,
        /// <summary>
        /// Код организации
        /// </summary>
        Codeorg,
        /// <summary>
        /// Организация
        /// </summary>
        Org,
        /// <summary>
        /// Дата рождения
        /// </summary>
        /// 
        Birthdate,

        /// <summary>
        /// Id (По убыванию)
        /// </summary>
        _Id,
        /// <summary>
        /// Тип (По убыванию)
        /// </summary>
        _Doctype,
        /// <summary>
        /// Номер (По убыванию)
        /// </summary>
        _Num,
        /// <summary>
        /// Дата получения (По убыванию)
        /// </summary>
        _Date,
        /// <summary>
        /// Код организации (По убыванию)
        /// </summary>
        _Codeorg,
        /// <summary>
        /// Организация (По убыванию)
        /// </summary>
        _Org,
        /// <summary>
        /// Дата рождения (По убыванию)
        /// </summary>
        _Birthdate,
        /// <summary>
        /// Не существующее поле (для проверки)
        /// </summary>
        NoExistParam
    }
}
