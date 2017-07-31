using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.LogItems;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    [DataContract]
    public enum ProcessWithCallbackResultTypeEnum
    {
        /// <summary>
        /// Все прошло хорошо
        /// </summary>
        [EnumMember]
        Ok = 0,

        /// <summary>
        /// Отменено пользователем
        /// </summary>
        [EnumMember]
        Canceled = 1,

        /// <summary>
        /// Возникли ошибки
        /// </summary>
        [EnumMember]
        Error = 2,
    }

    
    [DataContract]
    [KnownType(typeof(DatumMarksUpdateResult))]
    public class ProcessWithCallbackResult
    {
        /// <summary>
        /// Тип результата
        /// </summary>
        [DataMember]
        public ProcessWithCallbackResultTypeEnum Type { get; set; }

        /// <summary>
        /// Строка сообщения
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class DatumMarksUpdateResult : ProcessWithCallbackResult
    {
        [DataMember]
        public uint AddedRecordCount { get; set; }

        [DataMember]
        public uint UpdatedRecordCount { get; set; }

        [DataMember]
        public uint IdenticalRecordCount { get; set; }

        [DataMember]
        public uint DeletedRecordCount { get; set; }

        /// <summary>
        /// Лог выполнения
        /// </summary>
        [DataMember]
        public List<LogItemInfoClass> LogItems { get; set; }
    }

    [DataContract]
    public struct ProcessWithCallbackProgressReport
    {
        /// <summary>
        /// Минимальная позиция
        /// </summary>
        [DataMember]
        public uint Min;

        /// <summary>
        /// Максимальная позиция
        /// </summary>
        [DataMember]
        public uint Max;

        /// <summary>
        /// Текущая позиция
        /// </summary>
        [DataMember]
        public uint Position;

        /// <summary>
        /// Сообщение
        /// </summary>
        [DataMember]
        public string Message;

        public void Step(string a_Message = null)
        {
            this.Position++;
            if (a_Message != null)
                this.Message = a_Message;
        }
    }


    public interface ICallbackForProcessWithCallbackService
    {
        [OperationContract(IsOneWay = true)]
        void Initialized(ProcessWithCallbackProgressReport a_Report);

        [OperationContract(IsOneWay = true)]
        void ProgressReport(ProcessWithCallbackProgressReport a_Report);

        [OperationContract(IsOneWay = true)]
        void Finished(ProcessWithCallbackResult a_Result);

        [OperationContract(IsOneWay = true)]
        void ErrorOccurred(ExceptionDetail a_Exception);
    }
    
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallbackForProcessWithCallbackService))]
    public interface IProcessWithCallbackService : IWcfService
    {
        /// <summary>
        ///Отражения сведений о реперах на карте
        /// </summary>
        /// <param name="a_MapFragmentGids">GID фрагментов карт</param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void UpdateDatumMarksForMaps(List<Guid> a_MapFragmentGids);

        /// <summary>
        /// Отменяет текующую операцию
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Cancel();
    }

    #region Обработчик

    /// <summary>
    /// Релизация обратного интерфейса для информирования клиента об промежуточных результатах или о конце полной обработки
    /// </summary>
    public class CallbackForProcessWithCallbackServiceHandler : ICallbackForProcessWithCallbackService
    {
        #region Типы

        /// <summary>
        /// Делегат используемый в OnInitialized
        /// </summary>
        /// <param name="a_Report"></param>
        public delegate void OnInitializedDelegate(ProcessWithCallbackProgressReport a_Report);

        /// <summary>
        /// Делегат используемый в ProgressReport
        /// </summary>
        /// <param name="a_Report"></param>
        public delegate void OnProgressReportDelegate(ProcessWithCallbackProgressReport a_Report);

        /// <summary>
        /// Делегат используемый в OnFinished
        /// </summary>
        /// <param name="a_Result"></param>
        public delegate void OnFinishedDelegate(ProcessWithCallbackResult a_Result);

        /// <summary>
        /// Делегат используемый в ErrorOccurred
        /// </summary>
        /// <param name="a_Exception"></param>
        public delegate void OnErrorOccurredDelegate(ExceptionDetail a_Exception);

        #endregion Типы

        #region Хранение

        private readonly OnInitializedDelegate m_OnInitialized = null;

        private readonly OnProgressReportDelegate m_OnProgressReport = null;

        private readonly OnFinishedDelegate m_OnFinished = null;

        private readonly OnErrorOccurredDelegate m_OnErrorOccurred = null;

        #endregion Хранение

        #region Имплементация

        void ICallbackForProcessWithCallbackService.Initialized(ProcessWithCallbackProgressReport a_Report)
        {
            if (this.m_OnInitialized != null) this.m_OnInitialized(a_Report);
        }

        void ICallbackForProcessWithCallbackService.ProgressReport(ProcessWithCallbackProgressReport a_Report)
        {
            if (this.m_OnProgressReport != null) this.m_OnProgressReport(a_Report);
        }

        void ICallbackForProcessWithCallbackService.Finished(ProcessWithCallbackResult a_Result)
        {
            if (this.m_OnFinished != null) this.m_OnFinished(a_Result);
        }

        void ICallbackForProcessWithCallbackService.ErrorOccurred(ExceptionDetail a_Exception)
        {
            if (this.m_OnErrorOccurred != null) this.m_OnErrorOccurred(a_Exception);
        }

        #endregion Имплементация

        public CallbackForProcessWithCallbackServiceHandler(
            OnInitializedDelegate a_OnInitialized, 
            OnProgressReportDelegate a_OnProgressReport, 
            OnFinishedDelegate a_OnFinished,
            OnErrorOccurredDelegate a_OnErrorOccurred)
        {
            this.m_OnInitialized = a_OnInitialized;
            this.m_OnProgressReport = a_OnProgressReport;
            this.m_OnFinished = a_OnFinished;
            this.m_OnErrorOccurred = a_OnErrorOccurred;
        }

    }

    #endregion Обработчик
}
