param (
	[switch]$withoutDockerCompose = $false
)

$ErrorActionPreference = "Stop"

function SetVariablesFromDotEnv {
	[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'Low')]
	param($localEnvFile = "./external/postgres/pg-env.list")

	if (!( Test-Path $localEnvFile)) {
		throw "could not open $localEnvFile"
	}

	Write-Host 'Setting environment variables...' -ForegroundColor Blue
	$content = Get-Content $localEnvFile -ErrorAction Stop;

	foreach ($line in $content) {
		if ($line.StartsWith('#')) {
			continue;
		}

		if ($line.Trim()) {
			$line = $line.Replace("`"", "");
			$keyValuePair = $line -split "=", 2

			if ($PSCmdlet.ShouldProcess("$($keyValuePair[0])", "set value $($keyValuePair[1])")) {
				[Environment]::SetEnvironmentVariable($keyValuePair[0].Trim(), $keyValuePair[1].Trim(), "Process") | Out-Null

				Write-Host "$($keyValuePair[0].Trim()) has been set";
			}
		}
	}
}

SetVariablesFromDotEnv;

$local:postgresUsername = [Environment]::GetEnvironmentVariable('PG_PRIMARY_USER');
$local:postgresPassword = [Environment]::GetEnvironmentVariable('PG_PASSWORD');
$local:primaryDatabase = [System.Environment]::GetEnvironmentVariable('PG_DATABASE');

if (-not($withoutDockerCompose)) {
  Write-Host 'Reacreating database container...' -ForegroundColor Blue;
  docker rm -f common_database;
  docker volume rm postgres_db_data;
  docker-compose -f ./external/postgres/docker-compose.yml up -d --force-recreate;
  Write-Host 'Done.' -ForegroundColor Green;

	Write-Host 'Recreating ftp container...' -ForegroundColor Blue;
	docker rm -f ftp_server;
	docker volume rm ftp_storage ftp_user_data;
	docker-compose -f ./infra/ftp/docker-compose.yml up -d --force-recreate;
	Write-Host 'Done.' -ForegroundColor Green;
}

Write-Host 'Recreating database...' -ForegroundColor Blue;
docker exec common_database bash -c "sleep 5 && PGPASSWORD=$($local:postgresPassword) psql -h 0.0.0.0 -U $($local:postgresUsername) -f /home/temp/scripts/init_db.sql"
docker exec common_database bash -c "sleep 5 && PGPASSWORD=$($local:postgresPassword) psql $($local:primaryDatabase) -h 0.0.0.0 -U $($local:postgresUsername) -f /home/temp/scripts/db.sql";
Write-Host 'Done.' -ForegroundColor Green;