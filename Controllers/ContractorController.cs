using System;
using System.Collections.Generic;
using ContractorAPI.Models;
using ContractorAPI.Properties;
using ContractorAPI.Providers.Implementations;
using ContractorAPI.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractorAPI.Controllers
{
    /// <summary>
    /// Контроллер сущности Контрагент
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ContractorController : ControllerBase
    {
        /// <summary>
        /// Провайдер данных LiteDb сущности Контрагент
        /// </summary>
        private readonly IContractorDBProvider _сontractorDBProvider;

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        public ContractorController()
        {
            _сontractorDBProvider = new ContractorDBProvider();
        }

        /// <summary>
        /// GET: api/<ContractorController>
        /// Получить список контрагентов, если нет данных - создать заполнить тестовыми и вернуть
        /// </summary>
        /// <returns>Список контрагентов</returns>
        [HttpGet]
        public IEnumerable<Contractor> Get()
        {
            try
            {
                var hasExampleRows = _сontractorDBProvider.HasExampleRows();
                if (!hasExampleRows)
                {
                    var numberOfExampleRowsToAdd = int.Parse(Resources.NumberOfExampleRowsToAdd);

                    _сontractorDBProvider.FillWithExampleRows(numberOfExampleRowsToAdd);
                }

                var result = _сontractorDBProvider.GetContractors();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// POST api/<ContractorController>
        /// Добавить контрагета
        /// </summary>
        /// <param name="contractor">Контрагент для добавления</param>
        [HttpPost]
        public void Post([FromBody] Contractor contractor)
        {
            if (contractor == null)
            {
                throw new ArgumentNullException(nameof(contractor));
            }

            try
            {
                _сontractorDBProvider.AddContractor(contractor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
