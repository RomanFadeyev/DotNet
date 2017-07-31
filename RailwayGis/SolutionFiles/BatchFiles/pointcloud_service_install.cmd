chcp 1251
sc create PointCloudWindowsService binpath= "%cd%\KSPDIGT.PointCloudServer.Service.exe" start= auto DisplayName= "КСПД ИЖТ: облака точек" 
sc description PointCloudWindowsService "КСПД ИЖТ: облака точек"
sc start PointCloudWindowsService
