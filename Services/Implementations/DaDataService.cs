using ContractorAPI.Models;
using ContractorAPI.OptionSets;
using ContractorAPI.Properties;
using ContractorAPI.Services.Interfaces;
using Dadata;
using Dadata.Model;
using System;
using System.Linq;

namespace ContractorAPI.Services.Implementations
{
    /// <summary>
    /// Сервис-обертка к https://dadata.ru/
    /// </summary>
    public class DaDataService : IDaDataService
    {
        /// <summary>
        /// API Клиент к dadata
        /// </summary>
        private readonly ISuggestClientSync _API;

        public DaDataService()
        {
            _API = new SuggestClientSync(Resources.DaDataToken);
        }

        /// <summary>
        /// Проверка существования контрагента в ЕГРЮЛ по ИНН/КПП
        /// </summary>
        /// <param name="contractor">Контрагент для проверки</param>
        /// <returns>Существует ли в ЕГРЮЛ</returns>
        public bool IsExistInEGRUL(Contractor contractor)
        {
            if (contractor == null)
            {
                throw new ArgumentNullException(nameof(contractor));
            }

            var isExistInEGRUL = false;

            // Параметры запроса к API - Проверка для ИП по ИНН, Проверка для Юр. лица по ИНН и КПП
            var findPartyRequest = contractor.TypeOfContractor == ContractorTypes.Individual ? new FindPartyRequest(contractor.INN)
                : contractor.TypeOfContractor == ContractorTypes.Legal ? new FindPartyRequest(contractor.INN, contractor.KPP)
                : null;

            // Ответ с dadata
            var suggestResponse = _API.FindParty(findPartyRequest);

            if (suggestResponse?.suggestions?.Count > 0)
            {
                isExistInEGRUL = true;
            }

            return isExistInEGRUL;
        }

        /// <summary>
        /// Получение контрагента с полным наименованием ОПФ
        /// </summary>
        /// <param name="contractor">Контрагент</param>
        /// <returns>Контрагент с полным наименованием ОПФ</returns>
        public Contractor GetFullContractorWithOPF(Contractor contractor)
        {
            if (contractor == null)
            {
                throw new ArgumentNullException(nameof(contractor));
            }

            var suggestResponse = _API.FindParty(contractor.INN);

            if (suggestResponse?.suggestions?.Count > 0)
            {
                var daDataParty = suggestResponse.suggestions.FirstOrDefault();
                if (daDataParty?.data?.name != null && !string.IsNullOrWhiteSpace(daDataParty.data.name.full_with_opf))
                {
                    contractor.Fullname = daDataParty.data.name.full_with_opf;
                }
            }

            return contractor;
        }
    }
}
