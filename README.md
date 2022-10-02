# IMAP-OAuth-2.0-Example

https://youtu.be/eDTO9W81P-Y

Install-Module AzureAD
Install-Module ExchangeOnlineManagement

Connect-AzureAD
Connect-ExchangeOnline

$app = Get-AzureADServicePrincipal -SearchString "IMAP OAuth 2.0"
New-ServicePrincipal -AppId $app.AppId -ServiceId $app.ObjectId -DisplayName "IMAP OAuth Service Principal"

Add-MailboxPermission -Identity "MAILTOACCESS" -User $app.ObjectId -AccessRights FullAccess

$startDate = Get-Date
$endDate = $startDate.AddYears(99)

New-AzureADApplicationPasswordCredential -ObjectId "OBJECTID" -CustomKeyIdentifier "IMAP Secret" -StartDate $startDate -EndDate $endDate
