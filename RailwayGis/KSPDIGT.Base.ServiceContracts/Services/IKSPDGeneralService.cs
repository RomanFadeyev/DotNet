using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.ServiceModel;
using Intech.Common.Annotations;
using Intech.Common.Wcf;
using KSPDIGT.Base.Common.Enums;
using KSPDIGT.Base.DataContracts.AsupCatalog;
using KSPDIGT.Base.DataContracts.BusinessLogicResponses;
using KSPDIGT.Base.DataContracts.Calcs;
using KSPDIGT.Base.DataContracts.ClientServerFactories;
using KSPDIGT.Base.DataContracts.CommentList;
using KSPDIGT.Base.DataContracts.ControlData;
using KSPDIGT.Base.DataContracts.DataModels.Model;
using KSPDIGT.Base.DataContracts.DataModels.Model.DRM;
using KSPDIGT.Base.DataContracts.DatumMark;
using KSPDIGT.Base.DataContracts.Document;
using KSPDIGT.Base.DataContracts.Generic;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.ImpLog;
using KSPDIGT.Base.DataContracts.ImpLogItem;
using KSPDIGT.Base.DataContracts.MapFragment;
using KSPDIGT.Base.DataContracts.ObjectProperties;
using KSPDIGT.Base.DataContracts.OperationDataModels;
using KSPDIGT.Base.DataContracts.ParametricModel;
using KSPDIGT.Base.DataContracts.PhotoFixation;
using KSPDIGT.Base.DataContracts.Picketaging;
using KSPDIGT.Base.DataContracts.PointCloud;
using KSPDIGT.Base.DataContracts.Query;
using KSPDIGT.Base.DataContracts.Settings;
using KSPDIGT.Base.DataContracts.SizeCheck;
using KSPDIGT.Base.DataContracts.Stationing;
using KSPDIGT.Base.DataContracts.Survey;
using KSPDIGT.Base.DataContracts.Surveys;
using KSPDIGT.Base.DataContracts.Surveys.XSD;
using KSPDIGT.Base.DataContracts.Tasks;
using KSPDIGT.Base.DataContracts.Volumes;
using Action = KSPDIGT.Base.DataContracts.DataModels.Model.Action;

namespace KSPDIGT.Base.ServiceContracts.Services
{    

    [ServiceContract]
    [ServiceKnownType(typeof (BasicBusinessLogicResponse))]
    [ServiceKnownType(typeof (DatumMarkBusinessLogicResponse))]
    // ReSharper disable InconsistentNaming
    public interface IKSPDGeneralService : IWcfService
    {
        #region Настройки 

        /// <summary>
        /// Получить сетевое имя сервера приложений
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetServerMachineName();

        /// <summary>
        ///  Получить строку соединения
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetDatabaseConnectionString();

        /// <summary>
        /// Получить период проверки клиентов (секунды)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int GetClientCheckPeriod();

        /// <summary>
        /// Получить глобальные настройки
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        GlobalSettingsClass GetGlobalSettings();


        [OperationContract]
        string GetAsupHostString();

        /// <summary>
        /// Получить информацию о СУБД
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetDatabaseInfo();

        #endregion Настройки

        #region Работа с Параметрической моделью пути

        [OperationContract]
        List<ParametricModelProjectShortInfo> GetParametricModelProjectList(Guid a_MapFragmentGuid);

        [OperationContract]
        List<ParametricModelProjectShortInfoForEditing> GetParametricModelProjectListForEditingByType(int a_Type);

        [OperationContract]
        List<ParametricModelProjectShortInfoForEditing> GetParametricModelProjectListForEditing();

        [OperationContract]
        void UpdateParametricModel(ParametricModelProjectShortInfoForEditing a_ParametricModelProjectShortInfoForEditing);

        [OperationContract]
        void DeleteParametricModel(long a_Id);

        [OperationContract]
        int CreateParametricModelProject(CreatingParametricModelProjectParameters a_CreatingParameters);

        [OperationContract]
        int InsertProject(Prj a_Prj);

        #region Align
        /// <summary>
        /// Список осей пути в ALIGN
        /// </summary>
        /// <param name="a_PrjGid"></param>
        /// <returns>Align</returns>
        [OperationContract]
        List<Align> GetDrmAlignList(Guid a_PrjGid);

        /// <summary>
        /// Получить инф-ю об осях путей (AlignOp+Drm)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AlignOpDrmDto> GetAlignOpDrmInfos();

        /// <summary>
        /// Align по ЦМП гиду
        /// </summary>                
        [OperationContract]
        List<AlignOp> GetAlignsByPrjGid(Guid a_PrjGid);

        /// <summary>
        /// Align по ЦМП id
        /// </summary>                
        [OperationContract]
        List<Align> GetAlignsByPrjId(long a_PrjId);
        
        /// <summary>
        /// Получить Align
        /// </summary>        
        [OperationContract]
        Align GetAlignById(long a_AlignId);

        /// <summary>
        /// Получить Align
        /// </summary>        
        [OperationContract]
        Align GetAlignByGid(Guid a_Gid);

        /// <summary>
        /// Получить все оси (Align), у которых есть точки геометрии, для указанного фрагмента карты
        /// </summary>
        /// <param name="a_MapFragmentGid">Gid фрагмента карты</param>
        /// <returns>Список осей (Align), у которых есть точки геометрии</returns>  
        [OperationContract]
        List<Align> GetAlignsHavePointsByMapFragmentGid(Guid a_MapFragmentGid);

        /// <summary>
        /// Получить Align по AxisFeature.Gid
        /// </summary>        
        [OperationContract]
        Align GetAlignByAxisFetureGid(string a_AxisFeatureGid);

        /// <summary>
        /// Получить инф-ю об осях путей (AlignOp+Drm), связанных с осейвой линией
        /// </summary>
        /// <param name="a_MapGid">Id карты</param>
        /// <param name="a_VectorObjGidn">Vertor Guid из объекта</param>
        /// <returns></returns>
        [OperationContract]
        List<AlignOpDrmDto> GetAlignOpDrmInfosByVectorObj(Guid a_MapGid, string a_VectorObjGidn);


        [OperationContract]
        long InsertAlign(Align a_Align);

        /// <summary>
        /// Сохранить пути
        /// </summary>
        /// <param name="a_Aligns"></param>
        [OperationContract]
        void SaveAligns(IEnumerable<Align> a_Aligns);

        /// <summary>
        /// Сохраняем AsupID для указанного Align
        /// </summary>
        /// <param name="a_AlignId"> </param>
        /// <param name="a_AsupPutglId"> </param>
        /// <returns>Align</returns>
        [OperationContract]
        bool SaveAsupPutglIdForDrmAlign(long a_AlignId, int a_AsupPutglId);

   
        #endregion

        #region StationIrr
        [OperationContract]
        List<StationIrr> GetStationIrrsSortPicketByAlignId(long a_AlignId);

        /// <summary>
        /// Получить информацию для пикетажной разметки
        /// </summary>
        /// <param name="a_AlignId">Id линии</param>
        /// <returns></returns>
        [OperationContract]
        PicketInfoSet GetPicketagePeggingInfo(long a_AlignId);

        [OperationContract]
        PicketagePos[] GetStationIrrsSortPicketByMfaGid(Guid a_MapFragmentAxisGid);

        [OperationContract]
        void InsertStationIrrs(List<StationIrr> a_StationIrrs);

        /// <summary>
        /// Обновить неправильные пикеты (старые - удаляются!)
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <param name="a_Pickets"></param>
        [OperationContract]
        void UpdateIrrPickets(long a_AlignId, PicketInfo[] a_Pickets);
        #endregion 

        #region Points
        /// <summary>
        /// Удаление геометрии
        /// </summary>
        /// <param name="a_AlignId"></param>
        [OperationContract]
        void DeleteAlignPoints(long a_AlignId);

        /// <summary>
        /// Записать для указанного пути точки геометрии (AlignPoint)
        /// Данные передаются в структуре AxisPointDump для экономии памяти
        /// </summary>
        /// <param name="a_AxisPoints"></param>
        [OperationContract]
        void InsertAxisPoints([NotNull]AxisPointDump a_AxisPoints);

        /// <summary>
        /// Возвращает точки оси пути в структуре AxisPoint
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <returns></returns> 
        [OperationContract]
        [NotNull]
        AxisPointDump GetAlignPointsSortDistanceAsDump(long a_AlignId);

        /// <summary>
        /// Возвращает геометрию по ID оси и границам пикетажа
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <param name="a_StationingStart"></param>
        /// <param name="a_StationingEnd"></param>
        /// <returns></returns>
        [OperationContract]
        [NotNull]
        AxisPointDump GetAlignPointsSortDistanceWithinRangesAsDump(long a_AlignId, StationingInfo a_StationingStart, StationingInfo a_StationingEnd);

        /// <summary>
        /// Возвращает точки оси пути, только координаты (XYZ)
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <returns></returns>
        [OperationContract]
        [NotNull]
        List<Point3D> GetAlignPointsSortDistanceAsXYZ(long a_AlignId);

        /// <summary>
        /// Принудительная установка глубины снятия поверхностного слоя #1618
        /// </summary>
        /// <param name="a_PrjId"></param>
        [OperationContract]
        void FixAlignPointsBallast(long a_PrjId);
        #endregion

        [OperationContract]
        void InsertAlignElements(List<AlignElement> a_AlignElements, long a_AlignId);

