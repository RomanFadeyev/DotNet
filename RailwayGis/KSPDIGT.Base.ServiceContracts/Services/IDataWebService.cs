using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.Wsdl;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    /// <summary>
    /// Web-сервис, предоставляющий доступ к данным КСПД ИЖТ
    /// </summary>
    [ServiceContract]
    [WsdlDocumentation("Web-сервис доступа к данным КСПД ИЖТ")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IDataWebService : IWcfService
    {
        /// <summary>
        /// Получить список выполненных расчетов
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Получить список выполненных расчетов\r\n" +
                           "@param Begin Дата начала периода (необязательный)\r\n" +
                           "@param Begin Дата конца периода (необязательный)\r\n" +
                           "@param Begin Тип расчета (необязательный)\r\n" +
                           "@return Список расчетов")]
        CalcData[] GetCalcList(DateTime? Begin, DateTime? End, short? Type);

        /// <summary>
        /// Выгрузить XML с расчетом
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Выгрузить XML с расчетом\r\n" +
                           "@return ZIP-архив, в котором помещен XML и сопутствующие ему файлы документов")]
        byte[] GetCalc(Guid CalcGid);

        /// <summary>
        /// Получить список объектов
        /// Выполняет поиск и возврат сведений о расчетах согласно указанным фильтрам
        /// svn://redmine/kspdigt.net.2/trunk/Технические требования/Технические проекты/Web-сервисы/Web-сервис доступа к данным.doc
        /// 5.2.1. Метод «Получить список объектов»
        /// </summary>
        /// <param name="ObjType">Тип искомого объекта</param>
        /// <param name="AsupPutglId">Идентификатор АСУ-П главного пути</param>
        /// <param name="StartKm">Пикетаж начала, км</param>
        /// <param name="StartM">Пикетаж начала, м</param>
        /// <param name="EndKm">Пикетаж конца, км</param>
        /// <param name="EndM">Пикетаж конца, м</param>
        /// <returns>Список искомых объектов</returns>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Получить список объектов")]
        ObjectTypeResult[] GetWayObjects(ObjectTypeEnum ObjType, long AsupPutglId, int? StartKm, double? StartM, int? EndKm, double? EndM);

        /// <summary>
        /// Получить список документов расчета
        /// </summary>
        /// <param name="CalcGid">GID расчета</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Получить список документов расчета")]
        DocumentData[] GetCalcDocs(Guid CalcGid);

        /// <summary>
        /// Получить содержимое документа
        /// </summary>
        /// <param name="DocGid">GID документа</param>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Получить содержимое документа")]
        DocumentContent GetDocContent(Guid DocGid);

        /// <summary>
        /// Проверить учетную запись пользователя
        /// </summary>
        /// <param name="a_Login">Логин</param>
        /// <param name="a_Password">Пароль</param>
        /// <returns>Признак «Авторизован/не авторизован»</returns>
        [OperationContract]
        [WebInvoke(Method = "GET")]
        [WsdlDocumentation("Проверить учетную запись пользователя")]
        bool CheckUserAuthorization(string a_Login, string a_Password);
    }

    public class ObjectTypeInfoAttribute : Attribute
    {
        internal ObjectTypeInfoAttribute(string layer, string cls)
        {
            this.Layer = layer;
            this.Cls = cls;
        }
        public string Layer { get; private set; }
        public string Cls { get; private set; }
    }

    /// <summary>
    /// Тип искомого объекта
    /// Для вх.параметра метода GetWayObjects
    /// </summary>
    [DataContract(Name = "ObjectType")]
    public enum ObjectTypeEnum
    {
        /// <summary>
        /// Километровый столб. pPk_p 0102000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.PicketColumnPoint, "0102000")]
        Km = 1, 
        /// <summary>
        /// Стрелочный перевод. wSwtch_p 0530000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.TrackSwitchPoint, "0530000")]
        Switch = 2,
        /// <summary>
        /// Железнодорожный переезд. wCross_a 0404000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.RailroadAndPedestrianCrossingArea, "0404000")]
        Crossing = 3,
        /// <summary>
        /// Туннель. wConst_a 0905000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.ISSOArea, "0905000")]
        Tunnel = 4,
        /// <summary>
        /// Мост. wConst_a 0901000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.ISSOArea, "0901000")]
        Bridge = 5,
        /// <summary>
        /// Опора контактной сети. wTower_p 1001000
        /// </summary>
        [EnumMember]
        [ObjectTypeInfo(TableName.ContactNetworkSupportPoint, "1001000")]
        Tower = 6,
    }

    public class TableName
    {
        public const string RailroadAndPedestrianCrossingArea = "pCross_a";
        public const string PicketColumnPoint = "pPk_p";
        public const string TrackSwitchPoint = "pSwtch_p";
        public const string ISSOArea = "pConst_a";
        public const string ContactNetworkSupportPoint = "pTower_p";
    }

    /// <summary>
    /// Элемент/объект результирующего списка метода GetWayObjects
    /// </summary>
    [DataContract]
    public class ObjectTypeResult
    {
        /// <summary>
        /// ID объекта
        /// </summary>
        [DataMember]
        public long ObjectId { get; set; }
        /// <summary>
        /// GUID фрагмента карты
        /// </summary>
        [DataMember]
        public Guid MapFragmentGid { get; set; }
        /// <summary>
        /// GUID системы координат
        /// </summary>
        [DataMember]
        public Guid CoordSysGid { get; set; }
        /// <summary>
        /// Тип по классификатору
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        [DataMember]
        public string Num { get; set; }
        /// <summary>
        /// Длина
        /// </summary>
        [DataMember]
        public double Length { get; set; }
        /// <summary>
        /// Пикетаж начала, км
        /// </summary>
        [DataMember]
        public int StartKm { get; set; }
        /// <summary>
        /// Пикетаж начала, м
        /// </summary>
        [DataMember]
        public double StartM { get; set; }
        /// <summary>
        /// Пикетаж конца, км
        /// </summary>
        [DataMember]
        public int EndKm { get; set; }
        /// <summary>
        /// Пикетаж конца, м
        /// </summary>
        [DataMember]
        public double EndM { get; set; }
        /// <summary>
        /// Начало объекта, снесенное на ось пути, координата X
        /// </summary>
        [DataMember]
        public double StartX { get; set; }
        /// <summary>
        /// Начало объекта, снесенное на ось пути, координата Y
        /// </summary>
        [DataMember]
        public double StartY { get; set; }
        /// <summary>
        /// Конец объекта, снесенный на ось пути, координата X
        /// </summary>
        [DataMember]
        public double EndX { get; set; }
        /// <summary>
        /// Конец объекта, снесенный на ось пути, координата Y
        /// </summary>
        [DataMember]
        public double EndY { get; set; }
    }

    /// <summary>
    /// Выполненный расчет
    /// </summary>
    [DataContract]
    public class CalcData
    {
        /// <summary>
        /// Идентификатор расчета
        /// </summary>
        [DataMember]
        public Guid Gid { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор главного пути (АСУ-П)
        /// </summary>
        [DataMember]
        public long? AsupPutglId { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        [DataMember]
        public short Type { get; set; }

        /// <summary>
        /// Тип (строка)
        /// </summary>
        [DataMember]
        public string TypeDesc { get; set; }

        /// <summary>
        /// GID оси пути
        /// </summary>
        [DataMember]
        public string AxisLineGids { get; set; }

        /// <summary>
        /// GID переезда
        /// </summary>
        [DataMember]
        public string CrossGids { get; set; }

        /// <summary>
        /// GID Карты
        /// </summary>
        [DataMember]
        public Guid? MapFragmentGid { get; set; }

        /// <summary>
        /// Название карты
        /// </summary>
        [DataMember]
        public string MapFragmentName { get; set; }

        /// <summary>
        /// Начало границ: Экспл. пикетаж, км
        /// </summary>
        [DataMember]
        public int? StartKm { get; set; }

        /// <summary>
        /// Начало границ: Экспл. пикетаж, м
        /// </summary>
        [DataMember]
        public double? StartM { get; set; }

        /// <summary>
        /// Конец границ: Экспл. пикетаж, км
        /// </summary>
        [DataMember]
        public int? EndKm { get; set; }

        /// <summary>
        /// Конец границ: Экспл. пикетаж, м
        /// </summary>
        [DataMember]
        public double? EndM { get; set; }

        /// <summary>
        /// Пользователь (логин)
        /// </summary>
        [DataMember]
        public string Login { get; set; }

        /// <summary>
        /// Дата/время создания
        /// </summary>
        [DataMember]
        public DateTime CreateDateTime { get; set; }
    }

    /// <summary>
    /// Документ
    /// </summary>
    [DataContract]
    public class DocumentData
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [Description("ID")]
        public Guid Gid { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        [Description("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Классификатор, код
        /// </summary>
        [DataMember]
        [Description("Классификатор, код")]
        public string ClsCode { get; set; }

        /// <summary>
        /// Классификатор, наименование
        /// </summary>
        [DataMember]
        [Description("Классификатор, наименование")]
        public string ClsName { get; set; }

        /// <summary>
        /// Когда издан
        /// </summary>
        [DataMember]
        [Description("Когда издан")]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [DataMember]
        [Description("Автор")]
        public string Author { get; set; }

        /// <summary>
        /// Дата/время внесения в БД
        /// </summary>
        [DataMember]
        [Description("Дата/время внесения в БД")]
        public DateTime StoreDt { get; set; }
    }

    /// <summary>
    /// Содержимое документа
    /// </summary>
    [DataContract]
    public struct DocumentContent
    {
        /// <summary>
        /// Наименование файла
        /// </summary>
        [DataMember]
        public string Filename { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        [DataMember]
        public byte[] Data { get; set; }
    }
}