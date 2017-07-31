using System.Collections.Generic;
using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.Calculation;
using KSPDIGT.Base.DataContracts.SizeCheck;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    /// <summary>
    /// Обратный интерфейс для информирования клиента об промежуточных результатах или о конце полной обработки
    /// </summary>
    public interface ICalculationCallback
    {
        [OperationContract(IsOneWay = true)]
        void StartParametersCalculated(SizeCheckStartParameters a_StartParameters);
        
        [OperationContract(IsOneWay = true)]
        void OnePartCalculationFinished(List<CalculatedDistanceInfo> a_CalculatedDistanceInfos);

        [OperationContract(IsOneWay = true)]
        void FullCheckFinished();

        [OperationContract(IsOneWay = true)]
        void ErrorOccurred(ExceptionDetail a_Exception);
    }

    /// <summary>
    /// Сервис выполнения расчетов
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICalculationCallback))]
    public interface ICalculationService : IWcfService
    {
        [OperationContract(IsOneWay = true)]
        void Calculation(BasicDistanceCalculationParameters a_Parameters);

        [OperationContract(IsOneWay = true)]
        void CancelCalculation();
    }

    #region Обработчик

    /// <summary>
    /// Релизация обратного интерфейса для информирования клиента об промежуточных результатах или о конце полной обработки
    /// </summary>
    public class CalculationCallbackHandler : ICalculationCallback
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
        /// <param name="a_CalculatedDistanceInfos"></param>
        public delegate void OnOnePartCalculationFinished(List<CalculatedDistanceInfo> a_CalculatedDistanceInfos);

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

        void ICalculationCallback.StartParametersCalculated(SizeCheckStartParameters a_StartParameters)
        {
            if (this.m_OnStartParametersCalculated != null) this.m_OnStartParametersCalculated(a_StartParameters);
        }

        void ICalculationCallback.OnePartCalculationFinished(List<CalculatedDistanceInfo> a_CalculatedDistanceInfos)
        {
            if (this.m_OnOneSliceCheckFinished != null)
            {
                this.m_OnOneSliceCheckFinished(a_CalculatedDistanceInfos);
            }
        }

        void ICalculationCallback.FullCheckFinished()
        {
            if (this.m_OnFullCheckFinished != null) this.m_OnFullCheckFinished();
        }

        void ICalculationCallback.ErrorOccurred(ExceptionDetail a_Exception)
        {
            if (this.m_OnErrorOccurred != null) this.m_OnErrorOccurred(a_Exception);
        }

        #endregion Имплементация

        private readonly OnStartParametersCalculated m_OnStartParametersCalculated = null;

        private readonly OnOnePartCalculationFinished m_OnOneSliceCheckFinished = null;

        private readonly OnFullCheckFinished m_OnFullCheckFinished = null;

        private readonly OnErrorOccurred m_OnErrorOccurred = null;

        public CalculationCallbackHandler(
            OnStartParametersCalculated a_OnStartParametersCalculated,
            OnOnePartCalculationFinished a_OnOneSliceCheckFinished,
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
