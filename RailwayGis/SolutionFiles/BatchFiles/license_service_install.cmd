chcp 1251
sc create KSPDLicenseService binpath= "%cd%\KSPDIGT.License.Service.exe" start= auto DisplayName= "КСПД ИЖТ: сервер лицензий"
sc description KSPDLicenseService "КСПД ИЖТ: сервер лицензий"
sc start KSPDLicenseService
