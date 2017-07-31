chcp 1251
sc create KSPDUpdateService binpath= "%cd%\KSPDIGT.Update.WinService.exe" start= auto DisplayName= "КСПД ИЖТ: сервис обновлений" 
sc description KSPDUpdateService "КСПД ИЖТ: сервис обновлений"
sc start KSPDUpdateService
