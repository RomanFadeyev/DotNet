chcp 1251
sc create KSPDWindowsService binpath= "%cd%\KSPDIGT.Server.Service.exe" start= auto DisplayName= "���� ���: ������" 
sc description KSPDWindowsService "���� ���: ������"
sc start KSPDWindowsService
