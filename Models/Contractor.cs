using ContractorAPI.OptionSets;
using System;

namespace ContractorAPI.Models
{
    /// <summary>
    /// Модель сущности Контрагент
    /// </summary>
    public class Contractor
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Тип (Юр.лицо, ИП)
        /// </summary>
        public ContractorTypes TypeOfContractor { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string INN { get; set; }

        /// <summary>
        /// KPP
        /// </summary>
        public string KPP { get; set; }
    }
}
