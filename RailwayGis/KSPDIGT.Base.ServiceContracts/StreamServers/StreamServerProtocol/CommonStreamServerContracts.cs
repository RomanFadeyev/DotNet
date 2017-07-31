using System;
using System.IO;
using Intech.Common.Utils;
using KSPDIGT.Base.Common.Exceptions;
using KSPDIGT.Base.Common.StreamServer;
using KSPDIGT.Base.DataContracts.PointCloud;

namespace KSPDIGT.Base.DataContracts.StreamServers.StreamServerProtocol
{
    public class CommonStreamServerContracts
    {
        public const int SocketBufferSize = 1024 * 64;
        public const int BeginMarker = 0x12345678;
        public const int EndMarker = 0x12345679;

        /// <summary>
        /// Реализация протокола чтения точек из потока. На выходе - готовый CloudPointCluster
        /// </summary>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_Callback"></param>
        /// <returns></returns>
        protected static void ReadPointsFromStream(Stream a_Stream, Action<CloudPointCluster> a_Callback)
        {
            ReadPointsFromStream(a_Stream, delegate(byte[] a_Bytes)
            {
                using (var a_MemoryStream = new MemoryStream(a_Bytes))
                using (var a_Reader = new BinaryReader(a_MemoryStream))
                {
                    CloudPointCluster a_PointCluster = new CloudPointCluster();
                    a_PointCluster.DeserializeBinary(a_Reader);
                    a_Callback(a_PointCluster);
                }

            });
        }

        /// <summary>
        /// Реализация протокола чтения точек из потока. На выходе - массив байтов, который можно десериализовать в CloudPointCluster
        /// </summary>
        /// <param name="a_Stream">Сетевой поток</param>
        /// <param name="a_Callback"></param>
        /// <returns></returns>
        protected static void ReadPointsFromStream(Stream a_Stream, Action<byte[]> a_Callback)
        {
            using (BinaryReader a_Reader = new BinaryReader(a_Stream))
            {
                while (true)
                {
                    //1. Флаг начала пакета
                    int a_Flag = a_Reader.ReadInt32();
                    if (a_Flag == EndMarker)
                        break;

                    if (a_Flag == BaseStreamServer.ErrorMarker)
                    {
                        //Размер
                        int a_ErrorSize = a_Reader.ReadInt32();
                        //Сама ошибка
                        var a_ErrorBytes = a_Reader.ReadBytes(a_ErrorSize);

                        var a_ExceptionInfo = DataContractUtils.DeserializeFromBytes<BaseStreamServer.ExceptionInfo>(a_ErrorBytes);
//                      
                        // создать Exception с CallStack из a_ExceptionInfo
                        BaseStreamServer.StreamServerException a_Exception =
                            new BaseStreamServer.StreamServerException("Ошибка при чтении ТЛО", a_ExceptionInfo);

                        throw a_Exception;
                    }

                    if (a_Flag != BeginMarker)
                        throw new Exception("Неверный пакет");
                    
                    //Размер кластера
                    int a_ClusterSize = a_Reader.ReadInt32();
                    //Сам кластер
                    var a_Bytes = a_Reader.ReadBytes(a_ClusterSize);
                    a_Callback(a_Bytes);
                }
            }
        }

        public static void PushCluster(BinaryWriter a_Writer, CloudPointCluster a_Cluster)
        {
            //Пустой кластер не пишем
            if (a_Cluster.Points.Count == 0)
                return;

            a_Writer.Write(BeginMarker);
            using (MemoryStream a_MemoryStream = new MemoryStream())
            {
                using (BinaryWriter a_MemWriter = new BinaryWriter(a_MemoryStream))
                {
                    //Сериализуем кластер в байты
                    a_Cluster.SerializeBinary(a_MemWriter);
                    a_MemWriter.Flush();

                    int a_Size = (int) a_MemoryStream.Position;

                    //Записываем размер кластера
                    a_Writer.Write(a_Size);

                    //Записываем сам пакет в виде байтов
                    a_Writer.Write(a_MemoryStream.GetBuffer(), 0, a_Size);
                }
            }
        }

        public static void WriteCommandToStream<T>(Stream a_Stream, byte a_CommandSignature, T a_CommandParams) where T : class
        {
            BinaryWriter a_Writer = new BinaryWriter(a_Stream); //поток для отправки данных
            a_Writer.Write(a_CommandSignature); //Команда
            var a_CommandParamsAsBytes = DataContractUtils.SerializeToBytes(a_CommandParams);
            a_Writer.Write(a_CommandParamsAsBytes.Length); //Размер параметров
            a_Writer.Write(a_CommandParamsAsBytes); //Сами параметры
        }
    }
}
