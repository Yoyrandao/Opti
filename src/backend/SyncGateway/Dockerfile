FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-environment
EXPOSE 5000
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet public -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-environment /app/out .
ENTRYPOINT ["dotnet", "SyncGateway.dll", "--urls=http://0.0.0.0:5000"]