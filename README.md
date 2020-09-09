# ContractorAPI
Small example API (.net core + swagger-ui)

1. Реализовать на asp.net core 3.0  REST api для управления контрагентами. (аутентификация, логирование и проч. инфраструктура НЕ НУЖНЫ !)

Методы:
  - получить список контрагентов
  - добавить контрагента

 Контрагент содержит следующие атрибуты
  id: int
  name: string
  fullname: string (не указывается, будет получен из dadata)
  type: enum (Юр.лицо, ИП),
  inn: string
  kpp: string

В качестве БД использовать LiteDB (https://www.litedb.org/). (standalone NoSql db )
БД предварительно заполнить несколькими демо-записями контрагентов (3-4)

Валидация и логика:

  - name, type, inn не могут быть пустыми, kpp не может быть пусто у Юр.лица
  - при создании контрагента  проверять его наличие в ЕГРЮЛ по полям inn kpp для Юр.лица и по inn для ИП, через сервис dadata.ru (https://dadata.ru/api/find-party/),
    если организация с указанными inn kpp или ИП с указанным inn не существует, выдавать ошибку
  - при создании из ответа dadata записывать full_with_opf — полное наименование с ОПФ в поле fullname

2. Добавить swagger-ui к  созданному api. Использовать Swashbuckle  https://github.com/domaindrivendev/Swashbuckle.AspNetCore  в дефолтной конфигурации