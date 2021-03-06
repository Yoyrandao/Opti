param (
  [string]$server,
  [string]$username,
  [string]$email
)
Write-Host "Enter docker registry password:";
$local:securePassword = Read-Host -AsSecureString;
$local:password = ConvertFrom-SecureString $local:securePassword;

kubectl create secret docker-registry registry-key `
  --docker-server=$server `
  --docker-username=$username `
  --docker-password=$local:password `
  --docker-email=$DOCKER_EMAIL

Write-Host "Credentials created."