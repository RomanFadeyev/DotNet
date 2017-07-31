using System;
using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.Common.Enums;
using KSPDIGT.Base.DataContracts.Authorization;
using KSPDIGT.Base.DataContracts.Exceptions;

namespace KSPDIGT.Base.ServiceContracts.Services
{

    /// <summary>
    /// Сервис аутентификации пользователя в КСПД ИЖТ 
    /// </summary>
    [ServiceContract()]
    public interface IAuthentificationService : IWcfService
    {
        /// <summary>
        /// Осуществляет попытку аутентификации пользователя
        /// </summary>
        /// <param name="a_Login">Имя</param>
        /// <param name="a_Password">Пароль</param>
        /// <param name="a_Version"> Версия клиентской программы </param>
        /// <param name="a_ClientApplicationGuid"> </param>
        /// <returns>Всегда возвращает значение. В случае неуспешной попытки выбрасывает исключение AuthorizationException</returns>
        /// <exception cref="AuthorizationException"></exception>>
        [OperationContract]
        AuthorizedUserInfo Login(string a_Login, string a_Password, string a_Version, Guid a_ClientApplicationGuid);

        /// <summary>
        /// Завершает сеанс пользователя
        /// </summary>
        /// <param name="a_Ticket">Тикет сеанса</param>
        [OperationContract]
        void Logout(string a_Ticket);

        /// <summary>
        /// Возвращает информацию о пользователе 
        /// </summary>
        /// <param name="a_Ticket">Тикет сеанса</param>
        /// <returns>Всегда возвращает значение. В случае неуспешной попытке в информации о пользователе будет описание ошибки</returns>
        [OperationContract]
        AuthorizedUserInfo GetLoggedUserInfo(string a_Ticket);

        /// <summary>
        /// Обновить пароль пользователя
        /// </summary>
        /// <param name="a_UserGid">Gid пользователя</param>
        /// <param name="a_NewPasswordMd5">MD5 пароля</param>
        [OperationContract]
        void UpdatePassword(Guid a_UserGid, string a_NewPasswordMd5);

        /// <summary>
        /// Клиент сообщает серверу, что он еще работает (не завис и не отсоеденился)
        /// </summary>
        [OperationContract]
        void IamAlive();

        /// <summary>
        /// Получить версию файл инсталяции
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetAppServerVersion();

        /// <summary>
        /// Получить файл инсталяции (для текущего АРМ)
        /// </summary>
        /// <param name="a_ArmType">Тип АРМ</param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetUpdateInstallFile(ArmType a_ArmType);

        /// <summary>
        /// Получить MD5 файла инсталяции (для текущего АРМ)
        /// </summary>
        /// <param name="a_ArmType">Тип АРМ</param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetUpdateInstallFileMd5(ArmType a_ArmType);
    }
}
