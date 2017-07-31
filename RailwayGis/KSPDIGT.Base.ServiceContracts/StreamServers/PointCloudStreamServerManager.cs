using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Intech.Common.Exceptions;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.PointCloud;
using KSPDIGT.Base.DataContracts.StreamServers.StreamServerProtocol;

namespace KSPDIGT.Base.ServiceContracts.StreamServers
{
    /// <summary>
    /// Операции взаимодействия с PointCloudStreamServer
    /// </summary>
    public class PointCloudStreamServerManager
    {
        private PointCloudStreamServerManager()
        {
        }

        private static PointCloudStreamServerManager m_Instance;
        private string m_Host;
        private int m_Port;

        public static void Initialize(string a_Host, int a_Port)
        {
            if (m_Instance!=null)
                throw new AlgorithmException("Повторный вызов инициализации менеджера");

            m_Instance = new PointCloudStreamServerManager();
            m_Instance.m_Host = a_Host;
            m_Instance.m_Port = a_Port;
        }

        public static PointCloudStreamServerManager Current
        {
            get
            {
                if (m_Instance==null)
                    throw new InvalidOperationException("Менеджер не инициализирован");
                return m_Instance;
            }
        }


        /// <summary>
        /// Выполнить команду PointCloudStreamServer. Какую именно команду выполнять - указывается в виде Callback-функции
        /// </summary>
        /// <param name="a_NetworkStreamAction"></param>
        private void ExecutePointCloudStreamServerCommand(Action<Stream> a_NetworkStreamAction)
        {
            using (TcpClient a_Client = new TcpClient(m_Instance.m_Host, m_Instance.m_Port)) 
            {
                a_Client.ReceiveBufferSize = CommonStreamServerContracts.SocketBufferSize;
                //a_Client.NoDelay
                using (NetworkStream a_Stream = a_Client.GetStream())
                    a_NetworkStreamAction(a_Stream);
            }
        }

        /// <summary>
        /// Команда PointCloudStreamServer "выбрать точки из облака"
        /// </summary>
        /// <param name="a_Connection"></param>
        /// <param name="a_Polygon"></param>
        /// <param name="a_AdditionalParameters"></param>
        /// <param name="a_Delegate"></param>
        /// <returns></returns>
        private void QueryPoints(string a_Connection, IEnumerable<Point2D> a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Action<CloudPointCluster> a_Delegate)
        {
            ExecutePointCloudStreamServerCommand(a_Stream => PointCloudStreamServerContracts.QueryPoints(a_Connection, a_Polygon.ToArray(), a_AdditionalParameters, a_Stream,a_Delegate));
        }

        public void QueryPoints(string a_Connection, IEnumerable<Point2D> a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Action<byte[]> a_Delegate)
        {
            ExecutePointCloudStreamServerCommand(a_Stream => PointCloudStreamServerContracts.QueryPoints(a_Connection, a_Polygon.ToArray(), a_AdditionalParameters, a_Stream, a_Delegate));
        }

        public List<CloudPointCluster> QueryPoints(string a_Connection, List<Point2D> a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters)
        {
            List<CloudPointCluster> a_Result = new List<CloudPointCluster>();
            QueryPoints(a_Connection, a_Polygon, a_AdditionalParameters, delegate(CloudPointCluster a_Cluster)
            {
                a_Result.Add(a_Cluster);
            });

            return a_Result;
        }

        
    }
}