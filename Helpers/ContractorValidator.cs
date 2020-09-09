using ContractorAPI.Models;
using ContractorAPI.OptionSets;

namespace ContractorAPI.Helpers
{
    /// <summary>
    /// Валидация полей контрагента
    /// </summary>
    public class ContractorFieldsValidator
    {
        /// <summary>
        /// Проверка на валидность полей
        /// </summary>
        /// <param name="contractor">Контрагент</param>
        /// <returns>Валидны ли поля</returns>
        public static bool IsValid(Contractor contractor)
        {
            var isValid = true;

            if (contractor == null)
            {
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(contractor.Name) 
                || contractor.TypeOfContractor == ContractorTypes.Undefined 
                || string.IsNullOrWhiteSpace(contractor.INN))
            {
                isValid = false;
            }

            if (contractor.TypeOfContractor == ContractorTypes.Legal && string.IsNullOrWhiteSpace(contractor.KPP))
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
