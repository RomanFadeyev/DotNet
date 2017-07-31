using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.SizeCheck;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    /// <summary>
    /// Обратный интерфейс для информирования клиента об промежуточных результатах или о конце полной обработки
    /// </summary>
    public interface ISizeCheckCallback
    {
        [OperationContract(IsOneWay = true)]
        void StartParametersCalculated(SizeCheckStartParameters a_StartParameters);
        
        [OperationContract(IsOneWay = true)]
        void OneSliceCheckFinished(SizeCheckOneSliceData a_OneSliceData);

        [OperationContract(IsOneWay = true)]
        void FullCheckFinished();

        [OperationContract(IsOneWay = true)]
        void ErrorOccurred(ExceptionDetail a_Exception);
    }

    /// <summary>
    /// Сервис проверки габаритов
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISizeCheckCallback))]
    public interface ISizeCheckService : IWcfService
    {
        [OperationContract(IsOneWay = true)]
        void CheckSSize(SizeCheckParameters a_Parameters);

        [OperationContract(IsOneWay = true)]
        void CancelCheck();
    }

    #region Обработчик

    /// <summary>
    /// Релизация обратного интерфейса для информирования клиента об промежуточных результатах или о конце полной обработки
    /// </summary>
    public class SizeCheckCallbackHandler : ISizeCheckCallback
    {
        #region Типы

        /// <summary>
        /// Делегат используемый в StartParametersCalculated
        /// </summary>
        /// <param name="a_StartParameters"></param>
        public delegate void OnStartParametersCalculated(SizeCheckStartParameters a_StartParameters);

        /// <summary>
        /// Делегат используемый в OneSliceCheckFinished
        /// </summary>
        /// <param name="a_OneSliceData"></param>
        public delegate void OnOneSliceCheckFinished(SizeCheckOneSliceData a_OneSliceData);

        /// <summary>
        /// Делегат используемый в FullCheckFinished
        /// </summary>
        public delegate void OnFullCheckFinished();

        /// <summary>
        /// Делегат используемый в ErrorOccurred
        /// </summary>
        /// <param name="a_Exception"></param>
        public delegate void OnErrorOccurred(ExceptionDetail a_Exception);

        #endregion Типы

        #region Имплементация

        void ISizeCheckCallback.StartParametersCalculated(SizeCheckStartParameters a_StartParameters)
        {
            if (this.m_OnStartParametersCalculated != null) this.m_OnStartParametersCalculated(a_StartParameters);
        }

        void ISizeCheckCallback.OneSliceCheckFinished(SizeCheckOneSliceData a_OneSliceData)
        {
            if (this.m_OnOneSliceCheckFinished != null)
            {
                this.m_OnOneSliceCheckFinished(a_OneSliceData);
            }
        }

        void ISizeCheckCallback.FullCheckFinished()
        {
            if (this.m_OnFullCheckFinished != null) this.m_OnFullCheckFinished();
        }

        void ISizeCheckCallback.ErrorOccurred(ExceptionDetail a_Exception)
        {
            if (this.m_OnErrorOccurred != null) this.m_OnErrorOccurred(a_Exception);
        }

        #endregion Имплементация

        private readonly OnStartParametersCalculated m_OnStartParametersCalculated = null;

        private readonly OnOneSliceCheckFinished m_OnOneSliceCheckFinished = null;

        private readonly OnFullCheckFinished m_OnFullCheckFinished = null;

        private readonly OnErrorOccurred m_OnErrorOccurred = null;

        public SizeCheckCallbackHandler(
            OnStartParametersCalculated a_OnStartParametersCalculated,
            OnOneSliceCheckFinished a_OnOneSliceCheckFinished,
            OnFullCheckFinished a_OnFullCheckFinished,
            OnErrorOccurred a_OnErrorOccurred)
        {
            this.m_OnStartParametersCalculated = a_OnStartParametersCalculated;
            this.m_OnOneSliceCheckFinished = a_OnOneSliceCheckFinished;
            this.m_OnFullCheckFinished = a_OnFullCheckFinished;
            this.m_OnErrorOccurred = a_OnErrorOccurred;
        }
    }

    #endregion Обработчик

}
