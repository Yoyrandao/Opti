param (
  [string]$server,
  [string]$username,
  [string]$email
)
Write-Host "Enter docker registry password:";
$local:password = Read-Host;

kubectl create secret docker-registry registry-key `
  --docker-server=$server `
  --docker-username=$username `
  --docker-password=$local:password `
  --docker-email=$email

Write-Host "Credentials created."