        /// <summary>
        /// Заменить все элементы плана в указанном пути
        /// </summary>
        /// <param name="a_AlignId">Id пути</param>
        /// <param name="a_AlignElements">Новые элементы</param>
        [OperationContract]
        void UpdateAlignElements(long a_AlignId, [NotNull] IList<AlignElement> a_AlignElements);


        [OperationContract]
        List<GradeProfApPp> GetGradeProfsApPp(long a_AlignId);
      
        /// <summary>
        /// Создание элементов профиля
        /// </summary>
        [OperationContract]
        void InsertGradeProfs(List<GradeProf> a_GradeProfs);

        /// <summary>
        /// Получение информации об уклонах
        /// </summary>
        [OperationContract]
        List<GradeProfOp> GetGradeProfsOp(long a_AlignId);

        /// <summary>
        /// Получение информации об уклонах
        /// </summary>
        [OperationContract]
        List<GradeProf> GetGradeProfs(long a_AlignId);

       
        /// <summary>
        /// Получение информации об уклонах
        /// </summary>
        [OperationContract]
        List<GradeProfOp> GetGradeProfsOpSortPicketByStationing(long a_AlignId, double a_StartKmM,
                                                                double a_EndKmM);

        /// <summary>
        /// Получает сведения об осевой линии цировой(проектной) модели пути
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DigitalModelAxisLineInfo GetDigitalModelAxisLinesInfo(long a_PrjId);

        /// <summary>
        /// Получить списка информации об уклонах, ограниченный дистанцией
        /// </summary>        
        [OperationContract]
        List<GradeProf> GetGradeProfsByDistance(long a_AlignId, double a_StartDistance, double a_EndDistance);

        /// <summary>
        /// Возвращает сортированый по пикетажу список GradeProf
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <returns></returns>
        [OperationContract]
        List<GradeProf> GetGradeProfElementsSortDistance(long a_AlignId);

        /// <summary>
        /// Обновить профиль пути
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void UpdateProfileElements(long a_TrackId, IList<GradeProf> a_GradeProfs);

       
        /// <summary>
        /// Определение начальной точки AlignA на AlignB
        /// </summary>
        [OperationContract]
        PmUnionData GetPMUnionData(int a_IdA, int a_IdB);

        /// <summary>
        /// Пересчет координат AlignA в соотв. с параметрами совмещения PMUnionData
        /// </summary>
        [OperationContract]
        List<Point2D> GetPMUnionPoints(long a_AlignIdA, PmUnionData a_PmUnionData);

        /// <summary>
        /// Обновляет ЦМП фактического положения
        /// </summary>
        /// <param name="a_MapFragmentAxisGid">Гид в таблице MapFragmentAxis</param>
        /// <returns></returns>
        [OperationContract]
        void RefreshFactDrmProject(Guid a_MapFragmentAxisGid);

        /// <summary>
        /// Получить инф-ю о проекте и оси, связанных с осейвой линией
        /// </summary>
        /// <param name="a_MapGid">Id карты</param>
        /// <param name="a_AxisLineGid">Guid оси пути</param>
        /// <param name="a_PrjId">Id ЦМП</param>
        /// <param name="a_PrjName">Наименование ЦМП</param>
        /// <param name="a_AlignId">Id пути</param>
        /// <returns></returns>
        [OperationContract]
        void GetPrjAlignInfo(Guid a_MapGid, string a_AxisLineGid,
            out long? a_PrjId, out string a_PrjName, out long a_AlignId);

        /// <summary>
        /// ЦМП по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Prj GetPrjRowById(long id);

        /// <summary>
        /// ЦМП по гиду
        /// </summary>
        [OperationContract]
        Prj GetPrjByGid(Guid a_PrjGid);


        /// <summary>
        /// Получить проект ЦМП по a_Guid
        /// вместе с данными из Align
        /// </summary>
        /// <param name="a_Guid"></param>
        /// <returns></returns>
        [OperationContract]
        Prj GetPrjWithAligns(Guid a_Guid);



        /// <summary>
        /// Получить AlignElements
        /// </summary>
        /// <param name="a_AlignIds"></param>
        /// <returns></returns>
        [OperationContract]
        List<AlignElementOp> GetAlignElements(long[] a_AlignIds);


        [OperationContract]
        List<CurveDef> GetCurveDef(long a_AlignId);

        ///// <summary>
        ///// Получение точек геометрии по id путей
        ///// </summary>
        ///// <param name="a_AlignIds"></param>
        ///// <returns></returns>
        //[OperationContract]
        //AxisPointDump GetAlignPointsByAligns(long[] a_AlignIds);

        /// <summary>
        /// Получение скоростей по идентификатору оси
        /// </summary>
        /// <param name="a_AlignId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Speed> GetAlignSpeeds(long a_AlignId);



       
        /// <summary>
        /// Получить AlignElementsOpPp
        /// </summary>
        [OperationContract]
        List<AlignElementOpPp> GetAlignElementsOpPpSortDistance(long a_AlignId);

        /// <summary>
        /// Получить список AlignElements, ограниченный дистанцией
        /// </summary>        
        [OperationContract]
        List<AlignElement> GetAlignElementsByDistance(long a_AlignId, double a_StartDistance, double a_EndDistance);

        /// <summary>
        /// Получить AlignElements
        /// </summary>        
        [OperationContract]
        List<AlignElement> GetAlignElementsSortDistance(long a_AlignId);

        /// <summary>
        /// Получить AlignElementsOp
        /// </summary>        
        [OperationContract]
        List<AlignElementOp> GetAlignElementsOpSortDistance(long a_AlignId);

        /// <summary>
        /// Получить AlignElements
        /// </summary>        
        [OperationContract]
        List<AlignElementOp> GetAlignElementsSortPicketByPicketage(long a_AlignId, double a_StartKmM, double a_EndKmM);

        /// <summary>
        /// Возвращает список точек изменений атрибутов
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        /// <param name="a_AxisLineGid"></param>
        /// <param name="a_StartDistance"></param>
        /// <param name="a_EndDistance"></param>
        /// <returns></returns>
        [OperationContract]
        List<WayPartAttributeChangePoint> GetAttributeChangePointsFromDb(Guid a_MapFragmentGid, string a_AxisLineGid, double? a_StartDistance, double? a_EndDistance);

        #endregion Работа с Параметрической моделью пути

        #region Нормативные значения междупутья

        [OperationContract]
        ASUPNorm.ASUP_NORM_INTERTRACKDataTable GetAsupNorm(string a_SqlCommand);

        #endregion

        #region Получение информации об объекте

        [OperationContract]
        StationPlanDS.ASUE_STATIONPLANDataTable GetStationPlanDS(string a_SqlCommand);

        [OperationContract]
        GeoRadarDS.GEORADARIMAGEDataTable GetGeoRadarDS(string a_SqlCommand);


        [OperationContract]
        IssoDS.ASUP_ISSODataTable GetIssoDS(string a_SqlCommand);

        [OperationContract]
        object ExecuteScalar(string a_SelectSqlCommand);


        /// <summary>
        /// Получить снимки георадара
        /// </summary>
        /// <param name="a_SurveyGid">Id проезда</param>
        /// <returns></returns>
        [OperationContract]
        List<GeoradarImage> GetGeoradarImages(Guid a_SurveyGid);

        /// <summary>
        /// Получить данные георадара
        /// </summary>
        /// <param name="a_SurveyGid">Id проезда</param>
        /// <returns></returns>
        [OperationContract]
        List<GeoradarValue> GetGeoradarDatas(Guid a_SurveyGid);

        /// <summary>
        /// Добавление данных о ТЛС
        /// </summary>
        /// <param name="a_LsData"></param>
        [OperationContract]
        void AddLazerData(LsData a_LsData);

        /// <summary>
        /// Добавление данных фракций
        /// </summary>
        /// <param name="a_Fractions"></param>
        [OperationContract]
        void AddFractionData(List<Fraction> a_Fractions);

        /// <summary>
        /// Добавление данных о 3D-поверхностях
        /// </summary>
        /// <param name="a_SurfaceData"></param>
        [OperationContract]
        void AddSurfaceLazerData(SurfaceData a_SurfaceData);

        /// <summary>
        /// Получить данные ТЛС из проезда. Возвращает всегда единственный экзмепляр LsData, не смотря на то, что структура БД позволяет хранить произвольное кол-во LsData в проезде
        /// </summary>
        /// <param name="a_SurveyGid">Id проезда</param>
        /// <returns></returns>
        [OperationContract]
        LsData GetSoleLsData(Guid a_SurveyGid);

        /// <summary>
        /// Получить Gid коорд. системы
        /// </summary>
        /// <param name="a_Gid">Gid объекта</param>
        /// <returns></returns>
        [OperationContract]
        Guid CoordGidForLsData(Guid a_Gid);

        /// <summary>
        /// Получить данные 3D поверхности
        /// </summary>
        /// <param name="a_SurveyGid">Id проезда</param>
        /// <returns></returns>
        [OperationContract]
        List<SurfaceData> GetSurfaceData(Guid a_SurveyGid);

        /// <summary>
        /// Получить Длины километров
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<KmLengthDto> GetKmLength(Guid a_AxisGid);

        /// <summary>
        /// Получить 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StationingLengthDto> GetStationingLength(Guid a_AxisGid);

        /// <summary>
        /// Получить 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StationingPointDto> GetStationingPoint(Guid a_AxisGid);

