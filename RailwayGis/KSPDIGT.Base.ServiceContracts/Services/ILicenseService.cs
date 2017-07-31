using System;
using System.Collections.Generic;
using System.ServiceModel;
using Intech.Common.Wcf;

namespace KSPDIGT.License.DataContract
{
    /// <summary>
    /// Интерфейс WCF службы для функций обновления
    /// </summary>
    [ServiceContract]
    public interface ILicenseService : IWcfService
    {
        /// <summary>
        /// Очистить клиентские подключения
        /// </summary>
        /// <param name="a_LicenseKey">Ключ лицензии</param>
        /// <param name="a_ServerGid">GUID сервера приложений</param>
        [OperationContract]
        void ClearClientConnections(string a_LicenseKey, Guid a_ServerGid);

        /// <summary>
        /// Зарегистрировать клиентское подключение
        /// </summary>
        /// <param name="a_LicenseKey">ключ лицензии</param>
        /// <param name="a_ServerGid">GUID сервера приложений</param>
        /// <param name="a_IdClentConnection">Id клиентского подключения (у сервера приложений)</param>
        /// <param name="a_Version">Версия клиента</param>
        /// <param name="a_ArmName">Наименование клиентского рабочего места</param>
        /// <param name="a_LicenserName">Сведения о лицензиаре</param>
        [OperationContract]
        List<string> RegisterClientConnection(string a_LicenseKey, Guid a_ServerGid, string a_IdClentConnection, string a_Version,
            out string a_ArmName, out string a_LicenserName);

        /// <summary>
        /// Удалить клиентское подключение
        /// </summary>
        /// <param name="a_LicenseKey">ключ лицензии</param>
        /// <param name="a_ServerGid">GUID сервера приложений</param>
        /// <param name="a_IdClentConnection">Id клиентского подключения (у сервера приложений)</param>
        /// <param name="a_ClientDisconnectType">Режим отключения</param>
        [OperationContract]
        void RemoveClientConnection(string a_LicenseKey, Guid a_ServerGid, string a_IdClentConnection, ClientInfo.ClientDisconnectTypeEnum a_ClientDisconnectType);

        /// <summary>
        /// Получить список запрещенных операций
        /// </summary>
        /// <param name="a_LicenseKey">ключ лицензии</param>
        /// <param name="a_ServerGid">GUID сервера приложений</param>
        [OperationContract]
        void GetProhibitedTransactions(string a_LicenseKey, Guid a_ServerGid);
    }
}
