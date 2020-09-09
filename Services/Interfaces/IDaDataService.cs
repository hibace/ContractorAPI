using ContractorAPI.Models;

namespace ContractorAPI.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса-обертки над dadata.ru
    /// </summary>
    public interface IDaDataService
    {
        // <summary>
        /// Проверка существования контрагента в ЕГРЮЛ по ИНН/КПП
        /// </summary>
        /// <param name="contractor">Контрагент для проверки</param>
        /// <returns>Существует ли в ЕГРЮЛ</returns>
        bool IsExistInEGRUL(Contractor contractor);

        /// <summary>
        /// Получение контрагента с полным наименованием ОПФ
        /// </summary>
        /// <param name="contractor">Контрагент</param>
        /// <returns>Контрагент с полным наименованием ОПФ</returns>
        Contractor GetFullContractorWithOPF(Contractor contractor);
    }
}