        /// <summary>
        /// Получить MapFragmentAxisLine
        /// </summary>
        /// <param name="a_AxisGid"></param>
        /// <returns></returns>
        [OperationContract]
        MapFragmentAxisLine GetAxisLineWithStationing(string a_AxisGid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        /// <param name="a_AxisGid"></param>
        /// <returns></returns>
        [OperationContract]
        Guid GetMapFragmentAxisLineGid(Guid a_MapFragmentGid, string a_AxisGid);

        /// <summary>
        /// Получение FactDrmAlignGid
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        /// <param name="a_AxisGid"></param>
        /// <returns></returns>
        [OperationContract]
        Guid? GetFactDrmAlignGid(Guid a_MapFragmentGid, string a_AxisGid);

        /// <summary>
        /// Получение заголовка пути
        /// </summary>
        [OperationContract]
        string GetAxisTitleByAlignOp(AlignOp a_AlignOp);

        /// <summary>
        /// Получение строки с участками и перегонами
        /// </summary>
        [OperationContract]
        string GetPeregNames(int a_AsupId, int a_StartKm, double a_StartM, int a_EndKm, double a_EndM);

        #endregion

        #region Работа с материализованными представлениями

        /// <summary>
        /// Получить весь список материализованных представлений
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetAllMVIEWS(string a_Owner, string a_Name);

        /// <summary>
        /// Обновить материализованное представление
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string UpdateMVIEWS(string a_Owner, string a_Name);

        #endregion

        #region Работа с классификаторами

        [OperationContract]
        List<ClassifierShortInfoClass> GetClassifiersShortInfoByParentCode(string a_ParentCode, bool a_IncludeParentInfo);

        [OperationContract]
        List<ClassifierShortInfoClass> GetClassifiersShortInfoByParentCodeIncludeChildren(string a_ParentCode,
                                                                                          bool a_IncludeParentInfo);

        [OperationContract]
        List<ClassifierFullInfoClass> GetClassifierRecords(int a_Status = -1);

        /// <summary>
        /// Обновляет классификатор в таблице классификаторов
        /// </summary>
        /// <param name="a_ClsOld">Старая запись классификатора</param>
        /// <param name="a_ClsNew">Новая запись классификатора</param>
        /// <returns>Результат операции</returns>
        [OperationContract]
        string UpdateCls(Cls a_ClsOld, Cls a_ClsNew);

        /// <summary>
        /// Получить весь список классификаторов
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Cls> GetAllCls();

        /// <summary>
        /// Получить классификатор по коду
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Cls GetClsByCode(string a_Code);

        /// <summary>
        /// Получить классификатор по имени
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        //Cls GetClsByName(string a_Name);
        /// <summary>
        /// Ищет код классификатора по его полному наименованию или короткому наименованию
        /// </summary>
        /// <param name="a_ParentCode">Код предка искомого классификатора(обязательно)</param>
        /// <param name="a_Names">Список искомых наименований</param>
        /// <remarks>
        /// Ищет по всем статусам
        /// </remarks>
        /// <returns>Список найденных кодов. Если код был не найден то в соответствующей записи будет string.empty</returns>
        [OperationContract]
        List<string> FindClassifierCodeByNameOrShortname(string a_ParentCode, IEnumerable<string> a_Names);

        /// <summary>
        /// Добавляет классификатор в таблицу классификаторов
        /// </summary>
        /// <param name="a_Cls">Запись о классификаторе</param>
        /// <returns>Код вновь созданного классификатора и текст ошибки</returns>
        [OperationContract]
        ResultClassifier AddClassifier(Cls a_Cls);

        #region Работа с системами координат

        [OperationContract]
        List<CoordSysInfoClass> GetCoordSysRecords(bool a_Archive);

        [OperationContract]
        List<CoordSys> GetCoordSysRecordsExtTransform(Guid a_CoordSysGid);



        [OperationContract]
        void AddCoordSysRecord(CoordSys a_CoordSys);

        [OperationContract]
        void UpdateCoordSysRecord(CoordSys a_CoordSys);

        [OperationContract]
        void DeleteCoordSysRecord(Guid a_Gid);

        /// <summary>
        /// Записи каркасной сети
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Igs> GetIgsRows();

        /// <summary>
        /// Получить систему кооридинат по гиду (вместе с подчиненными объектами)
        /// </summary>
        /// <param name="a_CoordSysGid"></param>
        /// <returns></returns>
        [OperationContract]
        CoordSys GetCoordSysByGid(Guid a_CoordSysGid);

        /// <summary>
        /// Добавление каркасной сети
        /// </summary>
        /// <param name="a_Igs"></param>
        [OperationContract]
        void CreateIgs(Igs a_Igs);

        /// <summary>
        /// Изменение каркасной сети
        /// </summary>
        /// <param name="a_Igs"></param>
        [OperationContract]
        void EditIgs(Igs a_Igs);

        /// <summary>
        /// Удаление каркасной сети
        /// </summary>
        /// <param name="a_Gid"></param>
        [OperationContract]
        void DeleteIgs(Guid a_Gid);

        #endregion Работа с системами координат        

        #endregion Работа с классификаторами 

        #region Администрирование пользователей

        /// <summary>
        /// Получить список пользователей (вместе с ролями, включая удаленных)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<User> GetUsers();
        
        [OperationContract]
        User GetUserByGid(Guid a_Gid);

        [OperationContract]
        User GetUserByLogin(string a_Login);
        
       

        [OperationContract]
        List<Role> GetRolesList();

        [OperationContract]
        List<Role> GetRolesActionsList();

        [OperationContract]
        Action GetActionByName(string a_Name);

        [OperationContract]
        void CreateUser(User a_User);

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="a_User">Пользователь</param>
        /// <param name="a_UpdatePassword">Обновить пароль</param>
        [OperationContract]
        void UpdateUser(User a_User, bool a_UpdatePassword);

        [OperationContract]
        void MarkUserAsDeleted(Guid a_Gid);

        [OperationContract]
        void DeleteRole(long a_Id);

        #endregion

        #region Справочники АСУ-П

        /// <summary>
        /// Определение пути по Id
        /// </summary>
        [OperationContract]
        AsupPutgl GetAsupPutglByPutglId(int a_AsupPutglId);

        [OperationContract]
        List<AsupCatalogInfoClass.DistancesWayInfoClass> GetDistancesWayList();

        [OperationContract]
        List<AsupCatalogInfoClass.MainWaysInfoClass> GetMainWaysList();

        [OperationContract]
        List<AsupCatalogInfoClass.IssoInfoClass> GetIssoList();

        #endregion Справочники АСУ-П

        #region Работа с реперами

       
        /// <summary>
        /// Получить историю репера с документами
        /// </summary>
        /// <param name="a_RecLifelineGid"></param>
        [OperationContract]
        DatumWithDocuments[] GetDatumHistoryWithDocs(Guid a_RecLifelineGid);

        /// <summary>
        /// Получить описания реперов с координатами
        /// </summary>
        /// <param name="a_IncludeArchive">Включая архивные (удаленные)</param>
        /// <returns></returns>
        [OperationContract]
        List<DatumInfoClass> GetDatumMarks(bool a_IncludeArchive);

        [OperationContract]
        ImagesModel GetImgRecords(Guid a_DatumId);

        [OperationContract]
        ImagesModel GetImgRecordsByType(Guid a_DatumId, int? a_Type);

        /// <summary>
        /// Получить репер по Gid
        /// </summary>
        /// <param name="a_Gid"></param>
        [OperationContract]
        Datum GetDatumMarkByGid(Guid a_Gid);

        /// <summary>
        /// Добавить новый репер
        /// </summary>
        /// <param name="a_Datum">Репер</param>
        /// <param name="a_Files">Содержимое файлов</param>
        /// <param name="a_NewDocsWithContents">Новые документы к реперу вместе с содержимым</param>
        [OperationContract]
        Guid AddDatumMark(Datum a_Datum, List<DatumMarkFileData> a_Files, Tuple<Document, byte[]>[] a_NewDocsWithContents);

        /// <summary>
        /// Обновить параметры репера
        /// </summary>
        /// <param name="a_Datum"></param>
        /// <param name="a_NewDocsWithContents">Новые документы к реперу вместе с содержимым</param>
        /// <param name="a_AppendedExistDocGids">Gid-ы документов из БД, "присоедененных" к реперу</param>
        /// <returns>Новый GID репера</returns>
        [OperationContract]
        Guid UpdateDatumMark(Datum a_Datum, Tuple<Document, byte[]>[] a_NewDocsWithContents, Guid[] a_AppendedExistDocGids);

        /// <summary>
        /// Обновить состояние реперов
        /// </summary>
        /// <param name="a_DatumGuids">Guids реперов</param>
        /// <param name="a_FocusedDatumGuid">Guid "основного" репера (из списка) <param name="a_DatumGuids"></param></param>
        /// <param name="a_NewStatus">Новый статус</param>
        /// <param name="a_NewAddCause">Новое значение основания</param>
        /// <param name="a_DocumentTuples">Файлы основания</param>
        /// <returns>Новый Guid "основного" репера</returns>
        [OperationContract]
        Guid UpdateDatumsStatus(Guid[] a_DatumGuids, Guid a_FocusedDatumGuid, Datum.StatusEnum a_NewStatus,
            string a_NewAddCause, Tuple<Document, byte[]>[] a_DocumentTuples);

        /*/// <summary>
        /// Обновить состояние репера
        /// </summary>
        /// <param name="a_DatumGuid"></param>
        /// <param name="a_NewStatus"></param>
        /// <param name="a_NewAddCause">Новый GID репера</param>
        /// <param name="a_DocumentTuples">Документы-основания</param>
        /// <returns></returns>
        [OperationContract]
        Guid UpdateDatumStatus(Guid a_DatumGuid, Datum.StatusEnum a_NewStatus, string a_NewAddCause, Tuple<Document, byte[]>[] a_DocumentTuples);*/

        /// <summary>
        /// Удалить репер(переводит репер в архив)
        /// </summary>
        /// <param name="a_Gid"></param>
        /// <param name="a_Cause">Основание</param>
        [OperationContract]
        void DeleteDatumMark(Guid a_Gid, string a_Cause);

        /// <summary>
        /// Удалить реперы (переводит реперы в архив)
        /// </summary>
        /// <param name="a_Gids"></param>
        /// <param name="a_Cause">Основание</param>
        /// <param name="a_Documents">Документы-основания</param>
        [OperationContract]
        void DeleteDatumMarks(Guid[] a_Gids, string a_Cause, Tuple<Document, byte[]>[] a_Documents);

        /// <summary>
        /// Получить документы-основания для репера
        /// </summary>
        /// <param name="a_DatumGid"></param>
        /// <returns></returns>
        [OperationContract]
        Document[] GetDatumDocuments(Guid a_DatumGid);

        #region Импорт

        /// <summary>
        /// Проверить репер на корректность данных
        /// </summary>
        /// <param name="a_Datum">Проверяемый репер</param>
        /// <param name="a_BusinessLogicResponse">Ответ</param>
        [OperationContract]
        void ValidateDatumMark(Datum a_Datum, out DatumMarkBusinessLogicResponse a_BusinessLogicResponse);

        /// <summary>
        /// Возвращает первый GID по GID истории
        /// </summary>
        /// <param name="a_RecLifelineGid"></param>
        /// <param name="a_CheckOnlyActual"></param>
        /// <returns>Guid.Empty если не найден</returns>
        [OperationContract]
        Guid GetDatumMarkGidByHistoryGid(Guid a_RecLifelineGid, bool a_CheckOnlyActual);

        #endregion Импорт

        #endregion Работа с реперами

        #region Работа с данными администратора

        /// <summary>
        /// Возвращает список ролей хранимых в БД
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Role> GetAllRoles(bool a_RefreshFromServer);

        /// <summary>
        /// Возвращает список действий связанных с ролями
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Action> GetAllActions();

        /// <summary>
        /// Возвращает список областей связанных с ролями
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AccessibleMapExtent> GetAllRoleMapExtents();

        /// <summary>
        /// Возвращает список прав для пользователя, который указан в контексте
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Action> GetCurrentUserActions();

        /// <summary>
        /// Возвращает список всех слоев в рабочих наборах
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> GetAllLayersFromMapFragments();

        /// <summary>
        /// Применяет изменения в ролях
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void ApplyRolesChanges(List<Role> a_Roles);


        /// <summary>
        /// Возвращает информацию о пользователях КСПД
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<UserInfo> GetBaseUsersInfo();

        /// <summary>
        /// Возвращает информацию о пользователях АСУП
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<UserInfo> GetAllUsersInfo();

        

        /// <summary>
        /// Возвращает список фрагментов
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MapFragment> GetMapFragments();

        /// <summary>
        /// Возвращает фрагмент по гиду
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MapFragment GetMapFragmentByGid(Guid a_Gid);

        /// <summary>
        /// Возвращает данные о станциях, перегонах, остановочных пунктах .... по MapFragmentAxisLineGid
        /// </summary>
        /// <param name="a_MapFragmentAxisLineGid"></param>
        /// <returns></returns>
        [OperationContract]
        List<StationingCacheWaypartRange> StationingCacheWaypartRange(Guid a_MapFragmentAxisLineGid);

        /// <summary>
        /// Определение оси по гиду оси и по гиду фрагмента карты
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        /// <param name="a_AxisLineGid"></param>
        /// <returns></returns>
        [OperationContract]
        MapFragmentAxisLine GetAxisLine(Guid a_MapFragmentGid, Guid a_AxisLineGid);


        /// <summary>
        /// Сохраняет фрагменты
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SetMapFragments(List<MapFragment> a_MapFragments);

        /// <summary>
        /// Получить все осевые линии указанного фрагмента карты
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MapFragmentAxis> GetAllMapFragmentAxes(Guid? a_MapFragmentGid = null, bool a_IncludeAxisLine = true);

        /// <summary>
        /// Получить все осевые линии указанного типа для указанного фрагмента карты
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MapFragmentAxis> GetMapFragmentAxesByWayType(Guid a_MapFragmentGid, int a_WayType, bool a_IncludeAxisLine = true);


        /// <summary>
        /// Получить осевую линию фрагмента карты по AxisLineGid
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MapFragmentAxis GetMapFragmentAxis(Guid a_MapFragmentGid, string a_AxisLineGid);

        /// <summary>
        /// Добавляет запись о границе путей АСУ-П 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void AddMapFragmentAxis(MapFragmentAxis a_MapFragmentAxis,
                                List<StationingCacheWaypartRange> a_StationingCacheWaypartRanges,
                                List<StationingCachePropRange> a_StationingCachePropRanges,
                                List<PicketPointInfo> a_PicketPointInfos);

