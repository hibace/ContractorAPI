using ContractorAPI.Models;
using ContractorAPI.OptionSets;
using ContractorAPI.Properties;
using ContractorAPI.Providers.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using ContractorAPI.Helpers;
using ContractorAPI.Services.Interfaces;
using ContractorAPI.Services.Implementations;

namespace ContractorAPI.Providers.Implementations
{
    /// <summary>
    /// Сервис-обертка для манипуляций данными в LiteDB
    /// </summary>
    public class ContractorDBProvider : IContractorDBProvider
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Сервис dadata.ru
        /// </summary>
        private readonly IDaDataService _daDataService;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ContractorDBProvider()
        {
            // Строка подключения к БД
            _connectionString = Resources.ContractorDB;

            // Сервис dadata.ru
            _daDataService = new DaDataService();
        }

        /// <summary>
        /// Проверка есть ли тестовые данные
        /// </summary>
        /// <returns>Есть ли тестовые данные</returns>
        public bool HasExampleRows()
        {
            var hasExampleRows = false;

            using (var db = new LiteDatabase(_connectionString))
            {
                var contractorCollectionCount = db.GetCollection<Contractor>(Resources.Contractors).Count();

                hasExampleRows = contractorCollectionCount > 0;
            }

            return hasExampleRows;
        }

        /// <summary>
        /// Заполнение тестовыми контрагентами
        /// </summary>
        /// <param name="number">Кол-во добавляемых тестовых данных</param>
        public void FillWithExampleRows(int number)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var contractorCollection = db.GetCollection<Contractor>(Resources.Contractors);

                for (var i = 0; i < number; i++)
                {
                    var contractorToAdd = new Contractor
                    {
                        Id = new Guid(),
                        Name = "Test",
                        Fullname = string.Empty,
                        TypeOfContractor = ContractorTypes.Legal,
                        INN = "7707083893",
                        KPP = "540602001"
                    };

                    // Получение контрагента с полным наименование ОПФ
                    var fullContractorWithOPF = _daDataService.GetFullContractorWithOPF(contractorToAdd);

                    contractorCollection.Insert(fullContractorWithOPF);
                }
            }
        }

        /// <summary>
        /// Добавить контрагента
        /// </summary>
        /// <param name="contractor">Контрагент для добавления</param>
        public void AddContractor(Contractor contractor)
        {
            if (contractor == null)
            {
                throw new ArgumentNullException(nameof(contractor));
            }

            // Проверка валидности полей контрагента
            var isValid = ContractorFieldsValidator.IsValid(contractor);
            if (!isValid)
            {
                throw new Exception(Resources.NotValidContractorMessage);
            }

            // Проверка существованияв ЕГРЮЛ
            var isExistInEGRUL = _daDataService.IsExistInEGRUL(contractor);
            if (!isExistInEGRUL)
            {
                throw new Exception(Resources.NotExistsInEGRUL);
            }

            // Получение контрагента с полным наименование ОПФ
            var fullContractorWithOPF = _daDataService.GetFullContractorWithOPF(contractor);

            using (var db = new LiteDatabase(_connectionString))
            {
                var contractorCollection = db.GetCollection<Contractor>(Resources.Contractors);

                contractorCollection.Insert(fullContractorWithOPF);
            }
        }

        /// <summary>
        /// Получение всех контрагентов
        /// </summary>
        /// <returns>Перечисление контрагентов</returns>
        public IEnumerable<Contractor> GetContractors()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var contractorCollection = db.GetCollection<Contractor>(Resources.Contractors);
                var result = contractorCollection.FindAll().ToList();

                return result;
            }
        }

        /// <summary>
        /// Удалить всех контрагентов
        /// </summary>
        public void DeleteAll()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var contractorCollection = db.GetCollection<Contractor>(Resources.Contractors);
                contractorCollection.DeleteAll();            
            }
        }
    }
}
