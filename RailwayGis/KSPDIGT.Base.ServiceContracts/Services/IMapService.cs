using System;
using System.Collections.Generic;
using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.DataModels.Model;
using KSPDIGT.Base.DataContracts.GeoObjects;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    [ServiceContract]
    public interface IMapService : IWcfService
    {
        #region Работа с пикетажом

        /// <summary>
        /// Возвращает объект кэша пикетажа для осевой линии по <paramref name="a_MapFragmentGid"/>
        /// и <paramref name="a_AxisLineGid"/>
        /// </summary>
        /// <param name="a_MapFragmentGid">Gid фрагмента карты</param>
        /// <param name="a_AxisLineGid">GID осевой линии</param>
        /// <returns></returns>
        [OperationContract]
        MapFragmentAxisLine GetStationingCacheForAxisLine(Guid a_MapFragmentGid, string a_AxisLineGid);

        /// <summary>
        /// Возвращает объект кэша пикетажа для осевой линии из MapFragmentAxisLine по <paramref name="a_MapFragmentAxisLineGid"/>
        /// </summary>
        /// <param name="a_MapFragmentAxisLineGid">GID осевой линии </param>
        /// <returns></returns>
        [OperationContract]
        MapFragmentAxisLine GetStationingCacheForMapFragmentAxisLine(Guid a_MapFragmentAxisLineGid);

        /// <summary>
        /// Удаление КЭШа пикетажа указанной карты
        /// </summary>
        /// <param name="a_MapFragmentGid"></param>
        [OperationContract]
        void RemoveStationingCache(Guid a_MapFragmentGid);

        /// <summary>
        /// Добавление записи о пикетаже объекта
        /// </summary>
        /// <param name="a_StationingCacheInfo"></param>
        /// <returns></returns>
        [OperationContract]
        Guid AddStationingCache(StationingCacheObjectPos a_StationingCacheInfo);

        [OperationContract]
        void AddStationingCaches(List<StationingCacheObjectPos> a_StationingCacheInfos);

        [OperationContract]
        MapFragmentAxisLine GetMapFragmentAxisLineByGids(string a_Gids);

        [OperationContract]
        List<MapFragmentAxisLine> GetAllMapFragmentAxisLines(Guid? a_MapFragmentGuid = null);

        #endregion Работа с пикетажом

        /// <summary>
        /// Получить у указанной записи в ГБД (по ее идентификатору и имени таблицы) все атрибуты
        /// </summary>
        /// <param name="a_MapFragmentGuid"></param>
        /// <param name="a_FeatureIdentifier"></param>
        /// <returns></returns>
        [OperationContract]
        List<KeyValuePair<string,object>> GetFeatureAttributes(Guid a_MapFragmentGuid,  FeatureIdentifier a_FeatureIdentifier);
    }
}