        /// <summary>
        /// Удаляет данные об осях на основе гидов
        /// </summary>
        /// <param name="a_MapFragmentGuids"></param>
        [OperationContract]
        void DeleteMapFragmentAxes(List<Guid> a_MapFragmentGuids);

        /// <summary>
        /// Очищает фрагменты карт от осевых линий
        /// </summary>
        /// <param name="a_MapFragmentGuids"></param>
        [OperationContract]        
        void ClearMapFragmentAxes(List<Guid> a_MapFragmentGuids);


        /// <summary>
        /// Получить режим авторизации, действующий на сервере
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        AuthorizationMode GetAuthorizationMode();

        

        #endregion Работа с данными администратора
        
        #region Работа с журналом выдачи сведений

        /// <summary>
        /// Получить информацию из журнала выдачи сведений
        /// </summary>
        /// <returns>Записи журнала выдачи сведений</returns>
        [OperationContract]
        List<ExpLog> GetExpLogs();

        /// <summary>
        /// Добавление записи в журнал
        /// </summary> 
        [OperationContract]
        void AddExpLogRecord(ExpLog a_ExpLog, byte[] a_File);

        #endregion

        #region Работа с журналом импорта сведений

        /// <summary>
        /// Получить информацию из журнала импорта сведений
        /// </summary>
        /// <returns>Записи журнала импорта сведений</returns>
        [OperationContract]
        List<ImpLogInfo> GetImpLogs();


        /// <summary>
        /// Получить список объектов из журнала импорта сведений
        /// </summary>
        /// <returns>Список объектов журнала импорта сведений</returns>
        [OperationContract]
        List<JournalItemInfo> GetImpLogItems(Guid a_ImpLogGid);

        /// <summary>
        /// Получить список объектов из журнала экпорта сведений
        /// </summary>
        /// <returns>Список объектов журнала экпорта сведений</returns>
        [OperationContract]
        List<JournalItemInfo> GetExpLogItems(Guid a_ExpLogGid);

        /// <summary>
        /// Вносит сведения об сессии импорта
        /// </summary>
        /// <param name="a_ImpLog">Сведения об импорте</param>
        /// <param name="a_LogContent">Содержимое импортируемого файла</param>
        /// <param name="a_LogContentFileName">Наименование файла</param>
        [OperationContract]
        void LogImportSession(ImpLog a_ImpLog, byte[] a_LogContent, string a_LogContentFileName);

        #endregion

        #region Работа с ссылочными данными(REFDATA)

        #region Получение непреобразованных сущностей

        /// <summary>
        /// Получить список дорог
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AsupDor> GetAllRailroads();

        /// <summary>
        /// Получить список дистанций принадлежащих определенной дороге
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AsupDist> GetAllDistancesByRailroadCode(short a_RailroadCode);

        /// <summary>
        /// Получить список всех дистанций (для всех дорог)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<AsupDist> GetAllDistances();

        #endregion Получение непреобразованных сущностей

        /// <summary>
        /// Возвращает записи об путях из АСУ-П
        /// </summary>
        /// <param name="a_Ids">Список интересующих ID</param>
        /// <returns>Список информации об путях</returns>С
        [OperationContract]
        List<AsupPutgl> GetAsupPutgl(IEnumerable<long> a_Ids);

        /// <summary>
        /// Проверяет наличие георадарной съемки
        /// </summary>
        /// <param name="a_ClsType">Тип проезда</param>
        /// <param name="a_AsupId">Идентификатор АСУ-П выбранной оси пути</param>
        /// <param name="a_TaskGid">GID текущей задачи</param>
        /// <returns>GID проезда</returns>С
        [OperationContract]
        Guid CheckSurveyGeoradar(string a_ClsType, long? a_AsupId, Guid a_TaskGid);

        /// <summary>
        /// Возвращает записи проездов 
        /// </summary>
        /// <param name="a_Gid">Идентификатор проезда</param>
        /// <returns>Список проездов</returns>С
        [OperationContract]
        List<SurveyInfo> GetSurveyInfos(Guid? a_Gid);

       

        /// <summary>
        /// Получить список проездов по пути указанного типа
        /// </summary>
        /// <param name="a_AsupPutglId">ID пути</param>
        /// <param name="a_SurveyTypes">Список типов проездов для которых выбирать данные</param>
        /// <param name="a_KmM"></param>
        /// <returns>Список проездов</returns>
        [OperationContract]
        List<SurveyInfo> GetSurveysByTypes(long? a_AsupPutglId, List<SurveyType.Types> a_SurveyTypes, double? a_KmM);

