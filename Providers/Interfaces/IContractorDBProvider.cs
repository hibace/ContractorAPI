using ContractorAPI.Models;
using System.Collections.Generic;

namespace ContractorAPI.Providers.Interfaces
{
    /// <summary>
    /// Интерфейс LiteDb провайдера для сущности Контрагент
    /// </summary>
    public interface IContractorDBProvider
    {
        /// <summary>
        /// Проверка есть ли тестовые данные
        /// </summary>
        /// <returns>Есть ли тестовые данные</returns>
        bool HasExampleRows();

        /// <summary>
        /// Заполнение тестовыми контрагентами
        /// </summary>
        /// <param name="number">Кол-во добавляемых тестовых данных</param>
        void FillWithExampleRows(int number);

        /// <summary>
        /// Добавить контрагента
        /// </summary>
        /// <param name="contractor">Контрагент для добавления</param>
        void AddContractor(Contractor contractor);

        /// <summary>
        /// Получение всех контрагентов
        /// </summary>
        /// <returns>Перечисление контрагентов</returns>
        IEnumerable<Contractor> GetContractors();

        /// <summary>
        /// Удалить всех контрагентов
        /// </summary>
        void DeleteAll();
    }
}
