using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.PointCloud;

namespace KSPDIGT.Base.DataContracts.StreamServers.StreamServerProtocol
{
    /// <summary>
    /// Структуры для работы с StreamServer сервера КСПД (TCP сервер для передачи большого объема информации)
    /// </summary>
    public class KspdGeneralStreamServerContracts: CommonStreamServerContracts
    {
//        public const ushort TestPort = 3333; //TODO вынести в настройки

        public enum Command : byte
        {
            /// <summary>
            /// Получить точки непосредственно из указанного облака
            /// </summary>
            QueryPointsByConnectionString = 0,
            
            /// <summary>
            /// Получить точки по указанному проезду
            /// </summary>
            QueryPointsBySurveyGid = 1,

            ///// <summary>
            ///// Передать на сервер файл
            ///// </summary>
            //UploadFile=2
        }

        [DataContract]
        public class QueryPointsByConnectionStringCommandParams
        {
            [DataMember]
            public string CachePath;
            [DataMember]
            public Point2D[] Polygon;
            [DataMember]
            public PointsWithinPolygonParameters Params;

            public QueryPointsByConnectionStringCommandParams()
            {
            }

            public QueryPointsByConnectionStringCommandParams(Point2D[] a_Polygon, string a_CachePath, PointsWithinPolygonParameters a_Params)
            {
                this.Polygon = a_Polygon;
                CachePath = a_CachePath;
                Params = a_Params;
            }
        }


        [DataContract]
        public class QueryPointsBySurveyGidCommandParams
        {
            [DataMember]
            public Guid SurveyGid;
            [DataMember]
            public Point2D[] Polygon;
            [DataMember]
            public PointsWithinPolygonParameters Params;

            public QueryPointsBySurveyGidCommandParams()
            {
            }

            public QueryPointsBySurveyGidCommandParams(Point2D[] a_Polygon, Guid a_SurveyGid, PointsWithinPolygonParameters a_Params)
            {
                this.Polygon = a_Polygon;
                SurveyGid = a_SurveyGid;
                Params = a_Params;
            }
        }

        [DataContract]
        public class UploadFileCommandParams
        {
            [DataMember]
            public Guid ? VolumeGid;

            public UploadFileCommandParams()
            {
            }


            public UploadFileCommandParams(Guid ?a_VolumeGid)
            {
                VolumeGid = a_VolumeGid;
            }
        }


        /// <summary>
        /// Команда получения точек в указанном облаке (строка подключения) по указанному полигону
        /// </summary>
        /// <param name="a_AdditionalParameters">Дополнительные параметры</param>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_Connection">Строка подключения к облаку</param>
        /// <param name="a_Polygon">Полигон выборки</param>
        /// <param name="a_Callback"></param>
        /// <returns></returns>
        public static void QueryPoints(string a_Connection, Point2D[] a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Stream a_Stream, Action<CloudPointCluster> a_Callback)
        {
            WriteCommandToStream(a_Stream, (byte)Command.QueryPointsByConnectionString,new QueryPointsByConnectionStringCommandParams(a_Polygon, a_Connection, a_AdditionalParameters));
            ReadPointsFromStream(a_Stream, a_Callback);
        }


        /// <summary>
        /// Команда получения точек в указанном облаке (GUID проезда в БД) по указанному полигону
        /// </summary>
        /// <param name="a_AdditionalParameters">Дополнительные параметры</param>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_SurveyGid">Строка подключения к облаку</param>
        /// <param name="a_Polygon">Полигон выборки</param>
        /// <param name="a_Callback"></param>
        /// <returns></returns>
        public static void QueryPoints(Guid a_SurveyGid, Point2D[] a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Stream a_Stream, Action<CloudPointCluster> a_Callback)
        {
            WriteCommandToStream(a_Stream, (byte)Command.QueryPointsBySurveyGid, new QueryPointsBySurveyGidCommandParams(a_Polygon, a_SurveyGid, a_AdditionalParameters));
            ReadPointsFromStream(a_Stream, a_Callback);
        }

        ///// <summary>
        ///// Команда получения точек в указанном облаке (GUID проезда в БД) по указанному полигону
        ///// </summary>
        ///// <returns></returns>
        //public static void UploadFiles(Guid? a_VolumeGid, Stream a_Stream, IEnumerable<string> a_FilePathes)
        //{
        //    WriteCommandToStream(a_Stream, (byte)Command.UploadFile, new UploadFileCommandParams(a_VolumeGid));
            
        //    using (BinaryWriter a_Writer = new BinaryWriter(a_Stream, Encoding.Default, true))
        //        //поток для отправки данных
        //    {
        //        foreach (var a_FilePath in a_FilePathes)
        //        {

        //            //todo
        //            //using (var a_FileStream = new FileStream(a_FilePath, FileMode.Open, FileAccess.Read))
        //            //{
        //            //    a_FileStream.Read()
        //            // }

        //            var a_FileContent = File.ReadAllBytes(a_FilePath);
        //            a_Writer.Write(CommonStreamServerContracts.BeginMarker);
        //            a_Writer.Write(a_FileContent, 0, a_FileContent.Length);
        //        }

        //        a_Writer.Write(CommonStreamServerContracts.EndMarker);
        //    }



        //    //while (a_Size>0)
        //    //{
        //    //    var a_Buffer = a_WriteDelegate();
        //    //    a_Stream.Write(a_Buffer,0,a_Buffer.Length);
        //    //    a_Size -= a_Buffer.Length;
        //    //}
        //}
    }
}