        /// <summary>
        /// Проверяет список ГИД проездов пересекающихся с указанной областью.
        /// Если указана пустой спиоск гидо то выберет все, котороые попадают в регион
        /// </summary>
        /// <param name="a_CheckedSurveyGids"></param>
        /// <param name="a_Region"></param>
        /// <returns></returns>
        [OperationContract]
        List<Guid> GetSurveyGidsByRegion(List<Guid> a_CheckedSurveyGids, Bounds2D a_Region);

        /// <summary>
        /// Получаем отчет
        /// </summary>
        /// <param name="a_CalcGid">Идентификатор расчета</param>
        /// <param name="a_DocClsType">Тип отчета</param>
        /// <param name="a_Prnx"></param>
        /// <param name="a_Pdf"></param>
        /// <returns></returns>
        [OperationContract]
        ReportInfoClass GetReportClass(Guid a_CalcGid, string a_DocClsType, bool a_Prnx = true, bool a_Pdf = false);

        /// <summary>
        /// Проверяем наличие отчета в базе
        /// </summary>
        /// <param name="a_CalcGid">Идентификатор расчета</param>
        /// <param name="a_DocClsType">Тип отчета</param>
        /// <returns></returns>
        [OperationContract]
        bool CheckDocumentCalc(Guid a_CalcGid, string a_DocClsType);

        /// <summary>
        /// Обновление расчета в БД
        /// </summary>
        /// <param name="a_Guid"></param>
        /// <param name="a_Data"></param>
        [OperationContract]
        void UpdateCalcDatas(Guid a_Guid, byte[] a_Data);

        /// <summary>
        /// Сохранение расчета в БД
        /// </summary>
        /// <param name="a_Calc">Параметры расчета</param>
        /// <param name="a_CalcCharts">Список фремов на диаграмме</param>
        /// <param name="a_CalcAttrs">Атрибуты расчетов</param>
        /// <param name="a_CalcValues">Значения расчета</param>
        /// <param name="a_CalcDatasWithObjectName">Расчетные данные</param>
        /// <param name="a_ReportInfosClass">Данные отчета</param>
        /// <param name="a_Result"></param>
        [OperationContract]
        CalcInfo CreateCalcDatas(Calc a_Calc, List<CalcChart> a_CalcCharts, List<CalcAttr> a_CalcAttrs,
                                 List<CalcValue> a_CalcValues, List<CalcDatasWithObjectName> a_CalcDatasWithObjectName,
                                 List<ReportInfoClass> a_ReportInfosClass, out string a_Result);

        /// <summary>
        /// Удаление расчета
        /// </summary>
        /// <param name="a_CalcGid"></param>
        [OperationContract]
        void DeleteCalc(Guid a_CalcGid);        

        /// <summary>
        /// Получить данные расчета
        /// </summary>
        /// <param name="a_CalcGid">Gid расчета</param>
        /// <returns>Расчет со всеми данными</returns>
        [OperationContract]
        CalcResult GetCalcResult(Guid a_CalcGid);

        /// <summary>
        /// Получить данные расчета
        /// </summary>
        /// <param name="a_Gid">Gid данных</param>
        [OperationContract]
        byte[] GetSpecData(Guid a_Gid);
        
        /// <summary>
        /// Получить данные расчета
        /// </summary>
        /// <param name="a_CalcGid">Gid расчета</param>
        /// <param name="a_CalcDatasWithObjectName">Данные расчета с названиями объектов</param>
        /// <param name="a_CalcValues">Значения расчета</param>
        /// <param name="a_CalcAttrs">Атрибуты</param>
        [OperationContract]
        void GetCalcDatasWithObjectName(Guid a_CalcGid,
                                        out List<CalcDatasWithObjectName> a_CalcDatasWithObjectName,
                                        out List<CalcValue> a_CalcValues, out List<CalcAttr> a_CalcAttrs);

        /// <summary>
        /// Получить список результатов расчета для указанного расчета
        /// </summary>
        /// <param name="a_Calc">Расчет</param>
        /// <returns>Список результатов расчета</returns>
        [OperationContract]
        List<CalcInfoExt> GetCalcInfosExtByCalc(Calc a_Calc);

        /// <summary>
        /// Получить список результатов расчета по пути указанного типа
        /// </summary>
        /// <param name="a_AsupPutglId">ID пути</param>
        /// <param name="a_Type"></param>
        /// <param name="a_KmM"></param>
        /// <returns>Список результатов расчета</returns>
        [OperationContract]
        List<CalcInfo> GetCalcInfosByType(long? a_AsupPutglId, Calc.Types? a_Type, double? a_KmM);

        /// <summary>
        /// Получить список документов проезда
        /// </summary>
        /// <param name="a_SurveyGid">Guid проезда</param>
        /// <returns>Cписок документов проезда</returns>
        [OperationContract]
        List<DocumentToSurvey> GetDocumentsSurvey(Guid a_SurveyGid);

        /// <summary>
        /// Получить список проездов с документами по пути
        /// </summary>
        /// <param name="a_PutId">ID пути</param>
        /// <returns>Список проездов с документами</returns>
        [OperationContract]
        List<DocumentToSurvey> GetSurveysWithDoc(long? a_PutId);


        /// <summary>
        /// Возвращает список проездов c ТЛС, попадающих в определенный буфер для указанного фрагмента (или для всех фрагментов, если null)
        /// </summary>
        /// <param name="a_MapFragmentGid">Фрагмент карты или null</param>
        /// <param name="a_Polygon">Список точек</param>
        /// <returns></returns>
        [OperationContract]
        List<SurveyDisplayInfo> GetSurveysForMapFragment(Guid? a_MapFragmentGid, List<Point2D> a_Polygon);

        

        

        /// <summary>
        /// Получает Survey с заполнененными вспомогательными таблицами
        /// </summary>
        /// <param name="a_SurveyType">Тип проездов</param>
        /// <param name="a_AsupPutglId">ID Пути</param>
        /// <param name="a_Stationing">Пикетаж который должен входить в проезд</param>
        /// <param name="a_OnlySuitableForStationing">Возвращать записи только включающие пикетаж</param>
        /// <param name="a_CoordSysGid">Система координат</param>
        /// <param name="a_SearchedRegion">Координаты с которыми должен пересекаться переезд</param>
        /// <returns></returns>
        [OperationContract]
        List<Survey> GetSurveysForControl(
            SurveyType.Types a_SurveyType,
            long? a_AsupPutglId,
            StationingInfo a_Stationing,
            bool a_OnlySuitableForStationing,
            Guid? a_CoordSysGid,
            Bounds2D a_SearchedRegion);

        /// <summary>
        /// Получает Survey с заполнененными вспомогательными таблицами
        /// </summary>
        /// <param name="a_Guids">Перечень ГИД проездов</param>
        /// <returns></returns>
        [OperationContract]
        List<Survey> GetSurveysBySurveyGuids(List<Guid> a_Guids);

        /// <summary>
        /// Получить список проездов с измерениями по пути
        /// </summary>
        /// <param name="a_PutId">ID пути</param>
        /// <returns>Список проездов с документами</returns>
        [OperationContract]
        List<SurveyFull> GetSurveysFull(long? a_PutId);

        /// <summary>
        /// Получить список измерений по проезду
        /// </summary>
        /// <param name="a_SurveyId">ID проезда</param>
        /// <returns>Список проездов с документами</returns>
        [OperationContract]
        List<Measure> GetMeasures(Guid a_SurveyId);

        /// <summary>
        /// Получить список измерений по проезду
        /// </summary>
        /// <param name="a_SurveyId">ID проезда</param>
        /// <param name="a_Skip">Пропустить</param>
        /// <param name="a_Count">Кол-во</param>
        /// <returns>Список проездов с документами</returns>
        [OperationContract]
        List<Measure> GetMeasuresEx(Guid a_SurveyId, int a_Skip, int a_Count);

        /// <summary>
        /// Получить кол-во измерений по проезду
        /// </summary>
        /// <param name="a_SurveyId">ID проезда</param>
        /// <returns>Кол-во проездов</returns>
        [OperationContract]
        long GetMeasuresCount(Guid a_SurveyId);




        /// <summary>
        /// Возвращает данные измерений
        /// </summary>
        /// <param name="a_Guids"></param>
        /// <param name="a_ClsType"></param>
        /// <param name="a_KmMFirst"></param>
        /// <param name="a_KmMSecond"></param>
        /// <returns></returns>
        [OperationContract]
        List<Measure> GetMeasureDatas(List<Guid> a_Guids, string a_ClsType, double? a_KmMFirst, double? a_KmMSecond);

        /// <summary>
        /// Возвращает список георадарных данных
        /// </summary>
        /// <param name="a_Guids"></param>
        /// <returns></returns>
        [OperationContract]
        List<GeoradarData> GetGeoradarDatasByListGuid(List<Guid> a_Guids);

        /// <summary>
        /// Возвращает данные георадара
        /// </summary>
        /// <param name="a_Guids"></param>
        /// <param name="a_KMMFirst"></param>
        /// <param name="a_KMMSecond"></param>
        /// <returns></returns>
        [OperationContract]
        List<GeoradarValue> GetGeoradarValuesByListGuid(List<Guid> a_Guids, double? a_KMMFirst, double? a_KMMSecond);

        /// <summary>
        /// Возвращает участки пути и перегоны
        /// </summary>
        /// <returns>участки пути и перегоны</returns>
        [OperationContract]
        List<PutglPeregInfo> GetPutglPeregInfo(int? a_AsupId, int a_StartKm, double a_StartM, int a_EndKm, double a_EndM);

