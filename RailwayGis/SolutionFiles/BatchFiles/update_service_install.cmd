chcp 1251
sc create KSPDUpdateService binpath= "%cd%\KSPDIGT.Update.WinService.exe" start= auto DisplayName= "���� ���: ������ ����������" 
sc description KSPDUpdateService "���� ���: ������ ����������"
sc start KSPDUpdateService
