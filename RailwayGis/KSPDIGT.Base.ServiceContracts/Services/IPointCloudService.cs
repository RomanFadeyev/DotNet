using System.Collections.Generic;
using System.Drawing;
using System.ServiceModel;
using Intech.Common.Wcf;
using KSPDIGT.Base.DataContracts.Geometry;
using KSPDIGT.Base.DataContracts.PointCloud;

namespace KSPDIGT.Base.ServiceContracts.Services
{
    /// <summary>
    /// Сервис доступа к данным лазерного сканирования (LAS)
    /// </summary>
    [ServiceContract]
    public interface IPointCloudService : IWcfService
    {
        /// <summary>
        /// Получить изображение видимости пути
        /// </summary>
        /// <param name="a_SurveyPaths">Пути к каталогам с проездами</param>
        /// <param name="a_EyePoint">Точка наблудалеля (на уровне оси пути)</param>
        /// <param name="a_TargetPoint">Точка цели (на уровне оси пути)</param>
        /// <param name="a_ImageSize">Размер изображения в пикселях</param>
        /// <param name="a_BannerSize">Размер баннера в метрах</param>
        /// <param name="a_EyePointH">Уровень глаз водителя машиниста над осью пути (мм)</param>
        /// <returns></returns>
        [OperationContract]
        Bitmap GetBitmap(string[] a_SurveyPaths, Point3D a_EyePoint, Point3D a_TargetPoint,
            Size a_ImageSize, Size? a_BannerSize, uint a_EyePointH);

        [OperationContract]
        Bitmap GetCloudBitmap(string[] a_SurveyPaths, Point3D a_EyePoint, Point3D a_TargetPoint,
            Size a_ImageSize, PointsWithinPolygonParameters a_QueryParams, List<Point2D> a_FilteringPolygon,  double a_LasPointSize, List<GeometryObject> a_GeometryObjects/*,
            List<GeometryObject> a_TopGeometryObjects*/);
        
        /// <summary>
        /// Получение информации о сборках
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetBuildsInfo();

        /// <summary>
        /// Получить логи
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        byte[] GetLogs();

        /// <summary>
        /// Проверяет, является ли указанная директория облаком точек
        /// и доступна ли она с сервера ТЛО
        /// </summary>
        /// <param name="a_Directory">Директория с точками</param>
        /// <param name="a_AccessResult">Результат доступа</param>
        /// <param name="a_PointCloudVersion">Версия облака</param>
        [OperationContract]
        void IsPointCloudAvailable(string a_Directory, out bool a_AccessResult, out int a_PointCloudVersion);
    }
}