        /// <summary>
        /// Возвращает перегон
        /// </summary>
        /// <returns>участки пути и перегоны</returns>
        [OperationContract]
        List<AsupPutglUp> GetPeregon();


        /// <summary>
        /// Получить список путей с дорогами
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<RoadWayInfo> GetRoadWayList();

        /// <summary>
        /// Получить наименования участка пути и перегона для оси пути
        /// </summary>
        /// <param name="a_AsupId"></param>
        /// <param name="a_StartKm"></param>
        /// <param name="a_StartM"></param>
        /// <param name="a_Direction"></param>
        /// <param name="a_Stage"></param>
        [OperationContract]
        void GetWayDirectionInfo(int a_AsupId, int a_StartKm, decimal a_StartM, out string a_Direction, out string a_Stage);

        /// <summary>
        /// Получить том
        /// </summary>
        /// <param name="a_DocumentGid"></param>
        /// <returns></returns>
        [OperationContract]
        Volume GetVolumeByDocument(Guid a_DocumentGid);

        #endregion Работа с ссылочными данными(REFDATA)

        #region Связанное с фрагментами карты

        /// <summary>
        /// Получить список фрагментов карты для текущего пикетажа
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MapFragmentWithAxesInfo> GetMapFragmentForInfoStationing(Guid a_CurrentMapFragmentGid, int a_AsupId,
                                                                      int a_Km, double a_M);

        /// <summary>
        /// Функция получения доступных фрагментов карты
        /// </summary>
        /// <param name="a_QueryParameters">Параметры запроса</param>
        /// <returns></returns>
        [OperationContract]
        List<MapFragmentShortInfo> GetMapFragmentShortInfos(MapFragmentShortInfoQueryParameters a_QueryParameters = null);


        /// <summary>
        /// Получает основные подключения для указанной карты или первой карты (если null)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        PrimaryConnectionsClass GetPrimaryConnectionsForMapFragment(Guid? a_MapFragmentGid);

        /// <summary>
        /// Получает рабочий набор для указанной карты
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetMapDocumentContent();

        /// <summary>
        /// Получить список информации о слоях карты
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        /// <returns></returns>
        [OperationContract][NotNull]
        List<MapFragmentLayerInfo> GetMapFragmentLayerInfos(Guid a_MapFragmentGid);
       

        #endregion Связанное с фрагментами карты

        #region Работа с документами проезда

        /// <summary>
        /// Добавление документа проезда
        /// </summary>
        /// <param name="a_Document">Документ</param>
        /// <param name="a_DocumentToSurvey">Проезд для документов</param>
        /// <param name="a_Survey">Проезд</param>
        /// <param name="a_File">Файл</param>
        [OperationContract]
        string AddDocumentSurvey(Document a_Document, DocumentToSurvey a_DocumentToSurvey,
                                 Survey a_Survey, byte[] a_File);

        /// <summary>
        /// Добавить документ
        /// </summary>
        /// <param name="a_Document"></param>
        /// <param name="a_PrnxFile"></param>
        /// <param name="a_PdfFile"></param>
        [OperationContract]
        void AddDocument(Document a_Document, byte[] a_PrnxFile, byte[] a_PdfFile);

        /// <summary>
        /// Изменить документ
        /// </summary>
        /// <param name="a_Document"></param>
        [OperationContract]
        void UpdateDocument(Document a_Document);

        /// <summary>
        /// Добавить новый проезд
        /// </summary>
        /// <param name="a_Survey"></param>
        [OperationContract]
        void AddSurvey(Survey a_Survey);

        /// <summary>
        /// Удаляем проезд
        /// </summary>
        /// <param name="a_Survey"></param>
        [OperationContract]
        void DelSurvey(Survey a_Survey);

        /// <summary>
        /// Удаляем проезд
        /// </summary>        
        [OperationContract]
        void DelSurveyByGid(Guid a_SurveyGid);

        /// <summary>
        /// Обновить пикетаж проезда
        /// </summary>
        /// <param name="a_SurveyGid"></param>
        /// <param name="a_StartStationing"></param>
        /// <param name="a_EndStationing"></param>
        [OperationContract]
        void UpdateSurvey(Guid a_SurveyGid, StationingInfo a_StartStationing, StationingInfo a_EndStationing);

        /// <summary>
        /// Получить все проезды ГРДО в текущей задаче
        /// </summary>
        [OperationContract]
        List<Survey> GetTaskSurveys(Guid a_TaskGid);

        /// <summary>
        /// Получение информации о заголовочном файле кэша ТЛС
        /// </summary>
        /// <param name="a_RootFileName"></param>
        /// <param name="a_LasBaseStatus"></param>
        /// <returns></returns>
        [OperationContract]
        PointCloudRoot GetLasRootInfo(string a_RootFileName, out LasSurveyHelper.LasBaseStatus a_LasBaseStatus
            /*, out double a_LasDirMbSize*/);

        /// <summary>
        /// Получить все проезды ГРДО в текущей задаче
        /// </summary>
        [OperationContract]
        List<GrdoData> GetTaskGrdo(Guid a_TaskGid);

        /// <summary>
        /// Получить список по границам участков пути
        /// </summary>
        [OperationContract]
        List<StationingCacheWaypartRange> GetStationingCacheWayparts(Guid a_MapFragmentGid, string a_AxisLinGid);

        /// <summary>
        /// Удалить все проезды ГРДО в текущей задаче
        /// </summary>
        [OperationContract]
        void DeleteRelatedTaskSurveys(Guid a_TaskGid);

        /// <summary>
        /// Добавить связь задачи и проезда
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <param name="StepGid"></param>
        /// <param name="a_SurveyGid"></param>
        [OperationContract]
        void AddSurveyToTask(Guid a_TaskGid, Guid StepGid, Guid a_SurveyGid);

        #endregion

        /// <summary>
        /// Добавить записи измерений
        /// </summary>
        /// <param name="a_Measures"></param>
        [OperationContract]
        string AddMeasures(List<Measure> a_Measures);

        #region Работа с георадаром

        /// <summary>
        /// Добавить новый георадар
        /// </summary>
        /// <param name="a_GeoradarData"></param>
        [OperationContract]
        string AddGeoradarData(GeoradarData a_GeoradarData);

        /// <summary>
        /// Добавить значения георадара
        /// </summary>
        /// <param name="a_GeoradarValue"></param>
        [OperationContract]
        string AddGeoradarValue(GeoradarValue a_GeoradarValue);

        /// <summary>
        /// Получить георадар по Survey Gid
        /// </summary>
        /// <param name="a_SurveyGid"></param>
        [OperationContract]
        List<GeoradarValue> GetGeoradarData(Guid a_SurveyGid);

        #endregion

        #region Работа с файлами

        /// <summary>
        /// Добавление одного хранилища
        /// </summary>
        /// <param name="a_VolumeInfo">Информация о томе</param>
        /// <param name="a_MinPriority">Минимально выставляемый приоритет (по умолчанию не ниже 0)</param>
        [OperationContract]
        Volume AddVolume(VolumeInfoClass a_VolumeInfo, int a_MinPriority = 0);

        /// <summary>
        /// Обновление хранилищ
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void UpdateVolumes(List<VolumeInfoClass> a_VolumeInfos);

        /// <summary>
        /// Получение списка хранилищ
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VolumeInfoClass> GetVolumesList(bool a_Actual = true);

        /// <summary>
        /// Проверка доступности каталога на сервере
        /// </summary>
        /// <param name="a_Path"></param>
        /// <param name="a_ReadOnly"></param>
        /// <returns></returns>
        [OperationContract]
        bool ServerPathExist(string a_Path, bool a_ReadOnly);


        /// <summary>
        /// Удаление файла с сервера
        /// </summary>
        /// <param name="a_VolumeGid">GID Тома</param>
        /// <param name="a_Volumes">Список томов</param>
        /// <param name="a_FileName">Имя файла</param>
        /// <param name="a_CheckReadOnly">Необходимость проверки тома на признак ReadOnly. Если true и том только для чтения, то выбрасываем ошибку</param>
        [OperationContract]
        void DelFileFromServer(Guid? a_VolumeGid, IEnumerable<Volume> a_Volumes, string a_FileName, bool a_CheckReadOnly = true);

        /// <summary>
        /// Получить файл с сервера
        /// </summary>
        /// <returns></returns>
        /// <param name="a_VolumeGid">GID Тома</param>
        /// <param name="a_FileName">Имя файла</param>
        [OperationContract]
        byte[] GetFileFromServer(Guid? a_VolumeGid, string a_FileName);

        /// <summary>
        /// Получить список путей с дорогами
        /// </summary>
        /// <returns></returns>
        /// <param name="a_FullFilePath">Полный путь к файлу на сервере</param>
        [OperationContract]
        byte[] GetFile(string a_FullFilePath);

        /// <summary>
        /// Получить лог-файлы с сервера (в виде архива)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetLogArchive();

        /// <summary>
        /// Получить лог-файлы с сервера ТЛО (в виде архива)
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetLogArchiveTlo();


        #endregion Работа с файлами

        #region Работа с запросами (модуль запросов)

        /// <summary>
        /// Выполнить SQL запрос
        /// </summary>
        /// <param name="a_Sql">Текст запроса</param>
        /// <param name="a_Params">Параметры</param>
        /// <param name="a_Range">Диапазон выборки</param>
        /// <returns></returns>
        [OperationContract]
        DataTable ExecuteQueryImmediate(string a_Sql, QueryExecuteParams a_Params, SearchSQLRange a_Range);

