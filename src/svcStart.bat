cd "backend\BackgroundAgent\bin\Debug\net5.0\"
start /B /MIN "" "BackgroundAgent.exe"

cd "../../../../../"

cd "backend\SyncGateway\bin\Debug\net5.0\"
start /B /MIN "" "SyncGateway.exe"

cls