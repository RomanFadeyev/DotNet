chcp 1251
sc create KSPDLicenseService binpath= "%cd%\KSPDIGT.License.Service.exe" start= auto DisplayName= "���� ���: ������ ��������"
sc description KSPDLicenseService "���� ���: ������ ��������"
sc start KSPDLicenseService
