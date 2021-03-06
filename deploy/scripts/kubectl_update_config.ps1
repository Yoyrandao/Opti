param (
  [string]$secretName,
  [string]$filePath
)

Write-Host "updating $($secretName)...";
kubectl create secret generic $secretName --from-file=$filePath;
Write-Host "updated.";
