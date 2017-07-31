chcp 1251
sc create KSPDWindowsService binpath= "%cd%\KSPDIGT.Server.Service.exe" start= auto DisplayName= "ÊÑÏÄ ÈÆÒ: סונגונ" 
sc description KSPDWindowsService "ÊÑÏÄ ÈÆÒ: סונגונ"
sc start KSPDWindowsService
