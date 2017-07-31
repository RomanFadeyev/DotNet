using System;
using System.IO;
using System.Runtime.Serialization;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.PointCloud;

namespace KSPDIGT.Base.DataContracts.StreamServers.StreamServerProtocol
{
    /// <summary>
    /// Структуры для работы с PointCloudStreamServer (TCP сервер для передачи большого объема информации)
    /// </summary>
    public class PointCloudStreamServerContracts: CommonStreamServerContracts
    {
//        public const ushort TestPort = 1234; //TODO вынести в настройки
        
        /// <summary>
        /// Тип команды
        /// </summary>
        public enum Command : byte
        {
            /// <summary>
            /// Получить ТЛО
            /// </summary>
            QueryPoints=0
        }

        [DataContract]
        public class QueryPointsCommandParams
        {
            [DataMember]
            public string CachePath;
            [DataMember]
            public Point2D[] Polygon;
            [DataMember]
            public PointsWithinPolygonParameters Params;

            public QueryPointsCommandParams()
            {
            }

            public QueryPointsCommandParams(Point2D[] a_Polygon, string a_CachePath, PointsWithinPolygonParameters a_Params)
            {
                this.Polygon = a_Polygon;
                CachePath = a_CachePath;
                Params = a_Params;
            }
        }

        /// <summary>
        /// Команда получения точек в указанном облаке по указанному полигону
        /// </summary>
        /// <param name="a_AdditionalParameters">Дополнительные параметры</param>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_Connection">Строка подключения к облаку</param>
        /// <param name="a_Polygon">Полигон выборки</param>
        /// <param name="a_Callback">Делегат для получения результатов в виде нераспакованного массива байт</param>
        /// <returns></returns>
        public static void QueryPoints(string a_Connection, Point2D[] a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Stream a_Stream, Action<CloudPointCluster> a_Callback)
        {
            WriteCommandToStream(a_Stream, (byte)Command.QueryPoints, new QueryPointsCommandParams(a_Polygon, a_Connection, a_AdditionalParameters));
            ReadPointsFromStream(a_Stream,a_Callback);
        }

        /// <summary>
        /// Команда получения точек в указанном облаке по указанному полигону
        /// </summary>
        /// <param name="a_AdditionalParameters">Дополнительные параметры</param>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_Connection">Строка подключения к облаку</param>
        /// <param name="a_Polygon">Полигон выборки</param>
        /// <param name="a_Callback">Делегат для получения результатов в виде нераспакованного массива байт</param>
        /// <returns></returns>
        public static void QueryPoints(string a_Connection, Point2D[] a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Stream a_Stream, Action<byte[]> a_Callback)
        {
            WriteCommandToStream(a_Stream, (byte)Command.QueryPoints, new QueryPointsCommandParams(a_Polygon, a_Connection, a_AdditionalParameters));
            ReadPointsFromStream(a_Stream, a_Callback);
        }
    }
}
