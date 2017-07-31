using System;
using Intech.Common.Wcf;
using KSPDIGT.License.DataContract;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    public abstract class ConnectionPoint<T>: IServiceConnectionPoint<T> where T:IWcfService
    {
        private Uri m_Uri;

        protected static string FormatUri(string a_Host,int a_DefaultPort, string a_ServType) 
        {
            if (a_Host.IndexOf(':') == -1)
                a_Host += ":"+a_DefaultPort.ToString();

            Type a_ServiceType = typeof (T);
            string a_ServiceName = a_ServiceType.Name.Remove(0, 1);
            string a_Uri = string.Format(@"net.tcp://{0}/KSPDIGT.{1}/{2}/basic", a_Host,a_ServType, a_ServiceName);
            return a_Uri;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected ConnectionPoint(string a_Uri)
        {
            m_Uri = new Uri(a_Uri);
        }

        public Uri Uri {
            get { return m_Uri; }
        }
    
    }

    /// <summary>
    /// Подключения к сервисам сервера КСПД
    /// </summary>
    public static class AppServerConnectionPoints
    {
        public class ConnectionPointHostedInAppServer<T> : ConnectionPoint<T> where T : IWcfService
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            protected ConnectionPointHostedInAppServer(string a_HostName)
                : base(FormatUri(a_HostName, 8097, "Server"))
            {
            }
        }

        /// <summary>
        /// Подключение к сервису <see cref="IKSPDGeneralService"/>
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public class KSPDGeneralService : ConnectionPointHostedInAppServer<IKSPDGeneralService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public KSPDGeneralService(string a_HostName) : base(a_HostName)
            {
            }

        }

        /// <summary>
        /// Подключение к сервису <see cref="IAuthentificationService"/>
        /// </summary>
        public class AuthentificationService : ConnectionPointHostedInAppServer<IAuthentificationService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public AuthentificationService(string a_HostName) : base(a_HostName)
            {
            }

        }

        /// <summary>
        /// Подключение к сервису <see cref="IMapService"/>
        /// </summary>
        public class MapService : ConnectionPointHostedInAppServer<IMapService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public MapService(string a_HostName)
                : base(a_HostName)
            {
            }

        }

        /// <summary>
        /// Подключение к сервису<see cref="ISizeCheckService"/>
        /// </summary>
        public class SizeCheckService : ConnectionPointHostedInAppServer<ISizeCheckService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public SizeCheckService(string a_HostName)
                : base(a_HostName)
            {
            }

        }


        /// <summary>
        /// Подключение к <see cref="ICalculationService"/>
        /// </summary>
        public class CalculationService: ConnectionPointHostedInAppServer<ICalculationService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public CalculationService(string a_HostName)
                : base(a_HostName)
            {
            }

        }

        /// <summary>
        /// Подключение к  сервису <see cref="IProcessWithCallbackService"/>
        /// </summary>
        public class ProcessWithCallbackService : ConnectionPointHostedInAppServer<IProcessWithCallbackService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public ProcessWithCallbackService(string a_HostName)
                : base(a_HostName)
            {
            }

        }
    }

    /// <summary>
    /// Подключения к сервисам сервера ТЛС
    /// </summary>
    public class PointCloudServerConnectionPoints
    {

        /// <summary>
        /// Подключение к сервису точек
        /// </summary>
        public class PointCloudService : ConnectionPoint<IPointCloudService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public PointCloudService(string a_HostName)
                : base(FormatUri(a_HostName, 8091, "PointCloudServer"))
            {
            }
        }
    }

    /// <summary>
    /// Подключения к сервисам сервера лицензий
    /// </summary>
    public static class LicenseServerConnectionPoints
    {
        /// <summary>
        /// Подключение к сервису ILicenseService
        /// </summary>
        public class LicenseService : ConnectionPoint<ILicenseService>
        {
            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            public LicenseService(string a_HostName)
                : base(FormatUri(a_HostName, 8098, "License"))
            {
            }
        }
    }

}
