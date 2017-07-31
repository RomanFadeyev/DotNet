using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.PointCloud;
using KSPDIGT.Base.DataContracts.StreamServers.StreamServerProtocol;

namespace KSPDIGT.Base.ServiceContracts.StreamServers
{
    /// <summary>
    /// Менеджер доступа к потоковому сетевому серверу в AppServer
    /// </summary>
    public class KspdGeneralStreamServerManager
    {
        protected KspdGeneralStreamServerManager()
        {
        }

        private static KspdGeneralStreamServerManager m_Instance;
        private string m_Host;
        private int m_Port;

        public static void Initialize(string a_Host, int a_Port)
        {
//            if (m_Instance!=null)
//                throw new AlgorithmException("Повторный вызов инициализации менеджера");
            
            m_Instance = new KspdGeneralStreamServerManager();
            m_Instance.m_Host = a_Host;
            m_Instance.m_Port = a_Port;
        }
        
        public static KspdGeneralStreamServerManager Current
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
        private void ExecuteStreamServerCommand(Action<Stream> a_NetworkStreamAction)
        {
            using (TcpClient a_Client = new TcpClient(m_Host, m_Port)) 
            {
                a_Client.ReceiveBufferSize = CommonStreamServerContracts.SocketBufferSize;
                //a_Client.NoDelay = true;
                using (NetworkStream a_Stream = a_Client.GetStream())
                    a_NetworkStreamAction(a_Stream);
            }
        }


        /// <summary>
        /// Команда PointCloudStreamServer "выбрать точки из облака по строке подключения"
        /// </summary>
        /// <param name="a_Connection"></param>
        /// <param name="a_Polygon"></param>
        /// <param name="a_AdditionalParameters"></param>
        /// <param name="a_Delegate"></param>
        /// <returns></returns>
        public void QueryPoints(string a_Connection, IEnumerable<Point2D> a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Action<CloudPointCluster> a_Delegate)
        {
            ExecuteStreamServerCommand(a_Stream => KspdGeneralStreamServerContracts.QueryPoints(a_Connection, a_Polygon.ToArray(), a_AdditionalParameters, a_Stream, a_Delegate));
        }

        /// <summary>
        /// Команда "выбрать точки из облака по Gid проезда"
        /// </summary>
        /// <param name="a_SurveyGuid"></param>
        /// <param name="a_Polygon"></param>
        /// <param name="a_AdditionalParameters"></param>
        /// <param name="a_Delegate"></param>
        /// <returns></returns>
        public void QueryPoints(Guid a_SurveyGuid, IEnumerable<Point2D> a_Polygon, PointsWithinPolygonParameters a_AdditionalParameters, Action<CloudPointCluster> a_Delegate)
        {
            ExecuteStreamServerCommand(a_Stream => KspdGeneralStreamServerContracts.QueryPoints(a_SurveyGuid, a_Polygon.ToArray(), a_AdditionalParameters, a_Stream, a_Delegate));
        }
    }
}