        /// <summary>
        /// Получить список запросов
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<QueryDO> GetQueries();

        #endregion

        #region Работа с Системами координат

        /// <summary>
        /// Получить все системы координат в системе
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CoordSys> GetAllCoordinateSystems();

       

        /// <summary>
        /// Получить список систем координат для трансформации
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CoordSys> GetTransformCoordinateSystems(Guid a_Guid);

        [OperationContract]
        byte[] GetTransformerAssembly(Guid a_FromCoordSysGid, Guid a_ToCoordSysGid);

        #endregion Работа с Системами координат 

        #region Работа с фотофиксацией

        /// <summary>
        /// Получаем GUID операционной системы сервера
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ServerMachineGuid();
        
        /// <summary>
        /// Список томов для записи фотофиксаций
        /// </summary>
        /// <param name="a_ExceptReadOnly">Исключая тома с признаком "только чтение"</param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<Volume> VolumesForPhotoFile(bool a_ExceptReadOnly=true);

        /// <summary>
        /// Перенос файла на сервере в указанный Volume
        ///  </summary>
        /// <param name="a_FileName">Передаваемое имя файла, возвращаемое вместе с родительской директорией</param>
        /// <param name="a_Volume">Том, где будет хранится файл</param>
        [OperationContract]
        string MoveFileOnServer(string a_FileName, Volume a_Volume);

        /// <summary>
        /// Копирование фотофиксации с клиента на сервер
        /// </summary> 
        /// <returns></returns>
        /// <param name="a_File">Файл для сохранения на сервере</param>
        /// <param name="a_FileName">Название файла</param>
        /// <param name="a_VolumeGid">Гид тома в который будем писать</param>
        /// <param name="a_Volumes">Список томов для записи</param>
        [OperationContract]
        PhotoFileInfo CopyPhotoFile(byte[] a_File, string a_FileName,Guid a_VolumeGid, IEnumerable<Volume> a_Volumes);

        #endregion

        #region Ведомость замечаний

        /// <summary>
        /// Получаем список замечаний
        /// </summary>
        /// <param name="a_TaskCheckGid"></param>
        /// <returns>список TaskCheckViolation</returns>
        [OperationContract]
        List<TaskCheckViolation> GetTaskCheckViolations(Guid a_TaskCheckGid);

 

        /// <summary>
        /// Получение списка ведомости замечаний
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <returns></returns>
        [OperationContract]
        List<TaskCheckViolationInfo> GetTasksCheckRows(Guid a_TaskGid);


        /// <summary>
        /// Возвращает TaskCheck по гиду
        /// </summary>
        /// <param name="a_TaskCheckGid"></param>
        /// <returns></returns>
        [OperationContract]
        TaskCheck GetTaskCheckByGid(Guid a_TaskCheckGid);

        /// <summary>
        /// Задача по гиду
        /// </summary>
        [OperationContract]
        Task GetTaskByGid(Guid a_TaskGid);

      

        /// <summary>
        /// Формирование сообщений о нарушении проверки
        /// </summary>
        /// <param name="a_TaskCheckGid"></param>
        /// <param name="a_TaskCheckViolations"></param>
        [OperationContract]
        void CreateTaskCheckViolationsByTaskCheckGid(Guid a_TaskCheckGid, List<TaskCheckViolation> a_TaskCheckViolations);

        /// <summary>
        /// Формирование сообщения о нарушении проверки
        /// </summary>
        /// <param name="a_TaskCheckGid"></param>
        /// <param name="a_TaskCheckViolation"></param>
        /// <returns>Guid? новой записи</returns>
        [OperationContract]
        Guid? CreateTaskCheckViolationByTaskCheckGid(Guid a_TaskCheckGid, TaskCheckViolation a_TaskCheckViolation);

        /// <summary>
        /// Очистка TaskCheckViolation по TaskCheckGid
        /// </summary>
        /// <param name="a_TaskCheckGid"></param>
        /// <param name="a_Key"></param>
        /// <param name="a_Metadata"></param>
        [OperationContract]
        void ClearTaskCheckViolation(Guid a_TaskCheckGid, string a_Key, string a_Metadata);

        /// <summary>
        /// Добавление замечания
        /// </summary>
        /// <param name="a_TaskCheckViolation"></param>
        /// <param name="a_TaskGid"></param>
        /// <param name="a_TaskStepGid"></param>        
        /// <returns></returns>
        [OperationContract]
        Guid CreateTaskCheckViolation(TaskCheckViolation a_TaskCheckViolation,
                                      Guid a_TaskGid,
                                      Guid a_TaskStepGid);

        /// <summary>
        /// Удаление замечания
        /// </summary>        
        [OperationContract]
        void DeleteTaskCheckViolation(Guid a_TaskCheckViolationGid);

        #endregion Ведомость замечаний

        #region Сдвижки между ПП и ИСП

        /// <summary>
        /// Определение дпты последнего расчета сдвижек
        /// </summary>
        /// <param name="a_Task"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime? GetLastAxisLineDistancesCalc(Task a_Task);

        #endregion

        #region Задачи (Tasks)

        /// <summary>
        /// Получение таблицы Поступивших задач
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TaskClass> GetTasksReceivedRows(int a_Operation);

        /// <summary>
        /// Получение таблицы Переданных задач
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TaskClass> GetTasksSentRows(int a_Operation);

        /// <summary>
        /// Создание работы и шага
        /// </summary>
        /// <param name="a_Task"></param>
        /// <param name="a_TaskStep"></param>
        [OperationContract]
        void CreateTaskAndTaskStep(Task a_Task, TaskStep a_TaskStep);

        /// <summary>
        /// Удаление ДКСС
        /// </summary>
        /// <param name="a_TaskGid"></param>
        [OperationContract]
        void DeleteTask(Guid a_TaskGid);

        /// <summary>
        /// Формирование записи о проверке
        /// </summary>
        /// <returns>GID созданной/найденной записи</returns>
        /// <param name="a_TaskGuid"> </param>
        /// <param name="a_TaskStepGuid"></param>
        /// <param name="a_Type"></param>
        /// <param name="a_ClearTask"></param> 
        [OperationContract]
        Guid? GetTaskCheckGid(Guid a_TaskGuid, Guid a_TaskStepGuid, String a_Type, bool a_ClearTask = true);

        /// <summary>
        /// Запрос данных из TaskCheck
        /// </summary>
        /// <returns>TaskCheck</returns>
        /// <param name="a_TaskGuid"> </param>
        /// <param name="a_Type"> </param>
        /// <param name="a_DateTime"></param>
        [OperationContract]
        Guid? GetTaskCheckGidForResult(Guid a_TaskGuid, string a_Type, out DateTime? a_DateTime);



        /// <summary>
        /// Получение Result из TaskCheck
        /// </summary>
        /// <param name="a_TaskCheckGid"> </param>
        [OperationContract]
        byte[] LoadResultFromTaskCheck(Guid a_TaskCheckGid);

        /// <summary>
        /// Получение Result из TaskData
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <param name="a_Type"></param>
        /// <param name="a_DateTime"> </param>
        [OperationContract]
        byte[] LoadResultFromTaskData(Guid a_TaskGid, /*Guid a_TaskStepGid,*/ string a_Type, /* int a_Operation,*/
                                      out DateTime? a_DateTime);

        /// <summary>
        /// Сохранение Result в TaskData
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <param name="a_TaskStepGid"></param>
        /// <param name="a_Type"></param>
        /// <param name="a_File"> </param>
        [OperationContract]
        DateTime? SaveResultToTaskData(Guid a_TaskGid, Guid a_TaskStepGid, string a_Type, byte[] a_File);

        /// <summary>
        /// Сохранение расчитанных данных в TaskCheck
        /// </summary>
        /// <param name="a_TaskCheckGid"> </param>
        /// <param name="a_File"> </param>
        [OperationContract]
        void SaveResultToTaskCheck(Guid a_TaskCheckGid, byte[] a_File);

        /// <summary>
        /// Создание файла отчета о замечаниях
        /// </summary>
        /// <param name="a_Task"></param>
        /// <param name="a_TaskGid"></param>
        /// <param name="a_TaskStepGid"></param>
        /// <param name="a_Type">Тип (строка)</param>
        /// <param name="a_User">Тек польз</param>
        /// <param name="a_ClsCode">Код классификатора</param>
        /// <param name="a_DocumentName">Наим документа</param>
        /// <param name="a_FileName">Наим файла (без пути)</param>
        /// <param name="a_File">Содерж файла</param>
        [OperationContract]
        void CreateDocumentToTask(Task a_Task, Guid a_TaskGid, Guid a_TaskStepGid,
                                  string a_Type, string a_User,
                                  string a_ClsCode, string a_DocumentName, string a_FileName, byte[] a_File);

        /// <summary>
        /// Определение даты последнего формирования ведомости нарушений
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <param name="a_Type"></param>
        /// <returns></returns>
        [OperationContract]
        DateTime? GetLastDocumentToTaskDate(Guid a_TaskGid, string a_Type);

        /// <summary>
        /// Алгоритм удаления проверок, связанных с проектом ЦМП
        /// </summary>
        /// <param name="a_DrmGid">GID проекта ЦМП</param>
        /// <param name="a_TaskGid">GID работы</param>
        [OperationContract]
        void DeleteTaskCheckViolations(Guid a_DrmGid, Guid a_TaskGid);

        #endregion

        #region Задачи (тех процессы и шаги)

