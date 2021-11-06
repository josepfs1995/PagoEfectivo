FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PagoEfectivo.Api.csproj", "src/services/PagoEfectivo.Api/"]
RUN dotnet restore "src/services/PagoEfectivo.Api/PagoEfectivo.Api.csproj"
COPY . .
RUN dotnet build  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PagoEfectivo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PagoEfectivo.Api.dll"]
