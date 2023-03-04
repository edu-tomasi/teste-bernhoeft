FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /inventario.command-api

COPY . .

RUN dotnet restore

WORKDIR /inventario.command-api/src/inventario.web-api/
RUN dotnet publish --no-restore -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY --from=build /out .
ENTRYPOINT ["dotnet", "inventario.web-api.dll"]