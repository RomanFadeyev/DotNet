chcp 1251
sc create PointCloudWindowsService binpath= "%cd%\KSPDIGT.PointCloudServer.Service.exe" start= auto DisplayName= "���� ���: ������ �����" 
sc description PointCloudWindowsService "���� ���: ������ �����"
sc start PointCloudWindowsService