        /// <summary>
        /// Получить задачу и текущий шаг
        /// </summary>
        /// <param name="a_TaskGuid"> </param>
        /// <param name="a_Task"></param>
        /// <param name="a_TaskStep"></param>
        [OperationContract]
        void GetTaskCurStep(Guid a_TaskGuid, out Task a_Task, out TaskStep a_TaskStep);

        /// <summary>
        /// Перевести задачу на след шаг
        /// </summary>
        /// <param name="a_TaskGuid">Ид задачи</param>
        /// <param name="a_NewOperation">Код нового шага</param>
        /// <param name="a_Resolution">Резолючия</param>
        /// <param name="a_User">Оператор</param>
        [OperationContract]
        void ShiftTask(Guid a_TaskGuid, short a_NewOperation, string a_Resolution, string a_User);

        /// <summary>
        /// Закрыть задачу
        /// </summary>
        /// <param name="a_TaskGuid"></param>
        [OperationContract]
        void CloseTask(Guid a_TaskGuid);

        /// <summary>
        /// Обновить задачу
        /// </summary>
        /// <param name="a_Task"></param>
        [OperationContract]
        void UpdateTask(Task a_Task);

        /// <summary>
        /// Получить список документов, входящих в работу
        /// </summary>
        /// <param name="a_TaskGid"></param>
        /// <returns></returns>
        [OperationContract]
        List<Document> GetTaskDocuments(Guid a_TaskGid);

        #endregion

        #region Результаты расчетов

        [OperationContract]
        List<CalcsResultInfoClass> GetCalcsList();

        [OperationContract]
        List<CalcDataInfoClass> GetCalcDataByObject(Guid a_CalcObjectGid);

        [OperationContract]
        List<CalcValue> GetCalcValuesByCalcData(Guid a_CalcDataGid);

        [OperationContract]
        List<CalcMessage> GetCalcMessagesByCalcGid(Guid a_CalcGid);

        [OperationContract]
        List<CalcDataClass> GetCalcDataByCalcGid(Guid a_CalcGid);

        [OperationContract]
        List<CalcChart> GetCalcChartsByCalcGid(Guid a_CalcGid);

        [OperationContract]
        List<CalcObject> GetCalcObjectByCalc(Guid a_CalcGid);

        [OperationContract]
        List<CalcAttr> GetCalcAttrsByCharts(List<CalcChart> a_Charts);

        [OperationContract]
        List<CalcAttr> GetCalcAttrsByCalcGid(Guid a_CalcGid);

        [OperationContract]
        Calc GetCalcByGid(Guid a_CalcGid);

        /// <summary>
        /// Получить СТРУКТУРУ и ДАННЫЕ расчета
        /// </summary>
        /// <param name="a_CalcGid"></param>
        /// <returns></returns>
        [OperationContract]
        Calc GetCalcComplexData(Guid a_CalcGid);

        #endregion

        #region Работа с документами

        /// <summary>
        /// Получить документ
        /// </summary>
        /// <param name="a_Gid"></param>
        /// <returns></returns>
        [OperationContract]
        Document GetDocument(Guid a_Gid);

        /// <summary>
        /// Получить все документы в проездах
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DocumentSurveyDTO> GetSurveyDocs();

        /// <summary>
        /// Получить все документы в работах
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DocumentTaskDTO> GetTaskDocs();

        /// <summary>
        /// Получить все документы в расчетах
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DocumentCalcDTO> GetCalcDocs();

        /// <summary>
        /// Получить прочие документы
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DocumentOtherDTO> GetOtherDocs();

        /// <summary>
        /// Получить все документы
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DocumentBaseDTO> GetAllDocs();

        #endregion

        #region Информация о сборках

        /// <summary>
        /// Получение информации о сборках
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetBuildsInfo();

        /// <summary>
        /// Получение информации о сборках сервера ТЛО
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetBuildsInfoTlo();

        #endregion

        #region Данные для контролов

        /// <summary>
        /// Получает данные для DrmProjectSelectionControl контрола
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DrmProjectSelectionControlData> Controls_GetDrmProjectSelectionControlData();

        #endregion Данные для контролов

        #region Габариты и нормативы

        [OperationContract]
        DataContracts.Specifications.Specifications GetSpecifications();

        #endregion Габариты и нормативы

        #region Логирование

        /// <summary>
        /// Записать событие в лог
        /// </summary>
        /// <param name="a_UserGid"></param>
        /// <param name="a_SessionId"></param>
        /// <param name="a_ActionName"></param>
        /// <param name="a_ClientGuid"></param>
        [OperationContract]
        void LogAction(Guid a_UserGid, string a_SessionId, string a_ActionName,
            Guid a_ClientGuid);


        /// <summary>
        /// Получить информацию о сессиях (логин-логаут)
        /// </summary>
        /// <param name="a_UserGid"></param>
        /// <param name="a_BeginDate"> </param>
        /// <param name="a_EndDate"> </param>
        /// <returns></returns>
        [OperationContract]
        UserSessionInfo[] GetUserSessionInfo(Guid a_UserGid, DateTime a_BeginDate, DateTime a_EndDate);
        
        /// <summary>
        /// Получить информацию о сессиях (логин-логаут)
        /// </summary>
        /// <param name="a_BeginDate"></param>
        /// <param name="a_EndDate"></param>
        /// <returns></returns>
        [OperationContract]
        UserSessionInfo[] GetAllUserSessionInfoByTime(DateTime a_BeginDate, DateTime a_EndDate);

        /// <summary>
        /// Получить мин. и макс. дату логина
        /// </summary>
        /// <param name="a_MinDate"></param>
        /// <param name="a_MaxDate"></param>
        [OperationContract]
        void GetMinMaxLoginDate(out DateTime? a_MinDate, out DateTime? a_MaxDate);

        /// <summary>
        /// Получить мин. и макс. дату логина пользователя
        /// </summary>
        /// <param name="a_UserGuid"></param>
        /// <param name="a_MinDate"></param>
        /// <param name="a_MaxDate"></param>
        [OperationContract]
        void GetMinMaxUserLoginDate(Guid a_UserGuid, out DateTime? a_MinDate, out DateTime? a_MaxDate);

        #endregion

        #region Распознавание плана, расчет уклонов. Запись результатов в базу
        /// <summary>
        /// Распознавание плана, расчет уклонов. Запись результатов в базу
        /// </summary>
        [OperationContract]
        string PlanProfileRecognizer(long a_AlignId);
        #endregion
        
        #region Запросы к серверу ТЛО

        [OperationContract]
        void TestSendDataMethod(byte [] a_Data);

        /// <summary>
        /// Получить изображение видимости пути
        /// </summary>
        /// <param name="a_SurveyGid">Gid проезда</param>
        /// <param name="a_EyePoint">Точка наблюдателя (на уровне оси пути)</param>
        /// <param name="a_TargetPoint">Точка цели (на уровне оси пути)</param>
        /// <param name="a_FarPlanDistance">Дистанция</param>
        /// <param name="a_ImageSize">Размер изображения в пикселях</param>
        /// <param name="a_BannerSize">Размер баннера в метрах</param>
        /// <param name="a_EyePointH">Уровень глаз водителя машиниста над осью пути (мм)</param>
        /// <returns></returns>
        [OperationContract]
        Bitmap GetProfileVisibilityBitmap(Guid a_SurveyGid, Point3D a_EyePoint, Point3D a_TargetPoint,
            double a_FarPlanDistance, Size a_ImageSize, Size? a_BannerSize, uint a_EyePointH);

        /// <summary>
        /// Получить изображение видимости пути
        /// </summary>
        /// <param name="a_SurveyGid"></param>
        /// <param name="a_EyePoint"></param>
        /// <param name="a_TargetPoint"></param>
        /// <param name="a_ImageSize"></param>
        /// <param name="a_QueryParams"></param>
        /// <param name="a_FilteringPolygon"></param>
        /// <param name="a_LasPointSize"></param>
        /// <param name="a_GeometryObjects"></param>
        /// <returns></returns>
        [OperationContract]
        Bitmap GetPointCloudBitmap(Guid a_SurveyGid, Point3D a_EyePoint, Point3D a_TargetPoint,
            Size a_ImageSize, PointsWithinPolygonParameters a_QueryParams,
            List<Point2D> a_FilteringPolygon, double a_LasPointSize, List<GeometryObject> a_GeometryObjects);

        /// <summary>
        /// Проверяет, является ли указанная директория облаком точек
        /// и доступна ли она с сервера ТЛО
        /// </summary>
        /// <param name="a_Directory">Директория с точками</param>
        /// <param name="a_AccessResultAppServer">Результат доступа на сервере приложений</param>
        /// <param name="a_AccessResultTloServer">Результат доступа на сервере ТЛО</param>
        /// <param name="a_PointCloudVersion">Версия облака</param>
        [OperationContract]
        void IsPointCloudAvailable(string a_Directory, out bool a_AccessResultAppServer,
            out bool? a_AccessResultTloServer, out int a_PointCloudVersion);

        #endregion

        #region IDataSourceAbstraction

        /// <summary>
        /// Получает список информации о протяженности объектов с учетом параметров
        /// </summary>
        /// <param name="a_MapFragmentGid">Карта на которой находится слой a_GeoLayer</param>
        /// <param name="a_GeoLayer">Слой(поле GEO_LAYER в бд)</param>
        /// <param name="a_ObjectIds">Список ObjectID(GEO_OBJECTID) </param>
        /// <returns></returns>
        [OperationContract]
        List<ObjectsPositionInfo> GetObjectsPositionForLayer(Guid a_MapFragmentGid, string a_GeoLayer, List<long> a_ObjectIds);

        #endregion IDataSourceAbstraction

    }
}