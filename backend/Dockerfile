FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["DepiDataBase/DepiDataBase.csproj", "DepiDataBase/"]

RUN dotnet restore "WebApplication1/WebApplication1.csproj"

COPY . .

WORKDIR /src/WebApplication1
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish \
    /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=publish /app/publish .

COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

USER appuser

EXPOSE 8080

ENTRYPOINT ["/app/entrypoint.sh"]
