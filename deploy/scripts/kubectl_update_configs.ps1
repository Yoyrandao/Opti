
function ApplyConfig($secretName, $filePath) {
    Write-Host "deleting old config";
    kubectl delete secret $secretName
    Write-Host "deleting old config";

    Write-Host "updating $($secretName)...";
    kubectl create secret generic $secretName --from-file=$filePath;
    Write-Host "updated.";
}

$local:configs = @(
    @{
        secretName = 'syncgateway-appsettings'
        filePath = '../k8s/backend/configs/appsettings.Kubernetes.json'
    },
    @{
        secretName = 'cc-appsettings'
        filePath = '../k8s/backend/configs/logger_config.yml'
    }
);

foreach ($config in $local:configs) {
    ApplyConfig $config.secretName $config.filePath
}